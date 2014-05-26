using System;
using System.IO;
using System.Linq;
using System.Reflection;
using JavaScriptEngineSwitcher.Core.Resources;
using JavaScriptEngineSwitcher.V8;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Autoprefixer.Tests
{
    [TestClass]
    public class AutoprefixerTests
    {
        [TestMethod]
        public void AutoprefixerCoreTests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resources = assembly.GetManifestResourceNames()
                .Where(r => r.StartsWith("Autoprefixer.Tests.Assets"))
                .Select(r => r.Replace(".css", "").Replace(".out", ""))
                .Distinct();
            
            Assert.AreEqual(23, resources.Count());

            using (var autoprefixer = new Compiler(() => new V8JsEngine()))
            {
                foreach (var resource in resources)
                {
                    var testName = resource.Replace("Autoprefixer.Tests.Assets.", "");
                    var cssIn = GetResourceAsString(resource + ".css", assembly);
                    var expectedCss = GetResourceAsString(resource + ".out.css", assembly);
                    var cssOut = autoprefixer.Prefix(cssIn, BrowserSpecificationFromTestName(testName), new Options
                    {
                        Cascade = testName == "cascade"
                    });
                    Assert.AreEqual(expectedCss, cssOut, 
                        "Autoprefixer test case " + testName + " did not produce expected output");
                }
            }
        }

        private BrowserSpecification BrowserSpecificationFromTestName(string name)
        {
            switch (name)
            {
                case "keyframes":
                    return new BrowserSpecification()
                        .BrowserVersionGreaterThan(Browsers.Chrome, 19)
                        .BrowserVersion(Browsers.Opera, 12);
                case "border-radius":
                    return new BrowserSpecification()
                        .BrowserVersion(Browsers.Safari, 4)
                        .BrowserVersion(Browsers.Firefox, 3.6m);
                case "vendor-hack":
                case "mistakes":
                    var browserSpecification = new BrowserSpecification();
                    browserSpecification.None();
                    return browserSpecification;
                case "gradient":
                    return new BrowserSpecification()
                        .BrowserVersion(Browsers.Chrome, 25)
                        .BrowserVersion(Browsers.Opera, 12)
                        .BrowserVersion(Browsers.Android, 2.3m);
                case "flexbox":
                case "flex-rewrite":
                case "double":
                    return new BrowserSpecification()
                        .BrowserVersionGreaterThan(Browsers.Chrome, 19)
                        .BrowserVersion(Browsers.Firefox, 21)
                        .BrowserVersion(Browsers.Explorer, 10);
                case "selectors":
                case "placeholder":
                    return new BrowserSpecification()
                        .BrowserVersion(Browsers.Chrome, 25)
                        .BrowserVersionGreaterThan(Browsers.Firefox, 17)
                        .BrowserVersion(Browsers.Explorer, 10);
                case "intrinsic":
                case "multicolumn":
                    return new BrowserSpecification()
                        .BrowserVersion(Browsers.Chrome, 25)
                        .BrowserVersion(Browsers.Firefox, 22);
                case "cascade":
                    // something missing here (cascade = true):
                    return new BrowserSpecification()
                        .BrowserVersionGreaterThan(Browsers.Chrome, 19)
                        .BrowserVersion(Browsers.Firefox, 21)
                        .BrowserVersion(Browsers.Explorer, 10);
                case "ie-transform":
                    return new BrowserSpecification()
                        .BrowserVersionGreaterThan(Browsers.Explorer, 0);
                default:
                    return new BrowserSpecification()
                        .BrowserVersion(Browsers.Chrome, 25)
                        .BrowserVersion(Browsers.Opera, 12);
            }
        }

        public static string GetResourceAsString(string resourceName, Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new NullReferenceException(
                        string.Format(Strings.Resources_ResourceIsNull, resourceName));
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
