---
title: LDAP Injection
author: Robert Auger
date: January, 2010
source: http://projects.webappsec.org/w/page/13246947/LDAP%20Injection
notoc: false
---

- **Project:** WASC Threat Classification
- **Threat Type:** Attack
- **Reference ID:** WASC-29
- **Author:** Robert Auger, January, 2010
- **Source:** [Web Application Security Consortium: LDAP Injection]

> *LDAP Injection is an attack technique used to exploit web sites that construct
> LDAP statements from user-supplied input.*

Lightweight Directory Access Protocol (LDAP) is an open-standard protocol for
both querying and manipulating X.500 directory services. The LDAP protocol runs
over Internet transport protocols, such as TCP. Web applications may use
user-supplied input to create custom LDAP statements for dynamic web page
requests.

When a web application fails to properly sanitize user-supplied input, it is
possible for an attacker to alter the construction of an LDAP statement. When an
attacker is able to modify an LDAP statement, the process will run with the same
permissions as the component that executed the command. (e.g. Database server,
Web application server, Web server, etc.).

This can cause serious security problems where the permissions grant the rights
to query, modify or remove anything inside the LDAP tree. The same advanced
exploitation techniques available in SQL Injection can also be similarly applied
in LDAP Injection.

## Example vulnerable code

```csharp
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.DirectoryServices;

public partial class Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName;
        DirectoryEntry entry;

        userName = Request.QueryString["user"];

        if (string.IsNullOrEmpty(userName))
        {
            Response.Write("<b>Invalid request. Please specify valid user name</b></br>");
            Response.End();

            return;
        }

        var searcher = new DirectorySearcher();

        searcher.Filter = "(&(samAccountName=" + userName + "))";

        SearchResultCollection results = searcher.FindAll();

        foreach (SearchResult result in results)
        {
            entry = result.GetDirectoryEntry();

            Response.Write("<p>");
            Response.Write("<b><u>User information for : " + entry.Name + "</u></b><br>");

            foreach (string proName in entry.Properties.PropertyNames)
            {
                Response.Write("<br>Property : " + proName);

                foreach( object val in entry.Properties[proName] )
                {
                    Response.Write("<br>Value: " + val.ToString());
                }
            }

            Response.Write("</p>");
        }
    }
}
```

Looking at the code, we see on line 20 that the userName variable is initialized
with the parameter user and then quickly validated to see if the value is empty
or null. If the value is not empty, the userName is used to initialize the
filter property on line 32. In this scenario, the attacker has complete control
over what will be queried on the LDAP server, and he will get the result of the
query when the code hits line 34 to 53 where all the results and their
attributes are displayed back to the user.

## Attack example

```bash
http://example/default.aspx?user=*
```

In the example above, we send the `*` character in the user parameter which will
result in the filter variable in the code to be initialized with
`(samAccountName=*)`. The resulting LDAP statement will make the server return
any object that contains the `samAccountName` attribute. In addition, the
attacker can specify other attributes to search for and the page will return an
object matching the query.

## Mitigation

The escape sequence for properly using user supplied input into LDAP differs
depending on if the user input is used to create the DN (Distinguished Name) or
used as part of the search filter. The listings below shows the character that
needs to be escape and the appropriate escape method for each case.

The escape sequence for properly using user supplied input into LDAP differs
depending on if the user input is used to create the DN (Distinguished Name) or
used as part of the search filter. The listings below shows the character that
needs to be escape and the appropriate escape method for each case.

### LDAP escape sequences

#### Escaping LDAP Distinguished Names

LDAP Distinguished Name escape sequences start with a leading `\` character.

| Character | Escaped |
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

#### Escaping LDAP Search Filters

LDAP search filters are escaped with an appropriate `{\ASCII}` code.

| Character | Escaped |
|-----------|---------|
| `(`       | `{\28}` |
| `)`       | `{\29}` |
| `\`       | `{\5c}` |
| `*`       | `{\2a}` |
| `/`       | `{\2f}` |
| `NUL`     | `{\0}`  |

## Example safe code

The code below implements the escape logic for both DN and Filter case. Use
`CanonicalizeStringForLdapFilter()` to escape when the input is used to create
the filter and `CanonicalizeStringForLdapDN()` for DN. In addition, both
`IsUserGivenStringPluggableIntoLdapSearchFilter` and
`IsUserGivenStringPluggableIntoLdapDN` can be used to detect the presence of
restricted characters.

```csharp
using System;
using System.Collections.Generic;
using System.Text;

public static class LdapCanonicaliztion
{
    /// <summary>
    /// Characters that must be escaped in an LDAP filter path
    /// WARNING: Always keep '\\' at the very beginning to avoid recursive replacements
    /// </summary>
    private static char[] ldapFilterEscapeSequence = new char[]
    {
        '\\', '*',  '(',
         ')', '\0', '/'
    };

    /// <summary>
    /// Mapping strings of the LDAP filter escape sequence characters
    /// </summary>
    private static string[] ldapFilterEscapeSequenceCharacter = new string[]
    {
        "\\5c", "\\2a", "\\28",
        "\\29", "\\00", "\\2f"
    };

    /// <summary>
    /// Characters that must be escaped in an LDAP DN path
    /// </summary>
    private static char[] ldapDnEscapeSequence = new char[]
    {
        '\\', ',', '+',
        '"',  '<', '>',
        ';'
    };

    /// <summary>
    /// Canonicalize a ldap filter string by inserting LDAP escape sequences.
    /// </summary>
    /// <param name="userInput">User input string to canonicalize</param>
    /// <returns>Canonicalized user input so it can be used in LDAP filter</returns>
    public static string CanonicalizeStringForLdapFilter(string userInput)
    {
        if (String.IsNullOrEmpty(userInput))
        {
            return userInput;
        }

        string name = (string)userInput.Clone();

        for (int charIndex = 0; charIndex < ldapFilterEscapeSequence.Length; ++charIndex)
        {
            int index = name.IndexOf(ldapFilterEscapeSequence[charIndex]);
            if (index != -1)
            {
                name = name.Replace(
                    new String(ldapFilterEscapeSequence[charIndex], 1), ldapFilterEscapeSequenceCharacter[charIndex]
                );
            }
        }

        return name;
    }

    /// <summary>
    /// Canonicalize a LDAP DN string by inserting LDAP escape sequences.
    /// </summary>
    /// <param name="userInput">User input string to canonicalize</param>
    /// <returns>Canonicalized user input so it can be used in LDAP filter</returns>
    public static string CanonicalizeStringForLdapDN(string userInput)
    {
        if (String.IsNullOrEmpty(userInput))
        {
            return userInput;
        }

        string name = (string)userInput.Clone();

        for (int charIndex = 0; charIndex < ldapDnEscapeSequence.Length; ++charIndex)
        {
            int index = name.IndexOf(ldapDnEscapeSequence[charIndex]);
            if (index != -1)
            {
                name = name.Replace(
                    new string(ldapDnEscapeSequence[charIndex], 1), @"\" + ldapDnEscapeSequence[charIndex]
                );
            }
        }

        return name;
    }

    /// <summary>
    /// Ensure that a user provided string can be plugged into an LDAP search filter
    /// such that there is no risk of an LDAP injection attack.
    /// </summary>
    /// <param name="userInput">String value to check.</param>
    /// <returns>True if value is valid or null, false otherwise.</returns>
    public static bool IsUserGivenStringPluggableIntoLdapSearchFilter(string userInput)
    {
        if (string.IsNullOrEmpty(userInput))
        {
            return true;
        }

        if (userInput.IndexOfAny(ldapDnEscapeSequence) != -1)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Ensure that a user provided string can be plugged into an LDAP DN
    /// such that there is no risk of an LDAP injection attack.
    /// </summary>
    /// <param name="userInput">String value to check.</param>
    /// <returns>True if value is valid or null, false otherwise.</returns>
    public static bool IsUserGivenStringPluggableIntoLdapDN(string userInput)
    {
        if (string.IsNullOrEmpty(userInput))
        {
            return true;
        }

        if (userInput.IndexOfAny(ldapFilterEscapeSequence) != -1)
        {
            return false;
        }

        return true;
    }
}
```

[Web Application Security Consortium: LDAP Injection]: http://projects.webappsec.org/w/page/13246947/LDAP%20Injection
