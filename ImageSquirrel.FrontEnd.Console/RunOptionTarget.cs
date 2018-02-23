using System.Reflection;

namespace ImageSquirrel.FrontEnd.CommandLine
{
    internal class RunOptionTarget
    {
        public RunOptionTarget(MethodInfo method, RunOptionAttribute attribute)
        {
            this.Method = method;
            this.Attribute = attribute;
        }

        public MethodInfo Method { get; private set; }
        public RunOptionAttribute Attribute { get; private set; }
    }
}
