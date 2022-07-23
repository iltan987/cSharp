namespace DatabaseManager
{
    public class DatabaseColumnProperty
    {
        public DatabaseColumnProperty(string dataType, bool notNull = false, bool unique = false, ForeignKey foreignKey = null, Check check = null, Default @default = null)
        {
            this.dataType = dataType;
            this.notNull = notNull;
            this.unique = unique;
            this.foreignKey = foreignKey;
            this.@default = @default;
        }

        internal string dataType { get; set; }
        internal bool notNull { get; set; }
        internal bool unique { get; set; }
        internal ForeignKey foreignKey { get; set; }
        internal Check check { get; set; }
        internal Default @default { get; set; }

        public static class DataTypes
        {
            public const string INT = "INT";
            public const string TEXT = "VARCHAR(MAX)";
            public const string DATE = "DATE";
        }

        public class ForeignKey
        {
            public ForeignKey(string tableName, string primaryKey)
            {
                this.tableName = tableName;
                this.primaryKey = primaryKey;
            }

            public string tableName { get; set; }
            public string primaryKey { get; set; }

            public override string ToString()
            {
                return $"FOREIGN KEY REFERENCES {tableName}({primaryKey})";
            }
        }

        public class Check
        {
            public Check(string condition)
            {
                this.condition = condition;
            }

            private string condition { get; set; }

            public override string ToString()
            {
                return $"CHECK ({condition})";
            }
        }

        public class Default
        {
            public Default(string value)
            {
                this.value = value;
            }

            public string value { get; set; }

            public override string ToString()
            {
                return $"DEFAULT '{value}'";
            }
        }
    }
}