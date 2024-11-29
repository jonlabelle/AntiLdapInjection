namespace AntiLdapInjection.Tests;

using Xunit;

public class LdapEncoderTests
{
    /// <summary>
    /// Tests that passing a null to filter encoding should return a null.
    /// </summary>
    [Fact]
    public void PassingANullToFilterEncodeShouldReturnANull()
    {
        const string target = null;
        const string expected = null;

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that passing an empty string to filter encoding should return an empty string.
    /// </summary>
    [Fact]
    public void PassingAnEmptyStringToFilterEncodeShouldReturnAnEmptyString()
    {
        var target = string.Empty;
        var expected = string.Empty;

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the encoding of RFC4515 parenthesis example.
    /// </summary>
    [Fact]
    public void FilterEncodingShouldEncodeParenthesis()
    {
        const string target = @"Parens R Us (for all your parenthetical needs)";
        const string expected = @"Parens R Us \28for all your parenthetical needs\29";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the encoding of RFC4515 asterisk example.
    /// </summary>
    [Fact]
    public void FilterEncodingShouldEncodeAsterisks()
    {
        const string target = @"*";
        const string expected = @"\2a";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the encoding of RFC4515 backslash example.
    /// </summary>
    [Fact]
    public void FilterEncodingShouldEncodeBackslashes()
    {
        const string target = @"C:\MyFile";
        const string expected = @"C:\5cMyFile";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the encoding of RFC4515 binary example.
    /// </summary>
    [Fact]
    public void FilterEncodingShouldEncodeBinary()
    {
        var binaryData = new[] { '\u0000', '\u0000', '\u0000', '\u0004' };
        var target = new string(binaryData);
        const string expected = @"\00\00\00\04";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the encoding of RFC4515 accented example.
    /// </summary>
    [Fact]
    public void FilterEncodingShouldEncodeAccentedCharacters()
    {
        const string target = @"Lučić";
        const string expected = @"Lu\c4\8di\c4\87";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the null non-printable character is encoded correctly in filter encoding.
    /// </summary>
    [Fact]
    public void FilterNullCharactersShouldBeHashThenHexEncoded()
    {
        const string target = "\u0000";
        const string expected = @"\00";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the null non-printable character is encoded correctly in DN encoding.
    /// </summary>
    [Fact]
    public void FilterDelCharactersShouldBeHashThenHexEncoded()
    {
        const string target = "\u007f";
        const string expected = @"\7f";

        var actual = LdapEncoder.FilterEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Checks the characters on http://projects.webappsec.org/LDAP-Injection are all escaped during filter encoding.
    /// </summary>
    [Fact]
    public void PassingAnUnsafeCharacterToFilterEncodeShouldNotReturnTheCharacter()
    {
        var targetArray = new[] { "\u0000", "(", ")", "\\", "*", "/" };

        foreach (var target in targetArray)
        {
            var notExpected = target;
            var actual = LdapEncoder.FilterEncode(target);
            Assert.NotEqual(notExpected, actual);
        }
    }

    /// <summary>
    /// Tests that passing a null to DN encoding should return a null.
    /// </summary>
    [Fact]
    public void PassingANullToDistinguishedNameEncodeShouldReturnANull()
    {
        const string target = null;
        const string expected = null;

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that passing an empty string to DN encoding should return an empty string.
    /// </summary>
    [Fact]
    public void PassingAnEmptyStringToDistinguishedNameEncodeShouldReturnAnEmptyString()
    {
        var target = string.Empty;
        var expected = string.Empty;

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that passing the always encoded values to DN encoding returns their slash escaped value.
    /// </summary>
    [Fact]
    public void PassingAnAlwaysEncodedValueToDistinguishedNameEncodeShouldReturnTheirSlashEscapedValue()
    {
        const string target = ",+\"\\<>;";
        const string expected = "\\,\\+\\\"\\\\\\<\\>\\;";

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Checks the characters on http://projects.webappsec.org/LDAP-Injection are all escaped during DN encoding.
    /// </summary>
    [Fact]
    public void PassingAnUnsafeCharacterToDistinguishedNameEncodeShouldNotReturnTheCharacter()
    {
        var targetArray = new[] { "&", "!", "|", "=", "<", ">", ",", "+", "-", "\"", "'", ";" };

        foreach (var target in targetArray)
        {
            var notExpected = target;
            var actual = LdapEncoder.DistinguishedNameEncode(target);
            Assert.NotEqual(notExpected, actual);
        }
    }

    /// <summary>
    /// Test that spaces at the start of a string get escaped properly.
    /// </summary>
    [Fact]
    public void PassingASpaceAtTheBeginningOfAStringToDistinguishedNameEncodeMustEscapeTheSpace()
    {
        const string target = "  abcdef";
        const string expected = "\\  abcdef";

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test that spaces at the end of a string get escaped properly.
    /// </summary>
    [Fact]
    public void PassingASpaceAtTheEndOfAStringToDistinguishedNameEncodeMustEscapeTheSpace()
    {
        const string target = "abcdef  ";
        const string expected = "abcdef \\ ";

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test that a single space input does not trigger both start and finish escaping, but returns a single escaped space.
    /// </summary>
    [Fact]
    public void PassingASingleSpaceStringShouldReturnASingleEscapedString()
    {
        const string target = " ";
        const string expected = "\\ ";

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Test that hashes at the start of a string get escaped properly.
    /// </summary>
    [Fact]
    public void PassingAHashAtTheBeginningOfAStringToDistinguishedNameEncodeMustEscapeTheHash()
    {
        const string target = "##abcdef";
        const string expected = "\\##abcdef";

        var actual = LdapEncoder.DistinguishedNameEncode(target);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that the override of initial character handling works with hashes.
    /// </summary>
    [Fact]
    public void
        PassingAHashAtTheBeginningOfAStringToDistinguishedNameEncodeButOverridingTheInitialRulesDoesNotEncodeTheHash()
    {
        const string target = "##abcdef";
        const string expected = "##abcdef";

        var actual = LdapEncoder.DistinguishedNameEncode(target, false, true);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that the override of initial character handling works with space.
    /// </summary>
    [Fact]
    public void
        PassingASpaceAtTheBeginningOfAStringToDistinguishedNameEncodeButOverridingTheInitialRulesDoesNotEncodeTheSpace()
    {
        const string target = "  abcdef";
        const string expected = "  abcdef";

        var actual = LdapEncoder.DistinguishedNameEncode(target, false, true);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests that the override of final character handling works with hashes.
    /// </summary>
    [Fact]
    public void
        PassingASpaceAtTheEndOfAStringToDistinguishedNameEncodeButOverridingTheFinalRuleDoesNotEncodeTheSpace()
    {
        const string target = "abcdef# ";
        const string expected = "abcdef# ";

        var actual = LdapEncoder.DistinguishedNameEncode(target, true, false);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the null non-printable character is encoded correctly in DN encoding.
    /// </summary>
    [Fact]
    public void DistinguishedNameNullCharactersShouldBeHashThenHexEncoded()
    {
        const string target = "\u0000";
        const string expected = "#00";

        var actual = LdapEncoder.DistinguishedNameEncode(target, true, false);

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Tests the null non-printable character is encoded correctly in DN encoding.
    /// </summary>
    [Fact]
    public void DistinguishedNameDelCharactersShouldBeHashThenHexEncoded()
    {
        const string target = "\u007f";
        const string expected = "#7F";

        var actual = LdapEncoder.DistinguishedNameEncode(target, true, false);

        Assert.Equal(expected, actual);
    }
}
