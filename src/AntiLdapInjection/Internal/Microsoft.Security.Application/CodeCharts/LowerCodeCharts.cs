// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeCharts.cs" company="Microsoft Corporation">
//   Copyright (c) 2008, 2009, 2010 All Rights Reserved, Microsoft Corporation
//
//   This source is subject to the Microsoft Permissive License.
//   Please see the License.txt file for more information.
//   All other rights reserved.
//
//   THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//   KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//   IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//   PARTICULAR PURPOSE.
//
// </copyright>
// <summary>
//   Enumerations for the various printable code tables within the Unicode UTF space.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System;

    /// <summary>
    /// Values for the lowest section of the UTF8 Unicode code tables, from U0000 to U0FFF.
    /// </summary>
    [Flags]
    internal enum LowerCodeCharts : long
    {
        /// <summary>
        /// No code charts from the lower region of the Unicode tables are safe-listed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The Basic Latin code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0000.pdf</remarks>
        BasicLatin = 0x1,

        /// <summary>
        /// The C1 Controls and Latin-1 Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0080.pdf</remarks>
        C1ControlsAndLatin1Supplement = 0x2,

        /// <summary>
        /// The Latin Extended-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0100.pdf</remarks>
        LatinExtendedA = 0x4,

        /// <summary>
        /// The Latin Extended-B code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0180.pdf</remarks>
        LatinExtendedB = 0x8,

        /// <summary>
        /// The IPA Extensions code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0250.pdf</remarks>
        IpaExtensions = 0x10,

        /// <summary>
        /// The Spacing Modifier Letters code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U02B0.pdf</remarks>
        SpacingModifierLetters = 0x20,

        /// <summary>
        /// The Combining Diacritical Marks code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0300.pdf</remarks>
        CombiningDiacriticalMarks = 0x40,

        /// <summary>
        /// The Greek and Coptic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0370.pdf</remarks>
        GreekAndCoptic = 0x80,

        /// <summary>
        /// The Cyrillic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0400.pdf</remarks>
        Cyrillic = 0x100,

        /// <summary>
        /// The Cyrillic Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0500.pdf</remarks>
        CyrillicSupplement = 0x200,

        /// <summary>
        /// The Armenian code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0530.pdf</remarks>
        Armenian = 0x400,

        /// <summary>
        /// The Hebrew code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0590.pdf</remarks>
        Hebrew = 0x800,

        /// <summary>
        /// The Arabic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0600.pdf</remarks>
        Arabic = 0x1000,

        /// <summary>
        /// The Syriac code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0700.pdf</remarks>
        Syriac = 0x2000,

        /// <summary>
        /// The Arabic Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0750.pdf</remarks>
        ArabicSupplement = 0x4000,

        /// <summary>
        /// The Thaana code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0780.pdf</remarks>
        Thaana = 0x8000,

        /// <summary>
        /// The Nko code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U07C0.pdf</remarks>
        Nko = 0x10000,

        /// <summary>
        /// The Samaritan code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0800.pdf</remarks>
        Samaritan = 0x20000,

        /// <summary>
        /// The Devanagari code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0900.pdf</remarks>
        Devanagari = 0x40000,

        /// <summary>
        /// The Bengali code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0980.pdf</remarks>
        Bengali = 0x80000,

        /// <summary>
        /// The Gurmukhi code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0A00.pdf</remarks>
        Gurmukhi = 0x100000,

        /// <summary>
        /// The Gujarati code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0A80.pdf</remarks>
        Gujarati = 0x200000,

        /// <summary>
        /// The Oriya code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0B00.pdf</remarks>
        Oriya = 0x400000,

        /// <summary>
        /// The Tamil code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0B80.pdf</remarks>
        Tamil = 0x800000,

        /// <summary>
        /// The Telugu code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0C00.pdf</remarks>
        Telugu = 0x1000000,

        /// <summary>
        /// The Kannada code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0C80.pdf</remarks>
        Kannada = 0x2000000,

        /// <summary>
        /// The Malayalam code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0D00.pdf</remarks>
        Malayalam = 0x4000000,

        /// <summary>
        /// The Sinhala code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0D80.pdf</remarks>
        Sinhala = 0x8000000,

        /// <summary>
        /// The Thai code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0E00.pdf</remarks>
        Thai = 0x10000000,

        /// <summary>
        /// The Lao code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0E80.pdf</remarks>
        Lao = 0x20000000,

        /// <summary>
        /// The Tibetan code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U0F00.pdf</remarks>
        Tibetan = 0x40000000,

        /// <summary>
        /// The default code tables marked as safe on initialisation.
        /// </summary>
        Default = 0x7F
    }
}
