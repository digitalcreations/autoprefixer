namespace BundleTransformer.Autoprefixer
{
	using System;
	using System.Configuration;

	using Core;
	using Configuration;

	/// <summary>
	/// Bundle transformer context extensions
	/// </summary>
	public static class BundleTransformerContextExtensions
	{
		/// <summary>
		/// Configuration settings of Clean-css Minifier
		/// </summary>
		private static readonly Lazy<AutoprefixerSettings> CleanConfig =
			new Lazy<AutoprefixerSettings>(() => (AutoprefixerSettings)ConfigurationManager.GetSection("bundleTransformer/autoprefixer"));

		/// <summary>
		/// Gets a Clean-css Minifier configuration settings
		/// </summary>
		/// <param name="context">Bundle transformer context</param>
		/// <returns>Configuration settings of Clean-css Minifier</returns>
		public static AutoprefixerSettings GetCleanConfiguration(this BundleTransformerContext context)
		{
			return CleanConfig.Value;
		}
	}
}