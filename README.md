# Anti-LDAP Injection

[![nuget package](https://img.shields.io/nuget/dt/AntiLdapInjection?color=blue)](https://www.nuget.org/packages/AntiLdapInjection "nuget package")
[![nuget version](https://img.shields.io/nuget/v/AntiLdapInjection)](https://www.nuget.org/packages/AntiLdapInjection "nuget version")
[![ci build status](https://github.com/jonlabelle/AntiLdapInjection/actions/workflows/ci.yml/badge.svg)](https://github.com/jonlabelle/AntiLdapInjection/actions/workflows/ci.yml "ci build status")
[![cd release status](https://github.com/jonlabelle/AntiLdapInjection/actions/workflows/cd.yml/badge.svg)](https://github.com/jonlabelle/AntiLdapInjection/actions/workflows/cd.yml "cd release status")

A .NET library that provides protections against [LDAP Injection](https://owasp.org/www-community/attacks/LDAP_Injection), a type of attack that can manipulate LDAP queries to access unauthorized information or perform unauthorized actions.

> [!NOTE]  
> Most of the code was extracted from Microsoft's AntiXss library LDAP Encoder, which is no longer maintained.

## Installation

The latest **AntiLdapInjection** package is available for installation on [NuGet].

### Using dotnet CLI

```bash
dotnet add package AntiLdapInjection
```

### Using NuGet Package Manager

```powershell
Install-Package AntiLdapInjection
```

See [NuGet page] for additional installation options.

## Usage

### FilterEncode

`FilterEncode` encodes input according to [RFC 4515](https://tools.ietf.org/html/rfc4515), where unsafe values are converted to `\XX` \(`XX` is the representation of the unsafe character\).

```csharp
LdapEncoder.FilterEncode(string filterToEncode)
```

#### FilterEncode encoding chart

| Character | Encoded |
| --------- | ------- |
| `(`       | `\28`   |
| `)`       | `\29`   |
| `\`       | `\5c`   |
| `*`       | `\2a`   |
| `/`       | `\2f`   |
| `NUL`     | `\0`    |

#### FilterEncode examples

##### Opening and closing parenthesis

```csharp
string filter = "Parens R Us (for all your parenthetical needs)";
string encoded = LdapEncoder.FilterEncode(filter);

Console.WriteLine(encoded); // "Parens R Us \28for all your parenthetical needs\29"
```

##### Asterisk in search filter

```csharp
string filter = "*";
string encoded = LdapEncoder.FilterEncode(filter);

Console.WriteLine(encoded); // "\2A"
```

##### Backslash in search filter

```csharp
string filter = @"C:\MyFile";
string encoded = LdapEncoder.FilterEncode(filter);

Console.WriteLine(encoded); // "C:\5CMyFile"
```

##### Accents in search filter

```csharp
string filter = "Lučić";
string encoded = LdapEncoder.FilterEncode(filter);

Console.WriteLine(encoded); // "Lu\C4\8Di\C4\87"
```

---

### DistinguishedNameEncode

`DistinguishedNameEncode` encodes input according to [RFC 2253](https://www.ietf.org/rfc/rfc2253.txt),
where unsafe characters are converted to `#XX` where `XX` is the representation
of the unsafe character and the comma, plus, quote, slash, less than and great
than signs are escaped using slash notation (`\X`). In addition to this, a space
or octothorpe (`#`) at the beginning of the input string is escaped (`\`), as is
a space at the end of a string.

```csharp
LdapEncoder.DistinguishedNameEncode(string distinguishedNameToEncode)
```

You have the option to turn off initial or final character escaping rules. For
example, if you are concatenating an escaped distinguished name fragment into the
midst of a complete distinguished name.

```csharp
LdapEncoder.DistinguishedNameEncode(
    string distinguishedNameToEncode,
    bool useInitialCharacterRules,
    bool useFinalCharacterRule
)
```

#### DistinguishedNameEncode encoding chart

| Character | Encoded |
|-----------|---------|
| `&`       | `\&`    |
| `!`       | `\!`    |
| `\|`      | `\\|`   |
| `=`       | `\=`    |
| `<`       | `\<`    |
| `>`       | `\>`    |
| `,`       | `\,`    |
| `+`       | `\+`    |
| `-`       | `\-`    |
| `"`       | `\"`    |
| `'`       | `\'`    |
| `;`       | `\;`    |

#### DistinguishedNameEncode examples

##### Distinguished name slash notation

```csharp
string dn = @", + \ "" \ < >";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);

Console.WriteLine(encoded); // "\, \+ \" \\ \< \>"
```

##### Leading space in distinguished name

```csharp
string dn = " Hello";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);

Console.WriteLine(encoded); // "\ Hello"
```

##### Trailing space in distinguished name

```csharp
string dn = "Hello ";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);

Console.WriteLine(encoded); // "Hello\ "
```

##### Octothorpe character in distinguished name

```csharp
string dn = "#Hello";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);

Console.WriteLine(encoded); // "\#Hello"
```

##### Accents in distinguished name

```csharp
string dn = "Lučić";
string encoded = LdapEncoder.DistinguishedNameEncode(dn);

Console.WriteLine(encoded); // "Lu#C4#8Di#C4#87"
```

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

## Other noteworthy .NET LDAP-related libraries

- **LdapForNet:** Cross platform port of OpenLdap Client library and Windows LDAP to .NET Core
  - [NuGet](https://www.nuget.org/packages/LdapForNet) [GitHub](https://github.com/flamencist/ldap4net)
- **Linq2Ldap:** Wrapper around System.DirectoryServices using LINQ Expressions as LDAP filters
  - [NuGet](https://www.nuget.org/packages/Linq2Ldap) [GitHub](https://github.com/cdibbs/linq2ldap)

[Web Application Security Consortium: LDAP Injection]: http://projects.webappsec.org/w/page/13246947/LDAP%20Injection
[NuGet]: https://www.nuget.org/packages/AntiLdapInjection
[NuGet page]: https://www.nuget.org/packages/AntiLdapInjection
