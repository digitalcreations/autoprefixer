using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoprefixer
{
    public enum Browsers
    {
        Android,
        BlackBerry,
        Chrome,
        Firefox,
        Explorer,
        iOS,
        Opera,
        Safari,
    }

    public sealed class BrowserSpecification
    {
        private readonly IList<string> _browsers;

        public BrowserSpecification()
        {
            _browsers = new List<string>();
        }

        public void None()
        {
            _browsers.Clear();
            _browsers.Add("none");
        }

        public BrowserSpecification Browser(Browsers browser)
        {
            _browsers.Add(Enum.GetName(typeof(Browsers), browser));
            return this;
        }

        public BrowserSpecification BrowserVersion(Browsers browser, decimal version)
        {
            _browsers.Add(string.Format("{0} {1}", Enum.GetName(typeof(Browsers), browser), version));
            return this;
        }
        public BrowserSpecification BrowserVersionGreaterThan(Browsers browser, decimal version)
        {
            _browsers.Add(string.Format("{0} > {1}", Enum.GetName(typeof(Browsers), browser), version));
            return this;
        }

        public BrowserSpecification BrowserVersionGreaterThanOrEqual(Browsers browser, decimal version)
        {
            _browsers.Add(string.Format("{0} >= {1}", Enum.GetName(typeof(Browsers), browser), version));
            return this;
        }

        public BrowserSpecification LastNVersions(int versions)
        {
            _browsers.Add(string.Format("last {0} version", versions));
            return this;
        }

        public BrowserSpecification WithMinimumUsagePercentage(double percentage)
        {
            _browsers.Add(string.Format("> {0}%", Math.Round(percentage * 100)));
            return this;
        }

        internal IList<string> GetBrowserSpecificationStrings()
        {
            return _browsers;
        }
    }
}
