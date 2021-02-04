// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lower.cs" company="Microsoft Corporation">
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
//   Provides safe character positions for the lower section of the UTF code tables.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AntiLdapInjection
{
    using System.Collections;

    /// <summary>
    /// Provides safe character positions for the lower section of the UTF code tables.
    /// </summary>
    internal static class Lower
    {
        /// <summary>
        /// Determines if the specified flag is set.
        /// </summary>
        /// <param name="flags">The value to check.</param>
        /// <param name="flagToCheck">The flag to check for.</param>
        /// <returns>true if the flag is set, otherwise false.</returns>
        internal static bool IsFlagSet(LowerCodeCharts flags, LowerCodeCharts flagToCheck)
        {
            return (flags & flagToCheck) != 0;
        }

        /// <summary>
        /// Provides the safe characters for the Basic Latin code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable BasicLatin()
        {
            return CodeChartHelper.GetRange(32, 126);
        }

        /// <summary>
        /// Provides the safe characters for the Latin 1 Supplement code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Latin1Supplement()
        {
            return CodeChartHelper.GetRange(161, 255, (int i) => i == 173);
        }

        /// <summary>
        /// Provides the safe characters for the Latin Extended A code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable LatinExtendedA()
        {
            return CodeChartHelper.GetRange(256, 383);
        }

        /// <summary>
        /// Provides the safe characters for the Latin Extended B code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable LatinExtendedB()
        {
            return CodeChartHelper.GetRange(384, 591);
        }

        /// <summary>
        /// Provides the safe characters for the IPA Extensions code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable IpaExtensions()
        {
            return CodeChartHelper.GetRange(592, 687);
        }

        /// <summary>
        /// Provides the safe characters for the Spacing Modifiers code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable SpacingModifierLetters()
        {
            return CodeChartHelper.GetRange(688, 767);
        }

        /// <summary>
        /// Provides the safe characters for the Combining Diacritical Marks code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CombiningDiacriticalMarks()
        {
            return CodeChartHelper.GetRange(768, 879);
        }

        /// <summary>
        /// Provides the safe characters for the Greek and Coptic code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable GreekAndCoptic()
        {
            return CodeChartHelper.GetRange(880, 1023, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if (i != 907 && i != 909)
                        {
                            return i == 930;
                        }

                        break;
                    case 888:
                    case 889:
                    case 895:
                    case 896:
                    case 897:
                    case 898:
                    case 899:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Cyrillic code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Cyrillic()
        {
            return CodeChartHelper.GetRange(1024, 1279);
        }

        /// <summary>
        /// Provides the safe characters for the Cyrillic Supplement code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable CyrillicSupplement()
        {
            return CodeChartHelper.GetRange(1280, 1317);
        }

        /// <summary>
        /// Provides the safe characters for the Armenian code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Armenian()
        {
            return CodeChartHelper.GetRange(1329, 1418, (int i) => i == 1367 || i == 1368 || i == 1376 || i == 1416);
        }

        /// <summary>
        /// Provides the safe characters for the Hebrew code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Hebrew()
        {
            return CodeChartHelper.GetRange(1425, 1524,
                (int i) => (i >= 1480 && i <= 1487) || (i >= 1515 && i <= 1519));
        }

        /// <summary>
        /// Provides the safe characters for the Arabic code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Arabic()
        {
            return CodeChartHelper.GetRange(1536, 1791,
                (int i) => i == 1540 || i == 1541 || i == 1564 || i == 1565 || i == 1568 || i == 1631);
        }

        /// <summary>
        /// Provides the safe characters for the Syriac code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Syriac()
        {
            return CodeChartHelper.GetRange(1792, 1871, (int i) => i == 1806 || i == 1867 || i == 1868);
        }

        /// <summary>
        /// Provides the safe characters for the Arabic Supplement code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable ArabicSupplement()
        {
            return CodeChartHelper.GetRange(1872, 1919);
        }

        /// <summary>
        /// Provides the safe characters for the Thaana code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Thaana()
        {
            return CodeChartHelper.GetRange(1920, 1969);
        }

        /// <summary>
        /// Provides the safe characters for the Nko code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Nko()
        {
            return CodeChartHelper.GetRange(1984, 2042);
        }

        /// <summary>
        /// Provides the safe characters for the Samaritan code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Samaritan()
        {
            return CodeChartHelper.GetRange(2048, 2110, (int i) => i == 2094 || i == 2095);
        }

        /// <summary>
        /// Provides the safe characters for the Devenagari code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Devanagari()
        {
            return CodeChartHelper.GetRange(2304, 2431,
                (int i) => i == 2362 || i == 2363 || i == 2383 || i == 2390 || i == 2391 || (i >= 2419 && i <= 2424));
        }

        /// <summary>
        /// Provides the safe characters for the Bengali code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Bengali()
        {
            return CodeChartHelper.GetRange(2433, 2555, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if ((i < 2520 || i > 2523) && i != 2526 && i != 2532)
                        {
                            return i == 2533;
                        }

                        break;
                    case 2436:
                    case 2445:
                    case 2446:
                    case 2449:
                    case 2450:
                    case 2473:
                    case 2481:
                    case 2483:
                    case 2484:
                    case 2485:
                    case 2490:
                    case 2491:
                    case 2501:
                    case 2502:
                    case 2505:
                    case 2506:
                    case 2511:
                    case 2512:
                    case 2513:
                    case 2514:
                    case 2515:
                    case 2516:
                    case 2517:
                    case 2518:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Gurmukhi code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Gurmukhi()
        {
            return CodeChartHelper.GetRange(2561, 2677, delegate(int i)
            {
                switch (i)
                {
                    default:
                        switch (i)
                        {
                            default:
                                switch (i)
                                {
                                    default:
                                        if ((i < 2642 || i > 2648) && i != 2653)
                                        {
                                            if (i >= 2655)
                                            {
                                                return i <= 2661;
                                            }

                                            return false;
                                        }

                                        break;
                                    case 2633:
                                    case 2634:
                                    case 2638:
                                    case 2639:
                                    case 2640:
                                        break;
                                }

                                break;
                            case 2577:
                            case 2578:
                            case 2601:
                            case 2609:
                            case 2612:
                            case 2615:
                            case 2618:
                            case 2619:
                            case 2621:
                            case 2627:
                            case 2628:
                            case 2629:
                            case 2630:
                                break;
                        }

                        break;
                    case 2564:
                    case 2571:
                    case 2572:
                    case 2573:
                    case 2574:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Gujarati code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Gujarati()
        {
            return CodeChartHelper.GetRange(2689, 2801, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if (i != 2788 && i != 2789)
                        {
                            return i == 2800;
                        }

                        break;
                    case 2692:
                    case 2702:
                    case 2706:
                    case 2729:
                    case 2737:
                    case 2740:
                    case 2746:
                    case 2747:
                    case 2758:
                    case 2762:
                    case 2766:
                    case 2767:
                    case 2769:
                    case 2770:
                    case 2771:
                    case 2772:
                    case 2773:
                    case 2774:
                    case 2775:
                    case 2776:
                    case 2777:
                    case 2778:
                    case 2779:
                    case 2780:
                    case 2781:
                    case 2782:
                    case 2783:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Oriya code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Oriya()
        {
            return CodeChartHelper.GetRange(2817, 2929, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if ((i < 2904 || i > 2907) && i != 2910 && i != 2916)
                        {
                            return i == 2917;
                        }

                        break;
                    case 2820:
                    case 2829:
                    case 2830:
                    case 2833:
                    case 2834:
                    case 2857:
                    case 2865:
                    case 2868:
                    case 2874:
                    case 2875:
                    case 2885:
                    case 2886:
                    case 2889:
                    case 2890:
                    case 2894:
                    case 2895:
                    case 2896:
                    case 2897:
                    case 2898:
                    case 2899:
                    case 2900:
                    case 2901:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Tamil code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Tamil()
        {
            return CodeChartHelper.GetRange(2946, 3066, delegate(int i)
            {
                switch (i)
                {
                    default:
                        switch (i)
                        {
                            default:
                                if (i >= 3032)
                                {
                                    return i <= 3045;
                                }

                                return false;
                            case 3011:
                            case 3012:
                            case 3013:
                            case 3017:
                            case 3022:
                            case 3023:
                            case 3025:
                            case 3026:
                            case 3027:
                            case 3028:
                            case 3029:
                            case 3030:
                                break;
                        }

                        break;
                    case 2948:
                    case 2955:
                    case 2956:
                    case 2957:
                    case 2961:
                    case 2966:
                    case 2967:
                    case 2968:
                    case 2971:
                    case 2973:
                    case 2976:
                    case 2977:
                    case 2978:
                    case 2981:
                    case 2982:
                    case 2983:
                    case 2987:
                    case 2988:
                    case 2989:
                    case 3002:
                    case 3003:
                    case 3004:
                    case 3005:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Telugu code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Telugu()
        {
            return CodeChartHelper.GetRange(3073, 3199, delegate(int i)
            {
                switch (i)
                {
                    default:
                        switch (i)
                        {
                            default:
                                if (i != 3172 && i != 3173)
                                {
                                    if (i >= 3184)
                                    {
                                        return i <= 3191;
                                    }

                                    return false;
                                }

                                break;
                            case 3159:
                            case 3162:
                            case 3163:
                            case 3164:
                            case 3165:
                            case 3166:
                            case 3167:
                                break;
                        }

                        break;
                    case 3076:
                    case 3085:
                    case 3089:
                    case 3113:
                    case 3124:
                    case 3130:
                    case 3131:
                    case 3132:
                    case 3141:
                    case 3145:
                    case 3150:
                    case 3151:
                    case 3152:
                    case 3153:
                    case 3154:
                    case 3155:
                    case 3156:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Kannada code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Kannada()
        {
            return CodeChartHelper.GetRange(3202, 3314, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if ((i < 3287 || i > 3293) && i != 3295 && i != 3300 && i != 3301)
                        {
                            return i == 3312;
                        }

                        break;
                    case 3204:
                    case 3213:
                    case 3217:
                    case 3241:
                    case 3252:
                    case 3258:
                    case 3259:
                    case 3269:
                    case 3273:
                    case 3278:
                    case 3279:
                    case 3280:
                    case 3281:
                    case 3282:
                    case 3283:
                    case 3284:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Malayalam code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Malayalam()
        {
            return CodeChartHelper.GetRange(3330, 3455, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if ((i < 3416 || i > 3423) && i != 3428 && i != 3429 && i != 3446 && i != 3447)
                        {
                            return i == 3448;
                        }

                        break;
                    case 3332:
                    case 3341:
                    case 3345:
                    case 3369:
                    case 3386:
                    case 3387:
                    case 3388:
                    case 3397:
                    case 3401:
                    case 3406:
                    case 3407:
                    case 3408:
                    case 3409:
                    case 3410:
                    case 3411:
                    case 3412:
                    case 3413:
                    case 3414:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Sinhala code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Sinhala()
        {
            return CodeChartHelper.GetRange(3458, 3572, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if (i != 3541 && i != 3543)
                        {
                            if (i >= 3552)
                            {
                                return i <= 3569;
                            }

                            return false;
                        }

                        break;
                    case 3460:
                    case 3479:
                    case 3480:
                    case 3481:
                    case 3506:
                    case 3516:
                    case 3518:
                    case 3519:
                    case 3527:
                    case 3528:
                    case 3529:
                    case 3531:
                    case 3532:
                    case 3533:
                    case 3534:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Thai code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Thai()
        {
            return CodeChartHelper.GetRange(3585, 3675, (int i) => i >= 3643 && i <= 3646);
        }

        /// <summary>
        /// Provides the safe characters for the Lao code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Lao()
        {
            return CodeChartHelper.GetRange(3713, 3805, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if (i != 3736 && i != 3744 && i != 3748 && i != 3750 && i != 3752 && i != 3753 && i != 3756 &&
                            i != 3770 && i != 3774 && i != 3775 && i != 3781 && i != 3783 && i != 3790 && i != 3791 &&
                            i != 3802)
                        {
                            return i == 3803;
                        }

                        break;
                    case 3715:
                    case 3717:
                    case 3718:
                    case 3721:
                    case 3723:
                    case 3724:
                    case 3726:
                    case 3727:
                    case 3728:
                    case 3729:
                    case 3730:
                    case 3731:
                        break;
                }

                return true;
            });
        }

        /// <summary>
        /// Provides the safe characters for the Tibetan code table.
        /// </summary>
        /// <returns>The safe characters for the code table.</returns>
        internal static IEnumerable Tibetan()
        {
            return CodeChartHelper.GetRange(3840, 4056, delegate(int i)
            {
                switch (i)
                {
                    default:
                        if ((i < 3980 || i > 3983) && i != 3992 && i != 4029)
                        {
                            return i == 4045;
                        }

                        break;
                    case 3912:
                    case 3949:
                    case 3950:
                    case 3951:
                    case 3952:
                        break;
                }

                return true;
            });
        }
    }
}
