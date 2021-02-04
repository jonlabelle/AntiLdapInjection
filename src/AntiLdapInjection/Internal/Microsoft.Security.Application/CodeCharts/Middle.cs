// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Middle.cs" company="Microsoft Corporation">
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
//   Provides safe character positions for the lower middle section of the UTF code tables.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System.Collections;

    /// <summary>
    /// Provides safe character positions for the middle section of the UTF code tables.
    /// </summary>
    internal static class Middle
    {
        /// <summary>
        /// Determines if the specified flag is set.
        /// </summary>
        /// <param name="flags">The value to check.</param>
        /// <param name="flagToCheck">The flag to check for.</param>
        /// <returns>true if the flag is set, otherwise false.</returns>
        internal static bool IsFlagSet(MidCodeCharts flags, MidCodeCharts flagToCheck)
        {
            return (flags & flagToCheck) != 0;
        }

        /// <summary>
        /// Provides the safe characters for the Greek Extended code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable GreekExtended()
        {
            return CodeChartHelper.GetRange(7936, 8190,
                (int i) =>
                    i == 7958 || i == 7959 || i == 7966 || i == 7967 || i == 8006 || i == 8007 || i == 8014 ||
                    i == 8015 || i == 8024 || i == 8026 || i == 8028 || i == 8030 || i == 8062 || i == 8063 ||
                    i == 8117 || i == 8133 || i == 8148 || i == 8149 || i == 8156 || i == 8176 || i == 8177 ||
                    i == 8181);
        }

        /// <summary>
        /// Provides the safe characters for the General Punctuation code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable GeneralPunctuation()
        {
            return CodeChartHelper.GetRange(8192, 8303, (int i) => i >= 8293 && i <= 8297);
        }

        /// <summary>
        /// Provides the safe characters for the Superscripts and subscripts code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SuperscriptsAndSubscripts()
        {
            return CodeChartHelper.GetRange(8304, 8340, (int i) => i == 8306 || i == 8307 || i == 8335);
        }

        /// <summary>
        /// Provides the safe characters for the Currency Symbols code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CurrencySymbols()
        {
            return CodeChartHelper.GetRange(8352, 8376);
        }

        /// <summary>
        /// Provides the safe characters for the Combining Diacritrical Marks for Symbols code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CombiningDiacriticalMarksForSymbols()
        {
            return CodeChartHelper.GetRange(8400, 8432);
        }

        /// <summary>
        /// Provides the safe characters for the Letterlike Symbols code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable LetterlikeSymbols()
        {
            return CodeChartHelper.GetRange(8448, 8527);
        }

        /// <summary>
        /// Provides the safe characters for the Number Forms code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable NumberForms()
        {
            return CodeChartHelper.GetRange(8528, 8585);
        }

        /// <summary>
        /// Provides the safe characters for the Arrows code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Arrows()
        {
            return CodeChartHelper.GetRange(8592, 8703);
        }

        /// <summary>
        /// Provides the safe characters for the Mathematical Operators code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable MathematicalOperators()
        {
            return CodeChartHelper.GetRange(8704, 8959);
        }

        /// <summary>
        /// Provides the safe characters for the Miscellaneous Technical code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable MiscellaneousTechnical()
        {
            return CodeChartHelper.GetRange(8960, 9192);
        }

        /// <summary>
        /// Provides the safe characters for the Control Pictures code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable ControlPictures()
        {
            return CodeChartHelper.GetRange(9216, 9254);
        }

        /// <summary>
        /// Provides the safe characters for the OCR code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable OpticalCharacterRecognition()
        {
            return CodeChartHelper.GetRange(9280, 9290);
        }

        /// <summary>
        /// Provides the safe characters for the Enclosed Alphanumerics code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable EnclosedAlphanumerics()
        {
            return CodeChartHelper.GetRange(9312, 9471);
        }

        /// <summary>
        /// Provides the safe characters for the Box Drawing code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable BoxDrawing()
        {
            return CodeChartHelper.GetRange(9472, 9599);
        }

        /// <summary>
        /// Provides the safe characters for the Block Elements code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable BlockElements()
        {
            return CodeChartHelper.GetRange(9600, 9631);
        }

        /// <summary>
        /// Provides the safe characters for the Geometric Shapes code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable GeometricShapes()
        {
            return CodeChartHelper.GetRange(9632, 9727);
        }

        /// <summary>
        /// Provides the safe characters for the Miscellaneous Symbols code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable MiscellaneousSymbols()
        {
            return CodeChartHelper.GetRange(9728, 9983, (int i) => i == 9934 || i == 9954 || (i >= 9956 && i <= 9959));
        }

        /// <summary>
        /// Provides the safe characters for the Dingbats code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Dingbats()
        {
            return CodeChartHelper.GetRange(9985, 10174,
                (int i) => i == 9989 || i == 9994 || i == 9995 || i == 10024 || i == 10060 || i == 10062 ||
                           i == 10067 || i == 10068 || i == 10069 || i == 10079 || i == 10080 || i == 10133 ||
                           i == 10134 || i == 10135 || i == 10160);
        }

        /// <summary>
        /// Provides the safe characters for the Miscellaneous Mathematical Symbols A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable MiscellaneousMathematicalSymbolsA()
        {
            return CodeChartHelper.GetRange(10176, 10223,
                (int i) => i == 10187 || i == 10189 || i == 10190 || i == 10191);
        }

        /// <summary>
        /// Provides the safe characters for the Supplemental Arrows A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SupplementalArrowsA()
        {
            return CodeChartHelper.GetRange(10224, 10239);
        }

        /// <summary>
        /// Provides the safe characters for the Braille Patterns code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable BraillePatterns()
        {
            return CodeChartHelper.GetRange(10240, 10495);
        }

        /// <summary>
        /// Provides the safe characters for the Supplemental Arrows B code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SupplementalArrowsB()
        {
            return CodeChartHelper.GetRange(10496, 10623);
        }

        /// <summary>
        /// Provides the safe characters for the Miscellaneous Mathematical Symbols B code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable MiscellaneousMathematicalSymbolsB()
        {
            return CodeChartHelper.GetRange(10624, 10751);
        }

        /// <summary>
        /// Provides the safe characters for the Supplemental Mathematical Operators code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SupplementalMathematicalOperators()
        {
            return CodeChartHelper.GetRange(10752, 11007);
        }

        /// <summary>
        /// Provides the safe characters for the Miscellaneous Symbols and Arrows code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable MiscellaneousSymbolsAndArrows()
        {
            return CodeChartHelper.GetRange(11008, 11097, (int i) => i == 11085 || i == 11086 || i == 11087);
        }

        /// <summary>
        /// Provides the safe characters for the Glagolitic code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Glagolitic()
        {
            return CodeChartHelper.GetRange(11264, 11358, (int i) => i == 11311);
        }

        /// <summary>
        /// Provides the safe characters for the Latin Extended C code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable LatinExtendedC()
        {
            return CodeChartHelper.GetRange(11360, 11391);
        }

        /// <summary>
        /// Provides the safe characters for the Coptic table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Coptic()
        {
            return CodeChartHelper.GetRange(11392, 11519, (int i) => i >= 11506 && i <= 11512);
        }

        /// <summary>
        /// Provides the safe characters for the Georgian Supplement code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable GeorgianSupplement()
        {
            return CodeChartHelper.GetRange(11520, 11557);
        }

        /// <summary>
        /// Provides the safe characters for the Tifinagh code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Tifinagh()
        {
            return CodeChartHelper.GetRange(11568, 11631, (int i) => i >= 11622 && i <= 11630);
        }

        /// <summary>
        /// Provides the safe characters for the Ethiopic Extended code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable EthiopicExtended()
        {
            return CodeChartHelper.GetRange(11648, 11742,
                (int i) => (i >= 11671 && i <= 11679) ||
                           i == 11687 || i == 11695 || i == 11703 || i == 11711 ||
                           i == 11719 || i == 11727 || i == 11735 || i == 11743);
        }
    }
}
