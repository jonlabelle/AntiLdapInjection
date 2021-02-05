// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeList.cs" company="Microsoft Corporation">
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
//   Provides safe list utility functions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System.Collections;
    using System.Globalization;

    internal static class SafeList
    {
        /// <summary>
        /// Generates a safe character array representing the specified value.
        /// </summary>
        /// <returns>A safe character array representing the specified value.</returns>
        /// <param name="value">The value to generate a safe representation for.</param>
        internal delegate char[] GenerateSafeValue(int value);

        /// <summary>
        /// Generates a new safe list of the specified size, using the specified function to produce safe values.
        /// </summary>
        /// <param name="length">The length of the safe list to generate.</param>
        /// <param name="generateSafeValue">The <see cref="GenerateSafeValue" /> function to use.</param>
        /// <returns>A new safe list.</returns>
        internal static char[][] Generate(int length, GenerateSafeValue generateSafeValue)
        {
            var array = new char[length + 1][];

            for (var i = 0; i <= length; i++)
            {
                array[i] = generateSafeValue(i);
            }

            return array;
        }

        /// <summary>
        /// Marks characters from the specified languages as safe.
        /// </summary>
        /// <param name="safeList">The safe list to punch holes in.</param>
        /// <param name="lowerCodeCharts">The combination of lower code charts to use.</param>
        /// <param name="lowerMidCodeCharts">The combination of lower mid code charts to use.</param>
        /// <param name="midCodeCharts">The combination of mid code charts to use.</param>
        /// <param name="upperMidCodeCharts">The combination of upper mid code charts to use.</param>
        /// <param name="upperCodeCharts">The combination of upper code charts to use.</param>
        internal static void PunchUnicodeThrough(
            ref char[][] safeList,
            LowerCodeCharts lowerCodeCharts,
            LowerMidCodeCharts lowerMidCodeCharts,
            MidCodeCharts midCodeCharts,
            UpperMidCodeCharts upperMidCodeCharts,
            UpperCodeCharts upperCodeCharts
        )
        {
            if (lowerCodeCharts != LowerCodeCharts.None)
            {
                PunchCodeCharts(ref safeList, lowerCodeCharts);
            }

            if (lowerMidCodeCharts != LowerMidCodeCharts.None)
            {
                PunchCodeCharts(ref safeList, lowerMidCodeCharts);
            }

            if (midCodeCharts != MidCodeCharts.None)
            {
                PunchCodeCharts(ref safeList, midCodeCharts);
            }

            if (upperMidCodeCharts != UpperMidCodeCharts.None)
            {
                PunchCodeCharts(ref safeList, upperMidCodeCharts);
            }

            if (upperCodeCharts != 0)
            {
                PunchCodeCharts(ref safeList, upperCodeCharts);
            }
        }

        /// <summary>
        /// Punches holes as necessary.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="whiteListedCharacters">The list of character positions to punch.</param>
        internal static void PunchSafeList(ref char[][] safeList, IEnumerable whiteListedCharacters)
        {
            PunchHolesIfNeeded(ref safeList, needed: true, whiteListedCharacters);
        }

        /// <summary>
        /// Generates a hash prefixed character array representing the specified value.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>#1</description></item>
        /// <item><term>10</term><description>#10</description></item>
        /// <item><term>100</term><description>#100</description></item>
        /// </list>
        /// </remarks>
        internal static char[] HashThenValueGenerator(int value)
        {
            return StringToCharArrayWithHashPrefix(value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Generates a hash prefixed character array representing the specified value in hexadecimal.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>#1</description></item>
        /// <item><term>10</term><description>#0a</description></item>
        /// <item><term>100</term><description>#64</description></item>
        /// </list>
        /// </remarks>
        internal static char[] HashThenHexValueGenerator(int value)
        {
            return StringToCharArrayWithHashPrefix(value.ToString("X2", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Generates a percent prefixed character array representing the specified value in hexadecimal.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>%01</description></item>
        /// <item><term>10</term><description>%0a</description></item>
        /// <item><term>100</term><description>%64</description></item>
        /// </list>
        /// </remarks>
        internal static char[] PercentThenHexValueGenerator(int value)
        {
            return StringToCharArrayWithPercentPrefix(value.ToString("x2", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Generates a slash prefixed character array representing the specified value in hexadecimal.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>\01</description></item>
        /// <item><term>10</term><description>\0a</description></item>
        /// <item><term>100</term><description>\64</description></item>
        /// </list>
        /// </remarks>
        internal static char[] SlashThenHexValueGenerator(int value)
        {
            return StringToCharArrayWithSlashPrefix(value.ToString("x2", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Generates a hash prefixed character array from the specified string.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>#1</description></item>
        /// <item><term>10</term><description>#10</description></item>
        /// <item><term>100</term><description>#100</description></item>
        /// </list>
        /// </remarks>
        private static char[] StringToCharArrayWithHashPrefix(string value)
        {
            return StringToCharArrayWithPrefix(value, '#');
        }

        /// <summary>
        /// Generates a percent prefixed character array from the specified string.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>%1</description></item>
        /// <item><term>10</term><description>%10</description></item>
        /// <item><term>100</term><description>%100</description></item>
        /// </list>
        /// </remarks>
        private static char[] StringToCharArrayWithPercentPrefix(string value)
        {
            return StringToCharArrayWithPrefix(value, '%');
        }

        /// <summary>
        /// Generates a slash prefixed character array from the specified string.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <returns>A character array representing the specified value.</returns>
        /// <remarks>
        /// Example inputs and encoded outputs:
        /// <list type="table">
        /// <item><term>1</term><description>\1</description></item>
        /// <item><term>10</term><description>\10</description></item>
        /// <item><term>100</term><description>\100</description></item>
        /// </list>
        /// </remarks>
        private static char[] StringToCharArrayWithSlashPrefix(string value)
        {
            return StringToCharArrayWithPrefix(value, '\\');
        }

        /// <summary>
        /// Generates a prefixed character array from the specified string and prefix.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="prefix">The prefix to use.</param>
        /// <returns>A prefixed character array representing the specified value.</returns>
        private static char[] StringToCharArrayWithPrefix(string value, char prefix)
        {
            var length = value.Length;
            var array = new char[length + 1];

            array[0] = prefix;

            for (var i = 0; i < length; i++)
            {
                array[i + 1] = value[i];
            }

            return array;
        }

        /// <summary>
        /// Punch appropriate holes for the selected code charts.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="codeCharts">The code charts to punch.</param>
        private static void PunchCodeCharts(ref char[][] safeList, LowerCodeCharts codeCharts)
        {
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.BasicLatin), Lower.BasicLatin());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.C1ControlsAndLatin1Supplement), Lower.Latin1Supplement());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.LatinExtendedA), Lower.LatinExtendedA());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.LatinExtendedB), Lower.LatinExtendedB());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.IpaExtensions), Lower.IpaExtensions());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.SpacingModifierLetters), Lower.SpacingModifierLetters());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.CombiningDiacriticalMarks), Lower.CombiningDiacriticalMarks());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.GreekAndCoptic), Lower.GreekAndCoptic());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Cyrillic), Lower.Cyrillic());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.CyrillicSupplement), Lower.CyrillicSupplement());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Armenian), Lower.Armenian());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Hebrew), Lower.Hebrew());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Arabic), Lower.Arabic());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Syriac), Lower.Syriac());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.ArabicSupplement), Lower.ArabicSupplement());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Thaana), Lower.Thaana());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Nko), Lower.Nko());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Samaritan), Lower.Samaritan());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Devanagari), Lower.Devanagari());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Bengali), Lower.Bengali());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Gurmukhi), Lower.Gurmukhi());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Gujarati), Lower.Gujarati());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Oriya), Lower.Oriya());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Tamil), Lower.Tamil());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Telugu), Lower.Telugu());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Kannada), Lower.Kannada());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Malayalam), Lower.Malayalam());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Sinhala), Lower.Sinhala());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Thai), Lower.Thai());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Lao), Lower.Lao());
            PunchHolesIfNeeded(ref safeList, Lower.IsFlagSet(codeCharts, LowerCodeCharts.Tibetan), Lower.Tibetan());
        }

        /// <summary>
        /// Punch appropriate holes for the selected code charts.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="codeCharts">The code charts to punch.</param>
        private static void PunchCodeCharts(ref char[][] safeList, LowerMidCodeCharts codeCharts)
        {
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Myanmar), LowerMiddle.Myanmar());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Georgian), LowerMiddle.Georgian());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.HangulJamo), LowerMiddle.HangulJamo());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Ethiopic), LowerMiddle.Ethiopic());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.EthiopicSupplement), LowerMiddle.EthiopicSupplement());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Cherokee), LowerMiddle.Cherokee());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.UnifiedCanadianAboriginalSyllabics), LowerMiddle.UnifiedCanadianAboriginalSyllabics());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Ogham), LowerMiddle.Ogham());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Runic), LowerMiddle.Runic());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Tagalog), LowerMiddle.Tagalog());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Hanunoo), LowerMiddle.Hanunoo());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Buhid), LowerMiddle.Buhid());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Tagbanwa), LowerMiddle.Tagbanwa());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Khmer), LowerMiddle.Khmer());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Mongolian), LowerMiddle.Mongolian());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.UnifiedCanadianAboriginalSyllabicsExtended), LowerMiddle.UnifiedCanadianAboriginalSyllabicsExtended());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Limbu), LowerMiddle.Limbu());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.TaiLe), LowerMiddle.TaiLe());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.NewTaiLue), LowerMiddle.NewTaiLue());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.KhmerSymbols), LowerMiddle.KhmerSymbols());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Buginese), LowerMiddle.Buginese());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.TaiTham), LowerMiddle.TaiTham());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Balinese), LowerMiddle.Balinese());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Sudanese), LowerMiddle.Sudanese());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.Lepcha), LowerMiddle.Lepcha());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.OlChiki), LowerMiddle.OlChiki());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.VedicExtensions), LowerMiddle.VedicExtensions());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.PhoneticExtensions), LowerMiddle.PhoneticExtensions());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.PhoneticExtensionsSupplement), LowerMiddle.PhoneticExtensionsSupplement());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.CombiningDiacriticalMarksSupplement), LowerMiddle.CombiningDiacriticalMarksSupplement());
            PunchHolesIfNeeded(ref safeList, LowerMiddle.IsFlagSet(codeCharts, LowerMidCodeCharts.LatinExtendedAdditional), LowerMiddle.LatinExtendedAdditional());
        }

        /// <summary>
        /// Punch appropriate holes for the selected code charts.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="codeCharts">The code charts to punch.</param>
        private static void PunchCodeCharts(ref char[][] safeList, MidCodeCharts codeCharts)
        {
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.GreekExtended), Middle.GreekExtended());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.GeneralPunctuation), Middle.GeneralPunctuation());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.SuperscriptsAndSubscripts), Middle.SuperscriptsAndSubscripts());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.CurrencySymbols), Middle.CurrencySymbols());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.CombiningDiacriticalMarksForSymbols), Middle.CombiningDiacriticalMarksForSymbols());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.LetterlikeSymbols), Middle.LetterlikeSymbols());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.NumberForms), Middle.NumberForms());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.Arrows), Middle.Arrows());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.MathematicalOperators), Middle.MathematicalOperators());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.MiscellaneousTechnical), Middle.MiscellaneousTechnical());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.ControlPictures), Middle.ControlPictures());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.OpticalCharacterRecognition), Middle.OpticalCharacterRecognition());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.EnclosedAlphanumerics), Middle.EnclosedAlphanumerics());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.BoxDrawing), Middle.BoxDrawing());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.BlockElements), Middle.BlockElements());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.GeometricShapes), Middle.GeometricShapes());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.MiscellaneousSymbols), Middle.MiscellaneousSymbols());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.Dingbats), Middle.Dingbats());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.MiscellaneousMathematicalSymbolsA), Middle.MiscellaneousMathematicalSymbolsA());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.SupplementalArrowsA), Middle.SupplementalArrowsA());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.BraillePatterns), Middle.BraillePatterns());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.SupplementalArrowsB), Middle.SupplementalArrowsB());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.MiscellaneousMathematicalSymbolsB), Middle.MiscellaneousMathematicalSymbolsB());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.SupplementalMathematicalOperators), Middle.SupplementalMathematicalOperators());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.MiscellaneousSymbolsAndArrows), Middle.MiscellaneousSymbolsAndArrows());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.Glagolitic), Middle.Glagolitic());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.LatinExtendedC), Middle.LatinExtendedC());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.Coptic), Middle.Coptic());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.GeorgianSupplement), Middle.GeorgianSupplement());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.Tifinagh), Middle.Tifinagh());
            PunchHolesIfNeeded(ref safeList, Middle.IsFlagSet(codeCharts, MidCodeCharts.BlockElements), Middle.EthiopicExtended());
        }

        /// <summary>
        /// Punch appropriate holes for the selected code charts.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="codeCharts">The code charts to punch.</param>
        private static void PunchCodeCharts(ref char[][] safeList, UpperMidCodeCharts codeCharts)
        {
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CyrillicExtendedA), UpperMiddle.CyrillicExtendedA());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.SupplementalPunctuation), UpperMiddle.SupplementalPunctuation());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CjkRadicalsSupplement), UpperMiddle.CjkRadicalsSupplement());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.KangxiRadicals), UpperMiddle.KangxiRadicals());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.IdeographicDescriptionCharacters), UpperMiddle.IdeographicDescriptionCharacters());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CjkSymbolsAndPunctuation), UpperMiddle.CjkSymbolsAndPunctuation());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Hiragana), UpperMiddle.Hiragana());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Katakana), UpperMiddle.Katakana());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Bopomofo), UpperMiddle.Bopomofo());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.HangulCompatibilityJamo), UpperMiddle.HangulCompatibilityJamo());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Kanbun), UpperMiddle.Kanbun());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.BopomofoExtended), UpperMiddle.BopomofoExtended());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CjkStrokes), UpperMiddle.CjkStrokes());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.KatakanaPhoneticExtensions), UpperMiddle.KatakanaPhoneticExtensions());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.EnclosedCjkLettersAndMonths), UpperMiddle.EnclosedCjkLettersAndMonths());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CjkCompatibility), UpperMiddle.CjkCompatibility());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CjkUnifiedIdeographsExtensionA), UpperMiddle.CjkUnifiedIdeographsExtensionA());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.YijingHexagramSymbols), UpperMiddle.YijingHexagramSymbols());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CjkUnifiedIdeographs), UpperMiddle.CjkUnifiedIdeographs());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.YiSyllables), UpperMiddle.YiSyllables());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.YiRadicals), UpperMiddle.YiRadicals());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Lisu), UpperMiddle.Lisu());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Vai), UpperMiddle.Vai());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CyrillicExtendedB), UpperMiddle.CyrillicExtendedB());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Bamum), UpperMiddle.Bamum());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.ModifierToneLetters), UpperMiddle.ModifierToneLetters());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.LatinExtendedD), UpperMiddle.LatinExtendedD());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.SylotiNagri), UpperMiddle.SylotiNagri());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.CommonIndicNumberForms), UpperMiddle.CommonIndicNumberForms());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Phagspa), UpperMiddle.Phagspa());
            PunchHolesIfNeeded(ref safeList, UpperMiddle.IsFlagSet(codeCharts, UpperMidCodeCharts.Saurashtra), UpperMiddle.Saurashtra());
        }

        /// <summary>
        /// Punch appropriate holes for the selected code charts.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="codeCharts">The code charts to punch.</param>
        private static void PunchCodeCharts(ref char[][] safeList, UpperCodeCharts codeCharts)
        {
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.DevanagariExtended), Upper.DevanagariExtended());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.KayahLi), Upper.KayahLi());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.Rejang), Upper.Rejang());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.HangulJamoExtendedA), Upper.HangulJamoExtendedA());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.Javanese), Upper.Javanese());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.Cham), Upper.Cham());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.MyanmarExtendedA), Upper.MyanmarExtendedA());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.TaiViet), Upper.TaiViet());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.MeeteiMayek), Upper.MeeteiMayek());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.HangulSyllables), Upper.HangulSyllables());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.HangulJamoExtendedB), Upper.HangulJamoExtendedB());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.CjkCompatibilityIdeographs), Upper.CjkCompatibilityIdeographs());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.AlphabeticPresentationForms), Upper.AlphabeticPresentationForms());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.ArabicPresentationFormsA), Upper.ArabicPresentationFormsA());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.VariationSelectors), Upper.VariationSelectors());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.VerticalForms), Upper.VerticalForms());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.CombiningHalfMarks), Upper.CombiningHalfMarks());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.CjkCompatibilityForms), Upper.CjkCompatibilityForms());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.SmallFormVariants), Upper.SmallFormVariants());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.ArabicPresentationFormsB), Upper.ArabicPresentationFormsB());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.HalfWidthAndFullWidthForms), Upper.HalfWidthAndFullWidthForms());
            PunchHolesIfNeeded(ref safeList, Upper.IsFlagSet(codeCharts, UpperCodeCharts.Specials), Upper.Specials());
        }

        /// <summary>
        /// Punches holes as necessary.
        /// </summary>
        /// <param name="safeList">The safe list to punch through.</param>
        /// <param name="needed">Value indicating whether the holes should be punched.</param>
        /// <param name="whiteListedCharacters">The list of character positions to punch.</param>
        private static void PunchHolesIfNeeded(ref char[][] safeList, bool needed, IEnumerable whiteListedCharacters)
        {
            if (needed)
            {
                foreach (int whiteListedCharacter in whiteListedCharacters)
                {
                    safeList[whiteListedCharacter] = null;
                }
            }
        }
    }
}
