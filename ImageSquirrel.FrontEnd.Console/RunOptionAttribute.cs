using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ImageSquirrel.FrontEnd.CommandLine
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal class RunOptionAttribute : Attribute
    {
        public RunOptionAttribute(string description, string key = null)
        {
            this.Key = key;
            this.Description = description;
        }

        public string Key { get; private set; }
        public string Description { get; private set; }

        public static IReadOnlyList<RunOptionTarget> GetImplementors(Type type)
        {
            return type
                .GetMethods()
                .Select(x => new RunOptionTarget(x, x.GetCustomAttribute<RunOptionAttribute>()))
                .Where(x => x.Attribute != null)
                .ToList();
        }
    }
}
