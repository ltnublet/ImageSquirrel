using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ImageSquirrel.DataSources.External;
using ImageSquirrel.DataSources.FolderData;
using ImageSquirrel.Formats.External;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageSquirrel.FrontEnd.CommandLine
{
    public class Program
    {
        [ImportMany(typeof(IDataSourceFactory))]
        public IDataSourceFactory[] Factories = new IDataSourceFactory[0];

        private Dictionary<string, RunOptionTarget> options;
        private bool exitRequested = false;

        public Program(string[] args)
        {
            // Load all IDataSourceFactory from plugins.
            IEnumerable<string> pluginLocations =
                new string[] { Assembly.GetExecutingAssembly().Location }.Concat(args);

            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                foreach (string location in pluginLocations)
                {
                    string buffer = Path.GetFullPath(location);
                    if (File.Exists(buffer))
                    {
                        buffer = Directory.GetParent(buffer).FullName;
                    }

                    catalog.Catalogs.Add(new DirectoryCatalog(buffer));
                }

                using (CompositionContainer container = new CompositionContainer(catalog))
                {
                    container.ComposeParts(this);
                }
            }

            // Populate command line argument options.
            int keylessInstances = 0;
            this.options = new Dictionary<string, RunOptionTarget>(StringComparer.OrdinalIgnoreCase);
            foreach (RunOptionTarget target in RunOptionAttribute.GetImplementors(this.GetType()))
            {
                options.Add(
                    target.Attribute.Key ?? (++keylessInstances).ToString(),
                    target);
            }
        }

        public static void Main(string[] args)
        {
            Program program = new Program(args);
            program.Run();
        }

        public void Run()
        {
            while (!this.exitRequested)
            {
                Console.WriteLine("\r\n" + new string('=', Console.BufferWidth - 1));

                foreach (KeyValuePair<string, RunOptionTarget> option in this.options)
                {
                    Console.WriteLine(option.Key + ": " + option.Value.Attribute.Description);
                }

                Console.Write("Enter option: ");

                string selected = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    this.options[selected].Method.Invoke(this, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unhandled error: " + e.Message);
                }
            }
        }

        [RunOption("Enumerates loaded `IDataSourceFactory`s.")]
        public void EnumerateDataSourceFactories()
        {
            foreach (IDataSourceFactory factory in this.Factories)
            {
                Console.WriteLine($"{factory.GetType()}:");

                foreach (IConfigurationRequirement requirement in factory.Requirements)
                {
                    Console.WriteLine(JObject.Parse(requirement.ToString()).ToString(Formatting.Indented));
                }
            }
        }

        [RunOption("Tests out the FolderDataSourceFactory.")]
        public void TestFolderDataSourceFactory()
        {
            IDataSourceFactory factory = this.Factories.Single(x => x.GetType() == typeof(FolderDataSourceFactory));
            Dictionary<IConfigurationRequirement, object> inputs =
                factory
                    .Requirements
                    .Select(
                        requirement =>
                        {
                            Console.Write(requirement.Name + " - (" + requirement.Description + "): ");
                            string input = Console.ReadLine();
                            if (input == "null")
                            {
                                input = null;
                            }

                            return new KeyValuePair<IConfigurationRequirement, object>(requirement, input);
                        })
                    .ToDictionary(x => x.Key, x => x.Value);

            IConfigurationRequirement rootRequirement = factory.Requirements.Single(x => x.Name == "Root Path");
            inputs[rootRequirement] = new FilePath((string)inputs[rootRequirement]);
            IConfigurationRequirement tickRequirement =
                factory.Requirements.Single(x => x.Name == "Change Filter Ticks");
            inputs[tickRequirement] = inputs[tickRequirement] ==
                null ? 0L : long.Parse((string)inputs[tickRequirement]);

            IDataSource dataSource = factory.MakeDataSource(inputs);

            dataSource.OnChange +=
                (obj, e) =>
                {
                    DataSourceChangeEventType type = e.EventType;
                    IDataInformation information = e.GetImageInformation();
                    if (type == DataSourceChangeEventType.Added)
                    {
                        Console.WriteLine("Added: " + information.Name);
                    }
                    else if (type == DataSourceChangeEventType.Changed)
                    {
                        Console.WriteLine("Changed: " + information.Name);
                    }
                    else if (type == DataSourceChangeEventType.Removed)
                    {
                        Console.WriteLine("Removed: " + information.Name);
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected DataSourceChangeEventType value.");
                    }
                };

            Console.WriteLine("Monitoring folder '" + dataSource.Name +"'. Press enter to stop.");
            Console.ReadLine();

            dataSource.Dispose();
        }

        [RunOption("Exits the application.", "Q")]
        public void Exit()
        {
            this.exitRequested = true;
        }
    }
}
