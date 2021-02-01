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

namespace AntiLdapInjection.Internal.Microsoft.Security.Application.CodeCharts
{
    using System;

    /// <summary>
    /// Values for the middle section of the UTF8 Unicode code tables, from U1F00 to U2DDF
    /// </summary>
    [Flags]
    internal enum MidCodeCharts : long
    {
        /// <summary>
        /// No code charts from the lower region of the Unicode tables are safe-listed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The Greek Extended code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U1F00.pdf</remarks>
        GreekExtended = 0x1,

        /// <summary>
        /// The General Punctuation code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2000.pdf</remarks>
        GeneralPunctuation = 0x2,

        /// <summary>
        /// The Superscripts and Subscripts code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2070.pdf</remarks>
        SuperscriptsAndSubscripts = 0x4,

        /// <summary>
        /// The Currency Symbols code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U20A0.pdf</remarks>
        CurrencySymbols = 0x8,

        /// <summary>
        /// The Combining Diacritical Marks for Symbols code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U20D0.pdf</remarks>
        CombiningDiacriticalMarksForSymbols = 0x10,

        /// <summary>
        /// The Letterlike Symbols code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2100.pdf</remarks>
        LetterlikeSymbols = 0x20,

        /// <summary>
        /// The Number Forms code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2150.pdf</remarks>
        NumberForms = 0x40,

        /// <summary>
        /// The Arrows code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2190.pdf</remarks>
        Arrows = 0x80,

        /// <summary>
        /// The Mathematical Operators code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2200.pdf</remarks>
        MathematicalOperators = 0x100,

        /// <summary>
        /// The Miscellaneous Technical code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2300.pdf</remarks>
        MiscellaneousTechnical = 0x200,

        /// <summary>
        /// The Control Pictures code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2400.pdf</remarks>
        ControlPictures = 0x400,

        /// <summary>
        /// The Optical Character Recognition table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2440.pdf</remarks>
        OpticalCharacterRecognition = 0x800,

        /// <summary>
        /// The Enclosed Alphanumeric code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2460.pdf</remarks>
        EnclosedAlphanumerics = 0x1000,

        /// <summary>
        /// The Box Drawing code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2500.pdf</remarks>
        BoxDrawing = 0x2000,

        /// <summary>
        /// The Block Elements code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2580.pdf</remarks>
        BlockElements = 0x4000,

        /// <summary>
        /// The Geometric Shapes code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U25A0.pdf</remarks>
        GeometricShapes = 0x8000,

        /// <summary>
        /// The Miscellaneous Symbols code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2600.pdf</remarks>
        MiscellaneousSymbols = 0x10000,

        /// <summary>
        /// The Dingbats code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2700.pdf</remarks>
        Dingbats = 0x20000,

        /// <summary>
        /// The Miscellaneous Mathematical Symbols-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U27C0.pdf</remarks>
        MiscellaneousMathematicalSymbolsA = 0x40000,

        /// <summary>
        /// The Supplemental Arrows-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U27F0.pdf</remarks>
        SupplementalArrowsA = 0x80000,

        /// <summary>
        /// The Braille Patterns code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2800.pdf</remarks>
        BraillePatterns = 0x100000,

        /// <summary>
        /// The Supplemental Arrows-B code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2900.pdf</remarks>
        SupplementalArrowsB = 0x200000,

        /// <summary>
        /// The Miscellaneous Mathematical Symbols-B code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2980.pdf</remarks>
        MiscellaneousMathematicalSymbolsB = 0x400000,

        /// <summary>
        /// The Supplemental Mathematical Operators code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2A00.pdf</remarks>
        SupplementalMathematicalOperators = 0x800000,

        /// <summary>
        /// The Miscellaneous Symbols and Arrows code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2B00.pdf</remarks>
        MiscellaneousSymbolsAndArrows = 0x1000000,

        /// <summary>
        /// The Glagolitic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2C00.pdf</remarks>
        Glagolitic = 0x2000000,

        /// <summary>
        /// The Latin Extended-C code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2C60.pdf</remarks>
        LatinExtendedC = 0x4000000,

        /// <summary>
        /// The Coptic code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2C80.pdf</remarks>
        Coptic = 0x8000000,

        /// <summary>
        /// The Georgian Supplement code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2D00.pdf</remarks>
        GeorgianSupplement = 0x10000000,

        /// <summary>
        /// The Tifinagh code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2D30.pdf</remarks>
        Tifinagh = 0x20000000,

        /// <summary>
        /// The Ethiopic Extended code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/U2D80.pdf</remarks>
        EthiopicExtended = 0x4000
    }
}
