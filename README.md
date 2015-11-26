# DEPRECATED
## This package is no longer maintained, as there is now an official BundleTransformer packages that does the same. Please see https://www.nuget.org/packages/BundleTransformer.Autoprefixer

# Autoprefixer for .NET

*This is a .NET adaptation of [Autoprefixer](https://github.com/ai/autoprefixer "Original Autoprefixer library"), a great Javascript library by Andrey Sitnik.*

Parse CSS and add vendor prefixes to CSS rules using values from Can I Use.

Write your CSS rules without vendor prefixes (in fact, forget about them entirely):

```css
:fullscreen a {
    transition: transform 1s
}
```

Process your CSS by Autoprefixer: 

```cs
using (var autoprefixer = new Autoprefixer.Compiler(() => new V8JsEngine()) {
	var prefixed = autoprefixer.Prefix(css, 
	   new BrowserSpecification()
            .BrowserVersionGreaterThan(Browsers.Explorer, 7)
			.WithMinimumUsagePercentage(0.01)
	);
}
```

It will use the data on current browser popularity and properties support to apply prefixes for you:

```css
:-webkit-full-screen a {
    -webkit-transition: -webkit-transform 1s;
    transition: transform 1s
}
:-moz-full-screen a {
    transition: transform 1s
}
:-ms-fullscreen a {
    transition: transform 1s
}
:fullscreen a {
    -webkit-transition: -webkit-transform 1s;
    transition: transform 1s
}
```

## Installation

You can use it independently, or with [BundleTransformer](https://bundletransformer.codeplex.com/). 

Autoprefixer is installed via Nuget.

### Independent installation

```
> Install-Package Autoprefixer
```

Everything you need is in the `Autoprefixer` namespace (see example above). You'll also need to pick a Javascript engine to use, we recommend the `JavaScriptEngineSwitcher.V8` Nuget package.

### BundleTransformer

```
Install-Package BundleTransformer.Autoprefixer.Unofficial
```

This is an unofficial translator for BundleTransformer. To configure it, make the following changes to your web.config (assuming you already have BundleTransformer set up correctly):

```xml
<configuration>
  <configSections>
    <sectionGroup name="bundleTransformer">
      <section name="core" type="BundleTransformer.Core.Configuration.CoreSettings, BundleTransformer.Core"/>
	  <!-- add this line: -->
      <section name="autoprefixer" type="BundleTransformer.Autoprefixer.Configuration.AutoprefixerSettings, BundleTransformer.Autoprefixer"/>
    </sectionGroup>
    <sectionGroup name="jsEngineSwitcher">
      <!-- you probably have this -->
      <section name="core" type="JavaScriptEngineSwitcher.Core.Configuration.CoreConfiguration, JavaScriptEngineSwitcher.Core"/>
    </sectionGroup>
  </configSections>

  <bundleTransformer xmlns="http://tempuri.org/BundleTransformer.Configuration.xsd">
    <core>
      <css>
        <translators>
		  <!-- add this line AFTER any other translators: -->
          <add name="AutoprefixerTranslator" type="BundleTransformer.Autoprefixer.Translators.AutoprefixerTranslator, BundleTransformer.Autoprefixer" enabled="true"/>
        </translators>
      </css>
    </core>
    <autoprefixer cascade="true">
      <!-- pick your javascript engine -->
      <jsEngine name="V8JsEngine"/> 
      <browsers>
        <clear/>
        <!-- write definitions from https://github.com/ai/autoprefixer -->
        <add definition="last 3 version"/>
      </browsers>
    </autoprefixer>
  </bundleTransformer>
```

### Visual cascade

`cascade` determines if you want a visual cascade of vendor-prefixes:

```css
a {
    -webkit-box-sizing: border-box;
       -moz-box-sizing: border-box;
            box-sizing: border-box
}
```

