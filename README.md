# Anti-LDAP Injection

[![CI build status](https://github.com/jonlabelle/AntiLdapInjection/workflows/ci/badge.svg)](https://github.com/jonlabelle/AntiLdapInjection/actions?query=workflow%3Aci "CI build status")
[![CD release status](https://github.com/jonlabelle/AntiLdapInjection/workflows/cd/badge.svg)](https://github.com/jonlabelle/AntiLdapInjection/actions?query=workflow%3Acd "CD release status")
[![Code Quality Analysis status](https://github.com/jonlabelle/AntiLdapInjection/workflows/code-quality-analysis/badge.svg)](https://github.com/jonlabelle/AntiLdapInjection/actions?query=workflow%3Acode-quality-analysis "Code Quality Analysis status")
[![Latest NuGet release](https://img.shields.io/nuget/vpre/AntiLdapInjection?color=blue&label=nuget&logo=nuget)](https://www.nuget.org/packages/AntiLdapInjection "Latest NuGet release")
[![Total NuGet downloads](https://img.shields.io/nuget/dt/AntiLdapInjection?color=blue&label=downloads&logo=nuget)](https://www.nuget.org/stats/packages/AntiLdapInjection?groupby=Version&groupby=ClientName&groupby=ClientVersion "Total NuGet downloads")

A .NET library that provides protection against LDAP Injection.

> Most of the of the code was extracted from Microsoft's AntiXss library (v4.3)
> LDAP Encoder, which is no longer maintained.

## Installation

The latest AntiLdapInjection package is available for installation on [NuGet](https://www.nuget.org/packages/AntiLdapInjection). Use any of the following client tool commands listed below to install the package.

- [.NET CLI](#install-package-using-dotnet-cli)
- [NuGet Package Manager](#install-package-using-nuget-package-manager)
- [PackageReference](#install-package-using-packagereference)

### Install package using dotnet CLI

```bash
dotnet add package AntiLdapInjection
```

### Install package using NuGet Package Manager

```powershell
Install-Package AntiLdapInjection
```

### Install package using PackageReference

For projects that support [PackageReference](https://docs.microsoft.com/nuget/consume-packages/package-references-in-project-files), copy this XML node into the project file to reference the package.

```xml
<PackageReference Include="AntiLdapInjection" Version="x.x.x" />
```

> Be sure to replace `x.x.x` with an appropriate **Version**.

## Usage

### FilterEncode

`FilterEncode` encodes input according to [RFC 4515](https://tools.ietf.org/html/rfc4515),
where unsafe values are converted to `\XX` \(`XX` is the representation of the
unsafe character\).

#### Example: opening/closing parenthesis

```csharp
string filter = "Parens R Us (for all your parenthetical needs)";
string encoded = LdapEncoder.FilterEncode(filter);
Console.WriteLine(encoded); // "Parens R Us \28for all your parenthetical needs\29"
```

#### Example: asterisk in search filter

```csharp
string filter = "*";
string encoded = LdapEncoder.FilterEncode(filter);
Console.WriteLine(encoded); // "\2A"
```

#### Example: backslash in search filter

```csharp
string filter = @"C:\MyFile";
string encoded = LdapEncoder.FilterEncode(filter);
Console.WriteLine(encoded); // "C:\5CMyFile"
```

#### Example: accents in search filter

```csharp
string filter = "Lučić";
string encoded = LdapEncoder.FilterEncode(filter);
Console.WriteLine(encoded); // "Lu\C4\8Di\C4\87"
```

### DistinguishedNameEncode

`DistinguishedNameEncode` encodes input according to [RFC 2253](https://www.ietf.org/rfc/rfc2253.txt),
where unsafe characters are converted to `#XX` where `XX` is the representation
of the unsafe character and the comma, plus, quote, slash, less than and great
than signs are escaped using slash notation (`\X`). In addition to this, a space
or octothorpe (`#`) at the beginning of the input string is escaped (`\`), as is
a space at the end of a string.

#### Example: distinguished name slash notation

```csharp
string dn = @", + \ "" \ < >";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);
Console.WriteLine(encoded); // "\, \+ \" \\ \< \>"
```

#### Example: leading space in distinguished name

```csharp
string dn = " Hello";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);
Console.WriteLine(encoded); // "\ Hello"
```

#### Example: trailing space in distinguished name

```csharp
string dn = "Hello ";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);
Console.WriteLine(encoded); // "Hello\ "
```

#### Example: octothorpe character in distinguished name

```csharp
string dn = "#Hello";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);
Console.WriteLine(encoded); // "\#Hello"
```

#### Example: accents in distinguished name

```csharp
string dn = "Lučić";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);
Console.WriteLine(encoded); // "Lu#C4#8Di#C4#87"
```

#### Initial and final character overrides

You have the option to turn off initial or final character escaping rules. For
example, if you are concatenating a escaped distinguished name fragment into the
midst of a complete distinguished name.

```csharp
DistinguishedNameEncode( string input,  bool useInitialCharacterRules,  bool useFinalCharacterRule)
```

In addition to the RFC mandated escaping, the safe list excludes the characters
listed under the [LDAP escape sequences](#ldap-escape-sequences) section.

## LDAP injection resources

- [OWASP: LDAP Injection Prevention Cheat Sheet](https://www.owasp.org/index.php/LDAP_injection)
- [OWASP: Testing for LDAP Injection](https://owasp.org/www-project-web-security-testing-guide/stable/4-Web_Application_Security_Testing/07-Input_Validation_Testing/06-Testing_for_LDAP_Injection.html)
- [Microsoft TechNet: Active Directory Characters to Escape](https://social.technet.microsoft.com/wiki/contents/articles/5312.active-directory-characters-to-escape.aspx)
- [Web Application Security Consortium: LDAP Injection]
- [Black Hat: PDF Whitepaper on LDAP Injection and Blind LDAP Injection](https://www.blackhat.com/presentations/bh-europe-08/Alonso-Parada/Whitepaper/bh-eu-08-alonso-parada-WP.pdf)
- [RFC-1960: A String Representation of LDAP Search Filters](https://www.ietf.org/rfc/rfc1960.html)
- [IBM Redbooks: Understanding LDAP - Design and Implementation](https://www.redbooks.ibm.com/abstracts/sg244986.html)
- [CWE: Improper Neutralization of Special Elements used in an LDAP Query \(LDAP Injection\)](https://cwe.mitre.org/data/definitions/90.html)

## Similar libraries

Similar libraries providing protections against LDAP injection, not necessarily
in .NET.

### Node.js

#### ldap-escape

[ldap-escape](https://github.com/tcort/ldap-escape "ldap-escape npm page")
is an [npm package](https://www.npmjs.com/package/ldap-escape) that provides
template literal tag functions for LDAP filters and distinguished names to
prevent LDAP injection attacks.

[Web Application Security Consortium: LDAP Injection]: http://projects.webappsec.org/w/page/13246947/LDAP%20Injection
