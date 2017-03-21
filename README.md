# Configgy.Microsoft.Extensions.Configuration

[Configgy](https://github.com/bungeemonkee/Configgy) extensions to incorporate [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/) configuration mechanisms

## Version Numbering

Each version will have two numbers: a Windows file version and a SemVer version. The SemVer version will be derived from the Windows file version.

Versioning will follow this pattern:

* Major Version : Increased when there is a release that breaks a public api
* Minor Version : Increased for each non-api-breaking release
* Build Number : 0 for alpha versions, 1 for beta versions, 2 for release candidates, 3 for releases
* Revision : Always 0 for release versions, always 1+ for alpha, beta, rc versions to indicate the alpha/beta/rc  number (alpha/beeta/rc numbers should always be two-digits)

So the following versions are equalivalent

* 1.0.0.1 => 1.0.0-alpha01
* 1.0.1.1 => 1.0.1-beta01
* 1.0.2.1 => 1.0.2-rc01
* 1.0.3.0 => 1.0.3
