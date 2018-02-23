namespace ImageSquirrel.DataSources.External
{
    public class DataSourceChangeEventType
    {
        private const string AddedName = "Added";
        private const string ChangedName = "Changed";
        private const string RemovedName = "Removed";

        private string name;

        private DataSourceChangeEventType(string name)
        {
            this.name = name;
        }

        public static DataSourceChangeEventType Added { get; } = new DataSourceChangeEventType(AddedName);

        public static DataSourceChangeEventType Changed { get; } = new DataSourceChangeEventType(ChangedName);

        public static DataSourceChangeEventType Removed { get; } = new DataSourceChangeEventType(RemovedName);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            const int added = 0;
            const int changed = 1;
            const int removed = 2;
            const int unknown = -1;

            if (this.Equals(DataSourceChangeEventType.Added))
            {
                return added;
            }
            else if (this.Equals(DataSourceChangeEventType.Changed))
            {
                return changed;
            }
            else if (this.Equals(DataSourceChangeEventType.Removed))
            {
                return removed;
            }
            else
            {
                return unknown;
            }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
