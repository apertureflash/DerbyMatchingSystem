using System.Configuration;

namespace CockDerbyMatchingSystem.Classes
{
    public class Reports : ConfigurationSection
    {
        /// <summary>
        /// The value of the property here "Folders" needs to match that of the config file section
        /// </summary>
        [ConfigurationProperty("FilterKeys")]
        public FilterHashKeyCollection HashKeys
        {
            get { return ((FilterHashKeyCollection)(base["FilterKeys"])); }
        }
    }

    /// <summary>
    /// The collection class that will store the list of each element/item that
    /// is returned back from the configuration manager.
    /// </summary>
    [ConfigurationCollection(typeof(FilterHashElement))]
    public class FilterHashKeyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FilterHashElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FilterHashElement)(element)).Key;
        }

        public FilterHashElement this[int idx]
        {
            get
            {
                return (FilterHashElement)BaseGet(idx);
            }
        }
    }

    /// <summary>
    /// The class that holds onto each element returned by the configuration manager.
    /// </summary>
    public class FilterHashElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return ((string)(base["key"]));
            }
            set
            {
                base["key"] = value;
            }
        }

        [ConfigurationProperty("value", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Value
        {
            get
            {
                return ((string)(base["value"]));
            }
            set
            {
                base["value"] = value;
            }
        }
    }




    public class BlogSettings : ConfigurationSection
    {
        private static BlogSettings settings = ConfigurationManager.GetSection("BlogSettings") as BlogSettings;

        public static BlogSettings Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("frontPagePostCount", DefaultValue = 20, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 100)]
        public int FrontPagePostCount
        {
            get { return (int)this["frontPagePostCount"]; }
            set { this["frontPagePostCount"] = value; }
        }

        [ConfigurationProperty("title", IsRequired = true)]        
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\", MinLength = 1, MaxLength = 256)]
        public string Title
        {
            get { return (string)this["title"]; }
            set { this["title"] = value; }
        }
    }
}
