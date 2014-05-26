using Autoprefixer;

namespace BundleTransformer.Autoprefixer.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using JavaScriptEngineSwitcher.Core;

    using Core;
    using Core.Assets;
    using Core.Resources;
    using Core.Translators;
    using Configuration;

    /// <summary>
	/// Minifier, which produces minifiction of CSS-code 
	/// by using Clean-css
	/// </summary>
	public sealed class AutoprefixerTranslator : ITranslator
	{
        /// <summary>
        /// Name of minifier
        /// </summary>
        const string TranslatorName = "Autoprefixer";

        /// <summary>
        /// Name of code type
        /// </summary>
        const string CodeType = "CSS";

		/// <summary>
		/// Delegate that creates an instance of JavaScript engine
		/// </summary>
		private readonly Func<IJsEngine> _createJsEngineInstance;

	    public IList<string> Browsers
	    {
	        get; 
            set;
        }

		/// <summary>
		/// Constructs instance of Clean CSS-minifier
		/// </summary>
		public AutoprefixerTranslator()
			: this(null, BundleTransformerContext.Current.GetCleanConfiguration())
		{ }

		/// <summary>
		/// Constructs instance of Clean CSS-minifier
		/// </summary>
		/// <param name="createJsEngineInstance">Delegate that creates an instance of JavaScript engine</param>
		/// <param name="autoprefixerConfig">Configuration settings of Clean-css Minifier</param>
		public AutoprefixerTranslator(Func<IJsEngine> createJsEngineInstance, AutoprefixerSettings autoprefixerConfig)
		{
			BrowserSettingsCollection browserConfig = autoprefixerConfig.Browsers;
		    Browsers = new List<string>();
		    foreach (BrowserSettings browser in browserConfig)
		    {
		        Browsers.Add(browser.Definition);
		    }

			if (createJsEngineInstance == null)
			{
				var jsEngineName = autoprefixerConfig.JsEngine.Name;
				if (string.IsNullOrWhiteSpace(jsEngineName))
				{
					throw new ConfigurationErrorsException(
						string.Format(Strings.Configuration_JsEngineNotSpecified,
							"autoprefixer",
							@"
  * JavaScriptEngineSwitcher.V8
  * JavaScriptEngineSwitcher.Msie",
							"V8JsEngine")
					);
				}

				createJsEngineInstance = (() =>
					JsEngineSwitcher.Current.CreateJsEngineInstance(jsEngineName));
			}
			_createJsEngineInstance = createJsEngineInstance;
		}


	    public bool IsDebugMode { get; set; }
	    public IAsset Translate(IAsset asset)
	    {
	        var translated = Translate(new[] {asset});
	        return translated[0];
	    }

	    public IList<IAsset> Translate(IList<IAsset> assets)
	    {
            if (assets == null)
            {
                throw new ArgumentException(Strings.Common_ValueIsEmpty, "assets");
            }

            var assetsToProcessing = assets.Where(a => a.IsStylesheet && !a.Minified).ToList();
            if (assetsToProcessing.Count == 0)
            {
                return assets;
            }

            using (var autoprefixer = new Compiler(_createJsEngineInstance))
            {
                foreach (var asset in assetsToProcessing)
                {
                    try
                    {
                        asset.Content = autoprefixer.Prefix(asset.Content, Browsers);
                    }
                    catch (AutoprefixerException e)
                    {
                        throw new AssetTranslationException(
                            string.Format(Strings.Translators_TranslationSyntaxError,
                                CodeType, asset.Url, TranslatorName, e.Message));
                    }
                    catch (Exception e)
                    {
                        throw new AssetTranslationException(
                            string.Format(Strings.Translators_TranslationFailed,
                                CodeType, asset.Url, TranslatorName, e.Message), e);
                    }
                }
            }

            return assets;
	    }
	}
}