namespace BundleTransformer.Autoprefixer.Configuration
{
	using System.Configuration;

	using Core.Configuration;

	/// <summary>
	/// Configuration settings of Clean-css Minifier
	/// </summary>
	public sealed class AutoprefixerSettings : ConfigurationSection
	{
		/// <summary>
		/// Get the list of supported browser versions.
		/// </summary>
		[ConfigurationProperty("browsers")]
        [ConfigurationCollection(typeof(BrowserSettingsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
		public BrowserSettingsCollection Browsers
		{
			get { return (BrowserSettingsCollection) this["browsers"]; }
		}

        [ConfigurationProperty("cascade", DefaultValue = false)]
	    public bool Cascade
	    {
            get { return (bool) this["cascade"]; }
            set { this["cascade"] = value; }
	    }

		/// <summary>
		/// Gets a configuration settings of JavaScript engine
		/// </summary>
		[ConfigurationProperty("jsEngine")]
		public JsEngineSettings JsEngine
		{
			get { return (JsEngineSettings)this["jsEngine"]; }
		}
	}
}