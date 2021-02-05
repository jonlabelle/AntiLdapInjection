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
    /// Values for the upper section of the UTF8 Unicode code tables, from UA8E0 to UFFFD.
    /// </summary>
    [Flags]
    internal enum UpperCodeCharts
    {
        /// <summary>
        /// No code charts from the upper region of the Unicode tables are safe-listed.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The Devanagari Extended code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA8E0.pdf</remarks>
        DevanagariExtended = 0x1,

        /// <summary>
        /// The Kayah Li code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA900.pdf</remarks>
        KayahLi = 0x2,

        /// <summary>
        /// The Rejang code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA930.pdf</remarks>
        Rejang = 0x4,

        /// <summary>
        /// The Hangul Jamo Extended-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA960.pdf</remarks>
        HangulJamoExtendedA = 0x8,

        /// <summary>
        /// The Javanese code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UA980.pdf</remarks>
        Javanese = 0x10,

        /// <summary>
        /// The Cham code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UAA00.pdf</remarks>
        Cham = 0x20,

        /// <summary>
        /// The Myanmar Extended-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UAA60.pdf</remarks>
        MyanmarExtendedA = 0x40,

        /// <summary>
        /// The Tai Viet code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UAA80.pdf</remarks>
        TaiViet = 0x80,

        /// <summary>
        /// The Meetei Mayek code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UABC0.pdf</remarks>
        MeeteiMayek = 0x100,

        /// <summary>
        /// The Hangul Syllables code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UAC00.pdf</remarks>
        HangulSyllables = 0x200,

        /// <summary>
        /// The Hangul Jamo Extended-B code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UD7B0.pdf</remarks>
        HangulJamoExtendedB = 0x400,

        /// <summary>
        /// The CJK Compatibility Ideographs code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UF900.pdf</remarks>
        CjkCompatibilityIdeographs = 0x800,

        /// <summary>
        /// The Alphabetic Presentation Forms code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFB00.pdf</remarks>
        AlphabeticPresentationForms = 0x1000,

        /// <summary>
        /// The Arabic Presentation Forms-A code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFB50.pdf</remarks>
        ArabicPresentationFormsA = 0x2000,

        /// <summary>
        /// The Variation Selectors code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFE00.pdf</remarks>
        VariationSelectors = 0x4000,

        /// <summary>
        /// The Vertical Forms code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFE10.pdf</remarks>
        VerticalForms = 0x8000,

        /// <summary>
        /// The Combining Half Marks code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFE20.pdf</remarks>
        CombiningHalfMarks = 0x10000,

        /// <summary>
        /// The CJK Compatibility Forms code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFE30.pdf</remarks>
        CjkCompatibilityForms = 0x20000,

        /// <summary>
        /// The Small Form Variants code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFE50.pdf</remarks>
        SmallFormVariants = 0x40000,

        /// <summary>
        /// The Arabic Presentation Forms-B code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFE70.pdf</remarks>
        ArabicPresentationFormsB = 0x80000,

        /// <summary>
        /// The half width and full width Forms code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFF00.pdf</remarks>
        HalfWidthAndFullWidthForms = 0x100000,

        /// <summary>
        /// The Specials code table.
        /// </summary>
        /// <remarks>http://www.unicode.org/charts/PDF/UFFF0.pdf</remarks>
        Specials = 0x200000
    }
}
