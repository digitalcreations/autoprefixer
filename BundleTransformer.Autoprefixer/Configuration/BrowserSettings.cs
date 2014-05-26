namespace BundleTransformer.Autoprefixer.Configuration
{
    using System.Configuration;

    public sealed class BrowserSettings : ConfigurationElement
    {
        [ConfigurationProperty("definition", IsRequired = true)]
        public string Definition
        {
            get { return (string)this["definition"]; }
            set { this["definition"] = value; }
        }
    }
}