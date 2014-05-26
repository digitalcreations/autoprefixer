namespace BundleTransformer.Autoprefixer.Configuration
{
	using System.Configuration;

	/// <summary>
	/// Configuration settings of Clean CSS-minifier
	/// </summary>
	public sealed class BrowserSettingsCollection : ConfigurationElementCollection
	{
	    protected override ConfigurationElement CreateNewElement()
	    {
	        return new BrowserSettings();
	    }

	    protected override object GetElementKey(ConfigurationElement element)
	    {
            return ((BrowserSettings)element).Definition;
	    }
	}
}