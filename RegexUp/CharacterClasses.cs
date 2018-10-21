namespace RegexUp
{
    /// <summary>
    /// Character class definitions.
    /// </summary>
    public static class CharacterClasses
    {
        /// <summary>
        /// Wildcard: Matches any single character except \n.
        /// </summary>
        public static ICharacterClass Wildcard { get; } = new UnicodeCategory(@".");

        /// <summary>
        /// Matches any word character.
        /// </summary>
        public static ICharacterClass Word { get; } = new UnicodeCategory(@"\w");

        /// <summary>
        /// Matches any non-word character.
        /// </summary>
        public static ICharacterClass NonWord { get; } = new UnicodeCategory(@"\W");

        /// <summary>
        /// Matches any white-space character.
        /// </summary>
        public static ICharacterClass Whitespace { get; } = new UnicodeCategory(@"\s");

        /// <summary>
        /// Matches any non-white-space character.
        /// </summary>
        public static ICharacterClass NonWhitespace { get; } = new UnicodeCategory(@"\S");

        /// <summary>
        /// Matches any digit character.
        /// </summary>
        public static ICharacterClass Digit { get; } = new UnicodeCategory(@"\d");

        /// <summary>
        /// Matches any non-digit character.
        /// </summary>
        public static ICharacterClass NonDigit { get; } = new UnicodeCategory(@"\D");

        /// <summary>
        /// General unicode category definitions.
        /// </summary>
        public static class UnicodeCategories
        {
            public static ICharacterClass IsLetterUppercase { get; } = new UnicodeCategory(@"\p{Lu}");
            public static ICharacterClass IsLetterLowercase { get; } = new UnicodeCategory(@"\p{Ll}");
            public static ICharacterClass IsLetterTitlecase { get; } = new UnicodeCategory(@"\p{Lt}");
            public static ICharacterClass IsLetterModifier { get; } = new UnicodeCategory(@"\p{Lm}");
            public static ICharacterClass IsLetterOther { get; } = new UnicodeCategory(@"\p{Lo}");
            public static ICharacterClass IsLetter { get; } = new UnicodeCategory(@"\p{L}");

            public static ICharacterClass IsNotLetterUppercase { get; } = new UnicodeCategory(@"\P{Lu}");
            public static ICharacterClass IsNotLetterLowercase { get; } = new UnicodeCategory(@"\P{Ll}");
            public static ICharacterClass IsNotLetterTitlecase { get; } = new UnicodeCategory(@"\P{Lt}");
            public static ICharacterClass IsNotLetterModifier { get; } = new UnicodeCategory(@"\P{Lm}");
            public static ICharacterClass IsNotLetterOther { get; } = new UnicodeCategory(@"\P{Lo}");
            public static ICharacterClass IsNotLetter { get; } = new UnicodeCategory(@"\P{L}");

            public static ICharacterClass IsMarkNonspacing { get; } = new UnicodeCategory(@"\p{Mn}");
            public static ICharacterClass IsMarkSpacing { get; } = new UnicodeCategory(@"\p{Mc}");
            public static ICharacterClass IsMarkEnclosing { get; } = new UnicodeCategory(@"\p{Me}");
            public static ICharacterClass IsMark { get; } = new UnicodeCategory(@"\p{M}");

            public static ICharacterClass IsNotMarkNonspacing { get; } = new UnicodeCategory(@"\P{Mn}");
            public static ICharacterClass IsNotMarkSpacing { get; } = new UnicodeCategory(@"\P{Mc}");
            public static ICharacterClass IsNotMarkEnclosing { get; } = new UnicodeCategory(@"\P{Me}");
            public static ICharacterClass IsNotMark { get; } = new UnicodeCategory(@"\P{M}");

            public static ICharacterClass IsNumberDecimalDigit { get; } = new UnicodeCategory(@"\p{Nd}");
            public static ICharacterClass IsNumberLetter { get; } = new UnicodeCategory(@"\p{Nl}");
            public static ICharacterClass IsNumberOther { get; } = new UnicodeCategory(@"\p{No}");
            public static ICharacterClass IsNumber { get; } = new UnicodeCategory(@"\p{N}");

            public static ICharacterClass IsNotNumberDecimalDigit { get; } = new UnicodeCategory(@"\P{Nd}");
            public static ICharacterClass IsNotNumberLetter { get; } = new UnicodeCategory(@"\P{Nl}");
            public static ICharacterClass IsNotNumberOther { get; } = new UnicodeCategory(@"\P{No}");
            public static ICharacterClass IsNotNumber { get; } = new UnicodeCategory(@"\P{N}");

            public static ICharacterClass IsPunctuationConnector { get; } = new UnicodeCategory(@"\p{Pc}");
            public static ICharacterClass IsPunctuationDash { get; } = new UnicodeCategory(@"\p{Pd}");
            public static ICharacterClass IsPunctuationOpen { get; } = new UnicodeCategory(@"\p{Ps}");
            public static ICharacterClass IsPunctuationClose { get; } = new UnicodeCategory(@"\p{Pe}");
            public static ICharacterClass IsPunctuationInitialQuote { get; } = new UnicodeCategory(@"\p{Pi}");
            public static ICharacterClass IsPunctuationFinalQuote { get; } = new UnicodeCategory(@"\p{Pf}");
            public static ICharacterClass IsPunctuationOther { get; } = new UnicodeCategory(@"\p{Po}");
            public static ICharacterClass IsPunctuation { get; } = new UnicodeCategory(@"\p{P}");

            public static ICharacterClass IsNotPunctuationConnector { get; } = new UnicodeCategory(@"\P{Pc}");
            public static ICharacterClass IsNotPunctuationDash { get; } = new UnicodeCategory(@"\P{Pd}");
            public static ICharacterClass IsNotPunctuationOpen { get; } = new UnicodeCategory(@"\P{Ps}");
            public static ICharacterClass IsNotPunctuationClose { get; } = new UnicodeCategory(@"\P{Pe}");
            public static ICharacterClass IsNotPunctuationInitialQuote { get; } = new UnicodeCategory(@"\P{Pi}");
            public static ICharacterClass IsNotPunctuationFinalQuote { get; } = new UnicodeCategory(@"\P{Pf}");
            public static ICharacterClass IsNotPunctuationOther { get; } = new UnicodeCategory(@"\P{Po}");
            public static ICharacterClass IsNotPunctuation { get; } = new UnicodeCategory(@"\P{P}");

            public static ICharacterClass IsSymbolMath { get; } = new UnicodeCategory(@"\p{Sm}");
            public static ICharacterClass IsSymbolCurrency { get; } = new UnicodeCategory(@"\p{Sc}");
            public static ICharacterClass IsSymbolModifier { get; } = new UnicodeCategory(@"\p{Sk}");
            public static ICharacterClass IsSymbolOther { get; } = new UnicodeCategory(@"\p{So}");
            public static ICharacterClass IsSymbol { get; } = new UnicodeCategory(@"\p{S}");

            public static ICharacterClass IsNotSymbolMath { get; } = new UnicodeCategory(@"\P{Sm}");
            public static ICharacterClass IsNotSymbolCurrency { get; } = new UnicodeCategory(@"\P{Sc}");
            public static ICharacterClass IsNotSymbolModifier { get; } = new UnicodeCategory(@"\P{Sk}");
            public static ICharacterClass IsNotSymbolOther { get; } = new UnicodeCategory(@"\P{So}");
            public static ICharacterClass IsNotSymbol { get; } = new UnicodeCategory(@"\P{S}");

            public static ICharacterClass IsSeparatorSpace { get; } = new UnicodeCategory(@"\p{Zs}");
            public static ICharacterClass IsSeparatorLine { get; } = new UnicodeCategory(@"\p{Zl}");
            public static ICharacterClass IsSeparatorParagraph { get; } = new UnicodeCategory(@"\p{Zp}");
            public static ICharacterClass IsSeparator { get; } = new UnicodeCategory(@"\p{Z}");

            public static ICharacterClass IsNotSeparatorSpace { get; } = new UnicodeCategory(@"\P{Zs}");
            public static ICharacterClass IsNotSeparatorLine { get; } = new UnicodeCategory(@"\P{Zl}");
            public static ICharacterClass IsNotSeparatorParagraph { get; } = new UnicodeCategory(@"\P{Zp}");
            public static ICharacterClass IsNotSeparator { get; } = new UnicodeCategory(@"\P{Z}");

            public static ICharacterClass IsOtherControl { get; } = new UnicodeCategory(@"\p{Cc}");
            public static ICharacterClass IsOtherFormat { get; } = new UnicodeCategory(@"\p{Cf}");
            public static ICharacterClass IsOtherSurrogate { get; } = new UnicodeCategory(@"\p{Cs}");
            public static ICharacterClass IsOtherPrivateUse { get; } = new UnicodeCategory(@"\p{Co}");
            public static ICharacterClass IsOther { get; } = new UnicodeCategory(@"\p{Cn}");
            public static ICharacterClass IsControl { get; } = new UnicodeCategory(@"\p{C}");

            public static ICharacterClass IsNotOtherControl { get; } = new UnicodeCategory(@"\P{Cc}");
            public static ICharacterClass IsNotOtherFormat { get; } = new UnicodeCategory(@"\P{Cf}");
            public static ICharacterClass IsNotOtherSurrogate { get; } = new UnicodeCategory(@"\P{Cs}");
            public static ICharacterClass IsNotOtherPrivateUse { get; } = new UnicodeCategory(@"\P{Co}");
            public static ICharacterClass IsNotOther { get; } = new UnicodeCategory(@"\P{Cn}");
            public static ICharacterClass IsNotControl { get; } = new UnicodeCategory(@"\P{C}");
        }

        /// <summary>
        /// Unicode named block definitions.
        /// </summary>
        public static class UnicodeNamedBlocks
        {
            public static ICharacterClass IsBasicLatin { get; } = new UnicodeCategory(@"\p{IsBasicLatin}");
            public static ICharacterClass IsLatin1Supplement { get; } = new UnicodeCategory(@"\p{IsLatin-1Supplement}");
            public static ICharacterClass IsLatinExtendedA { get; } = new UnicodeCategory(@"\p{IsLatinExtended-A}");
            public static ICharacterClass IsLatinExtendedB { get; } = new UnicodeCategory(@"\p{IsLatinExtended-B}");
            public static ICharacterClass IsIPAExtensions { get; } = new UnicodeCategory(@"\p{IsIPAExtensions}");
            public static ICharacterClass IsSpacingModifierLetters { get; } = new UnicodeCategory(@"\p{IsSpacingModifierLetters}");
            public static ICharacterClass IsCombiningDiacriticalMarks { get; } = new UnicodeCategory(@"\p{IsCombiningDiacriticalMarks}");
            public static ICharacterClass IsGreek { get; } = new UnicodeCategory(@"\p{IsGreek}");
            public static ICharacterClass IsGreekAndCoptic { get; } = new UnicodeCategory(@"\p{IsGreekandCoptic}");
            public static ICharacterClass IsCyrillic { get; } = new UnicodeCategory(@"\p{IsCyrillic}");
            public static ICharacterClass IsCyrillicSupplement { get; } = new UnicodeCategory(@"\p{IsCyrillicSupplement}");
            public static ICharacterClass IsArmenian { get; } = new UnicodeCategory(@"\p{IsArmenian}");
            public static ICharacterClass IsHebrew { get; } = new UnicodeCategory(@"\p{IsHebrew}");
            public static ICharacterClass IsArabic { get; } = new UnicodeCategory(@"\p{IsArabic}");
            public static ICharacterClass IsSyriac { get; } = new UnicodeCategory(@"\p{IsSyriac}");
            public static ICharacterClass IsThaana { get; } = new UnicodeCategory(@"\p{IsThaana}");
            public static ICharacterClass IsDevanagari { get; } = new UnicodeCategory(@"\p{IsDevanagari}");
            public static ICharacterClass IsBengali { get; } = new UnicodeCategory(@"\p{IsBengali}");
            public static ICharacterClass IsGurmukhi { get; } = new UnicodeCategory(@"\p{IsGurmukhi}");
            public static ICharacterClass IsGujarati { get; } = new UnicodeCategory(@"\p{IsGujarati}");
            public static ICharacterClass IsOriya { get; } = new UnicodeCategory(@"\p{IsOriya}");
            public static ICharacterClass IsTamil { get; } = new UnicodeCategory(@"\p{IsTamil}");
            public static ICharacterClass IsTelugu { get; } = new UnicodeCategory(@"\p{IsTelugu}");
            public static ICharacterClass IsKannada { get; } = new UnicodeCategory(@"\p{IsKannada}");
            public static ICharacterClass IsMalayalam { get; } = new UnicodeCategory(@"\p{IsMalayalam}");
            public static ICharacterClass IsSinhala { get; } = new UnicodeCategory(@"\p{IsSinhala}");
            public static ICharacterClass IsThai { get; } = new UnicodeCategory(@"\p{IsThai}");
            public static ICharacterClass IsLao { get; } = new UnicodeCategory(@"\p{IsLao}");
            public static ICharacterClass IsTibetan { get; } = new UnicodeCategory(@"\p{IsTibetan}");
            public static ICharacterClass IsMyanmar { get; } = new UnicodeCategory(@"\p{IsMyanmar}");
            public static ICharacterClass IsGeorgian { get; } = new UnicodeCategory(@"\p{IsGeorgian}");
            public static ICharacterClass IsHangulJamo { get; } = new UnicodeCategory(@"\p{IsHangulJamo}");
            public static ICharacterClass IsEthiopic { get; } = new UnicodeCategory(@"\p{IsEthiopic}");
            public static ICharacterClass IsCherokee { get; } = new UnicodeCategory(@"\p{IsCherokee}");
            public static ICharacterClass IsUnifiedCanadianAboriginalSyllabics { get; } = new UnicodeCategory(@"\p{IsUnifiedCanadianAboriginalSyllabics}");
            public static ICharacterClass IsOgham { get; } = new UnicodeCategory(@"\p{IsOgham}");
            public static ICharacterClass IsRunic { get; } = new UnicodeCategory(@"\p{IsRunic}");
            public static ICharacterClass IsTagalog { get; } = new UnicodeCategory(@"\p{IsTagalog}");
            public static ICharacterClass IsHanunoo { get; } = new UnicodeCategory(@"\p{IsHanunoo}");
            public static ICharacterClass IsBuhid { get; } = new UnicodeCategory(@"\p{IsBuhid}");
            public static ICharacterClass IsTagbanwa { get; } = new UnicodeCategory(@"\p{IsTagbanwa}");
            public static ICharacterClass IsKhmer { get; } = new UnicodeCategory(@"\p{IsKhmer}");
            public static ICharacterClass IsMongolian { get; } = new UnicodeCategory(@"\p{IsMongolian}");
            public static ICharacterClass IsLimbu { get; } = new UnicodeCategory(@"\p{IsLimbu}");
            public static ICharacterClass IsTaiLe { get; } = new UnicodeCategory(@"\p{IsTaiLe}");
            public static ICharacterClass IsKhmerSymbols { get; } = new UnicodeCategory(@"\p{IsKhmerSymbols}");
            public static ICharacterClass IsPhoneticExtensions { get; } = new UnicodeCategory(@"\p{IsPhoneticExtensions}");
            public static ICharacterClass IsLatinExtendedAdditional { get; } = new UnicodeCategory(@"\p{IsLatinExtendedAdditional}");
            public static ICharacterClass IsGreekExtended { get; } = new UnicodeCategory(@"\p{IsGreekExtended}");
            public static ICharacterClass IsGeneralPunctuation { get; } = new UnicodeCategory(@"\p{IsGeneralPunctuation}");
            public static ICharacterClass IsSuperscriptsAndSubscripts { get; } = new UnicodeCategory(@"\p{IsSuperscriptsandSubscripts}");
            public static ICharacterClass IsCurrencySymbols { get; } = new UnicodeCategory(@"\p{IsCurrencySymbols}");
            public static ICharacterClass IsCombiningDiacriticalMarksForSymbols { get; } = new UnicodeCategory(@"\p{IsCombiningDiacriticalMarksforSymbols}");
            public static ICharacterClass IsCombiningMarksForSymbols { get; } = new UnicodeCategory(@"\p{IsCombiningMarksforSymbols}");
            public static ICharacterClass IsLetterLikeSymbols { get; } = new UnicodeCategory(@"\p{IsLetterlikeSymbols}");
            public static ICharacterClass IsNumberForms { get; } = new UnicodeCategory(@"\p{IsNumberForms}");
            public static ICharacterClass IsArrows { get; } = new UnicodeCategory(@"\p{IsArrows}");
            public static ICharacterClass IsMathematicalOperators { get; } = new UnicodeCategory(@"\p{IsMathematicalOperators}");
            public static ICharacterClass IsMiscellaneousTechnical { get; } = new UnicodeCategory(@"\p{IsMiscellaneousTechnical}");
            public static ICharacterClass IsControlPictures { get; } = new UnicodeCategory(@"\p{IsControlPictures}");
            public static ICharacterClass IsOpticalCharacterRecognition { get; } = new UnicodeCategory(@"\p{IsOpticalCharacterRecognition}");
            public static ICharacterClass IsEnclosedAlphanumerics { get; } = new UnicodeCategory(@"\p{IsEnclosedAlphanumerics}");
            public static ICharacterClass IsBoxDrawing { get; } = new UnicodeCategory(@"\p{IsBoxDrawing}");
            public static ICharacterClass IsBlockElements { get; } = new UnicodeCategory(@"\p{IsBlockElements}");
            public static ICharacterClass IsGeometricShapes { get; } = new UnicodeCategory(@"\p{IsGeometricShapes}");
            public static ICharacterClass IsMiscellaneousSymbols { get; } = new UnicodeCategory(@"\p{IsMiscellaneousSymbols}");
            public static ICharacterClass IsDingbats { get; } = new UnicodeCategory(@"\p{IsDingbats}");
            public static ICharacterClass IsMiscellaneousMathematicalSymbolsA { get; } = new UnicodeCategory(@"\p{IsMiscellaneousMathematicalSymbols-A}");
            public static ICharacterClass IsSupplementalArrowsA { get; } = new UnicodeCategory(@"\p{IsSupplementalArrows-A}");
            public static ICharacterClass IsBraillePatterns { get; } = new UnicodeCategory(@"\p{IsBraillePatterns}");
            public static ICharacterClass IsSupplementalArrowsB { get; } = new UnicodeCategory(@"\p{IsSupplementalArrows-B}");
            public static ICharacterClass IsMiscellaneousMathematicalSymbolsB { get; } = new UnicodeCategory(@"\p{IsMiscellaneousMathematicalSymbols-B}");
            public static ICharacterClass IsSupplementalMathematicalOperators { get; } = new UnicodeCategory(@"\p{IsSupplementalMathematicalOperators}");
            public static ICharacterClass IsMiscellaneousSymbolsAndArrows { get; } = new UnicodeCategory(@"\p{IsMiscellaneousSymbolsandArrows}");
            public static ICharacterClass IsCJKRadicalsSupplement { get; } = new UnicodeCategory(@"\p{IsCJKRadicalsSupplement}");
            public static ICharacterClass IsKangxiRadicals { get; } = new UnicodeCategory(@"\p{IsKangxiRadicals}");
            public static ICharacterClass IsIdeographicDescriptionCharacters { get; } = new UnicodeCategory(@"\p{IsIdeographicDescriptionCharacters}");
            public static ICharacterClass IsCJKSymbolsAndPunctuation { get; } = new UnicodeCategory(@"\p{IsCJKSymbolsandPunctuation}");
            public static ICharacterClass IsHiragana { get; } = new UnicodeCategory(@"\p{IsHiragana}");
            public static ICharacterClass IsKatakana { get; } = new UnicodeCategory(@"\p{IsKatakana}");
            public static ICharacterClass IsBopomofo { get; } = new UnicodeCategory(@"\p{IsBopomofo}");
            public static ICharacterClass IsHangulCompatibilityJamo { get; } = new UnicodeCategory(@"\p{IsHangulCompatibilityJamo}");
            public static ICharacterClass IsKanbun { get; } = new UnicodeCategory(@"\p{IsKanbun}");
            public static ICharacterClass IsBopomofoExtended { get; } = new UnicodeCategory(@"\p{IsBopomofoExtended}");
            public static ICharacterClass IsKatakanaPhoneticExtensions { get; } = new UnicodeCategory(@"\p{IsKatakanaPhoneticExtensions}");
            public static ICharacterClass IsEnclosedCJKLettersAndMonths { get; } = new UnicodeCategory(@"\p{IsEnclosedCJKLettersandMonths}");
            public static ICharacterClass IsCJKCompatibility { get; } = new UnicodeCategory(@"\p{IsCJKCompatibility}");
            public static ICharacterClass IsCJKUnifiedIdeographsExtensionA { get; } = new UnicodeCategory(@"\p{IsCJKUnifiedIdeographsExtensionA}");
            public static ICharacterClass IsYijingHexagramSymbols { get; } = new UnicodeCategory(@"\p{IsYijingHexagramSymbols}");
            public static ICharacterClass IsCJKUnifiedIdeographs { get; } = new UnicodeCategory(@"\p{IsCJKUnifiedIdeographs}");
            public static ICharacterClass IsYiSyllables { get; } = new UnicodeCategory(@"\p{IsYiSyllables}");
            public static ICharacterClass IsYiRadicals { get; } = new UnicodeCategory(@"\p{IsYiRadicals}");
            public static ICharacterClass IsHangulSyllables { get; } = new UnicodeCategory(@"\p{IsHangulSyllables}");
            public static ICharacterClass IsHighSurrogates { get; } = new UnicodeCategory(@"\p{IsHighSurrogates}");
            public static ICharacterClass IsHighPrivateUseSurrogates { get; } = new UnicodeCategory(@"\p{IsHighPrivateUseSurrogates}");
            public static ICharacterClass IsLowSurrogates { get; } = new UnicodeCategory(@"\p{IsLowSurrogates}");
            public static ICharacterClass IsPrivateUse { get; } = new UnicodeCategory(@"\p{IsPrivateUse}");
            public static ICharacterClass IsPrivateUseArea { get; } = new UnicodeCategory(@"\p{IsPrivateUseArea}");
            public static ICharacterClass IsCJKCompatibilityIdeographs { get; } = new UnicodeCategory(@"\p{IsCJKCompatibilityIdeographs}");
            public static ICharacterClass IsAlphabeticPresentationForms { get; } = new UnicodeCategory(@"\p{IsAlphabeticPresentationForms}");
            public static ICharacterClass IsArabicPresentationFormsA { get; } = new UnicodeCategory(@"\p{IsArabicPresentationForms-A}");
            public static ICharacterClass IsVariationSelectors { get; } = new UnicodeCategory(@"\p{IsVariationSelectors}");
            public static ICharacterClass IsCombiningHalfMarks { get; } = new UnicodeCategory(@"\p{IsCombiningHalfMarks}");
            public static ICharacterClass IsCJKCompatibilityForms { get; } = new UnicodeCategory(@"\p{IsCJKCompatibilityForms}");
            public static ICharacterClass IsSmallFormVariants { get; } = new UnicodeCategory(@"\p{IsSmallFormVariants}");
            public static ICharacterClass IsArabicPresentationFormsB { get; } = new UnicodeCategory(@"\p{IsArabicPresentationForms-B}");
            public static ICharacterClass IsHalfwidthAndFullwidthForms { get; } = new UnicodeCategory(@"\p{IsHalfwidthandFullwidthForms}");
            public static ICharacterClass IsSpecials { get; } = new UnicodeCategory(@"\p{IsSpecials}");

            public static ICharacterClass IsNotBasicLatin { get; } = new UnicodeCategory(@"\P{IsBasicLatin}");
            public static ICharacterClass IsNotLatin1Supplement { get; } = new UnicodeCategory(@"\P{IsLatin-1Supplement}");
            public static ICharacterClass IsNotLatinExtendedA { get; } = new UnicodeCategory(@"\P{IsLatinExtended-A}");
            public static ICharacterClass IsNotLatinExtendedB { get; } = new UnicodeCategory(@"\P{IsLatinExtended-B}");
            public static ICharacterClass IsNotIPAExtensions { get; } = new UnicodeCategory(@"\P{IsIPAExtensions}");
            public static ICharacterClass IsNotSpacingModifierLetters { get; } = new UnicodeCategory(@"\P{IsSpacingModifierLetters}");
            public static ICharacterClass IsNotCombiningDiacriticalMarks { get; } = new UnicodeCategory(@"\P{IsCombiningDiacriticalMarks}");
            public static ICharacterClass IsNotGreek { get; } = new UnicodeCategory(@"\P{IsGreek}");
            public static ICharacterClass IsNotGreekAndCoptic { get; } = new UnicodeCategory(@"\P{IsGreekandCoptic}");
            public static ICharacterClass IsNotCyrillic { get; } = new UnicodeCategory(@"\P{IsCyrillic}");
            public static ICharacterClass IsNotCyrillicSupplement { get; } = new UnicodeCategory(@"\P{IsCyrillicSupplement}");
            public static ICharacterClass IsNotArmenian { get; } = new UnicodeCategory(@"\P{IsArmenian}");
            public static ICharacterClass IsNotHebrew { get; } = new UnicodeCategory(@"\P{IsHebrew}");
            public static ICharacterClass IsNotArabic { get; } = new UnicodeCategory(@"\P{IsArabic}");
            public static ICharacterClass IsNotSyriac { get; } = new UnicodeCategory(@"\P{IsSyriac}");
            public static ICharacterClass IsNotThaana { get; } = new UnicodeCategory(@"\P{IsThaana}");
            public static ICharacterClass IsNotDevanagari { get; } = new UnicodeCategory(@"\P{IsDevanagari}");
            public static ICharacterClass IsNotBengali { get; } = new UnicodeCategory(@"\P{IsBengali}");
            public static ICharacterClass IsNotGurmukhi { get; } = new UnicodeCategory(@"\P{IsGurmukhi}");
            public static ICharacterClass IsNotGujarati { get; } = new UnicodeCategory(@"\P{IsGujarati}");
            public static ICharacterClass IsNotOriya { get; } = new UnicodeCategory(@"\P{IsOriya}");
            public static ICharacterClass IsNotTamil { get; } = new UnicodeCategory(@"\P{IsTamil}");
            public static ICharacterClass IsNotTelugu { get; } = new UnicodeCategory(@"\P{IsTelugu}");
            public static ICharacterClass IsNotKannada { get; } = new UnicodeCategory(@"\P{IsKannada}");
            public static ICharacterClass IsNotMalayalam { get; } = new UnicodeCategory(@"\P{IsMalayalam}");
            public static ICharacterClass IsNotSinhala { get; } = new UnicodeCategory(@"\P{IsSinhala}");
            public static ICharacterClass IsNotThai { get; } = new UnicodeCategory(@"\P{IsThai}");
            public static ICharacterClass IsNotLao { get; } = new UnicodeCategory(@"\P{IsLao}");
            public static ICharacterClass IsNotTibetan { get; } = new UnicodeCategory(@"\P{IsTibetan}");
            public static ICharacterClass IsNotMyanmar { get; } = new UnicodeCategory(@"\P{IsMyanmar}");
            public static ICharacterClass IsNotGeorgian { get; } = new UnicodeCategory(@"\P{IsGeorgian}");
            public static ICharacterClass IsNotHangulJamo { get; } = new UnicodeCategory(@"\P{IsHangulJamo}");
            public static ICharacterClass IsNotEthiopic { get; } = new UnicodeCategory(@"\P{IsEthiopic}");
            public static ICharacterClass IsNotCherokee { get; } = new UnicodeCategory(@"\P{IsCherokee}");
            public static ICharacterClass IsNotUnifiedCanadianAboriginalSyllabics { get; } = new UnicodeCategory(@"\P{IsUnifiedCanadianAboriginalSyllabics}");
            public static ICharacterClass IsNotOgham { get; } = new UnicodeCategory(@"\P{IsOgham}");
            public static ICharacterClass IsNotRunic { get; } = new UnicodeCategory(@"\P{IsRunic}");
            public static ICharacterClass IsNotTagalog { get; } = new UnicodeCategory(@"\P{IsTagalog}");
            public static ICharacterClass IsNotHanunoo { get; } = new UnicodeCategory(@"\P{IsHanunoo}");
            public static ICharacterClass IsNotBuhid { get; } = new UnicodeCategory(@"\P{IsBuhid}");
            public static ICharacterClass IsNotTagbanwa { get; } = new UnicodeCategory(@"\P{IsTagbanwa}");
            public static ICharacterClass IsNotKhmer { get; } = new UnicodeCategory(@"\P{IsKhmer}");
            public static ICharacterClass IsNotMongolian { get; } = new UnicodeCategory(@"\P{IsMongolian}");
            public static ICharacterClass IsNotLimbu { get; } = new UnicodeCategory(@"\P{IsLimbu}");
            public static ICharacterClass IsNotTaiLe { get; } = new UnicodeCategory(@"\P{IsTaiLe}");
            public static ICharacterClass IsNotKhmerSymbols { get; } = new UnicodeCategory(@"\P{IsKhmerSymbols}");
            public static ICharacterClass IsNotPhoneticExtensions { get; } = new UnicodeCategory(@"\P{IsPhoneticExtensions}");
            public static ICharacterClass IsNotLatinExtendedAdditional { get; } = new UnicodeCategory(@"\P{IsLatinExtendedAdditional}");
            public static ICharacterClass IsNotGreekExtended { get; } = new UnicodeCategory(@"\P{IsGreekExtended}");
            public static ICharacterClass IsNotGeneralPunctuation { get; } = new UnicodeCategory(@"\P{IsGeneralPunctuation}");
            public static ICharacterClass IsNotSuperscriptsAndSubscripts { get; } = new UnicodeCategory(@"\P{IsSuperscriptsandSubscripts}");
            public static ICharacterClass IsNotCurrencySymbols { get; } = new UnicodeCategory(@"\P{IsCurrencySymbols}");
            public static ICharacterClass IsNotCombiningDiacriticalMarksForSymbols { get; } = new UnicodeCategory(@"\P{IsCombiningDiacriticalMarksforSymbols}");
            public static ICharacterClass IsNotCombiningMarksForSymbols { get; } = new UnicodeCategory(@"\P{IsCombiningMarksforSymbols}");
            public static ICharacterClass IsNotLetterLikeSymbols { get; } = new UnicodeCategory(@"\P{IsLetterlikeSymbols}");
            public static ICharacterClass IsNotNumberForms { get; } = new UnicodeCategory(@"\P{IsNumberForms}");
            public static ICharacterClass IsNotArrows { get; } = new UnicodeCategory(@"\P{IsArrows}");
            public static ICharacterClass IsNotMathematicalOperators { get; } = new UnicodeCategory(@"\P{IsMathematicalOperators}");
            public static ICharacterClass IsNotMiscellaneousTechnical { get; } = new UnicodeCategory(@"\P{IsMiscellaneousTechnical}");
            public static ICharacterClass IsNotControlPictures { get; } = new UnicodeCategory(@"\P{IsControlPictures}");
            public static ICharacterClass IsNotOpticalCharacterRecognition { get; } = new UnicodeCategory(@"\P{IsOpticalCharacterRecognition}");
            public static ICharacterClass IsNotEnclosedAlphanumerics { get; } = new UnicodeCategory(@"\P{IsEnclosedAlphanumerics}");
            public static ICharacterClass IsNotBoxDrawing { get; } = new UnicodeCategory(@"\P{IsBoxDrawing}");
            public static ICharacterClass IsNotBlockElements { get; } = new UnicodeCategory(@"\P{IsBlockElements}");
            public static ICharacterClass IsNotGeometricShapes { get; } = new UnicodeCategory(@"\P{IsGeometricShapes}");
            public static ICharacterClass IsNotMiscellaneousSymbols { get; } = new UnicodeCategory(@"\P{IsMiscellaneousSymbols}");
            public static ICharacterClass IsNotDingbats { get; } = new UnicodeCategory(@"\P{IsDingbats}");
            public static ICharacterClass IsNotMiscellaneousMathematicalSymbolsA { get; } = new UnicodeCategory(@"\P{IsMiscellaneousMathematicalSymbols-A}");
            public static ICharacterClass IsNotSupplementalArrowsA { get; } = new UnicodeCategory(@"\P{IsSupplementalArrows-A}");
            public static ICharacterClass IsNotBraillePatterns { get; } = new UnicodeCategory(@"\P{IsBraillePatterns}");
            public static ICharacterClass IsNotSupplementalArrowsB { get; } = new UnicodeCategory(@"\P{IsSupplementalArrows-B}");
            public static ICharacterClass IsNotMiscellaneousMathematicalSymbolsB { get; } = new UnicodeCategory(@"\P{IsMiscellaneousMathematicalSymbols-B}");
            public static ICharacterClass IsNotSupplementalMathematicalOperators { get; } = new UnicodeCategory(@"\P{IsSupplementalMathematicalOperators}");
            public static ICharacterClass IsNotMiscellaneousSymbolsAndArrows { get; } = new UnicodeCategory(@"\P{IsMiscellaneousSymbolsandArrows}");
            public static ICharacterClass IsNotCJKRadicalsSupplement { get; } = new UnicodeCategory(@"\P{IsCJKRadicalsSupplement}");
            public static ICharacterClass IsNotKangxiRadicals { get; } = new UnicodeCategory(@"\P{IsKangxiRadicals}");
            public static ICharacterClass IsNotIdeographicDescriptionCharacters { get; } = new UnicodeCategory(@"\P{IsIdeographicDescriptionCharacters}");
            public static ICharacterClass IsNotCJKSymbolsAndPunctuation { get; } = new UnicodeCategory(@"\P{IsCJKSymbolsandPunctuation}");
            public static ICharacterClass IsNotHiragana { get; } = new UnicodeCategory(@"\P{IsHiragana}");
            public static ICharacterClass IsNotKatakana { get; } = new UnicodeCategory(@"\P{IsKatakana}");
            public static ICharacterClass IsNotBopomofo { get; } = new UnicodeCategory(@"\P{IsBopomofo}");
            public static ICharacterClass IsNotHangulCompatibilityJamo { get; } = new UnicodeCategory(@"\P{IsHangulCompatibilityJamo}");
            public static ICharacterClass IsNotKanbun { get; } = new UnicodeCategory(@"\P{IsKanbun}");
            public static ICharacterClass IsNotBopomofoExtended { get; } = new UnicodeCategory(@"\P{IsBopomofoExtended}");
            public static ICharacterClass IsNotKatakanaPhoneticExtensions { get; } = new UnicodeCategory(@"\P{IsKatakanaPhoneticExtensions}");
            public static ICharacterClass IsNotEnclosedCJKLettersAndMonths { get; } = new UnicodeCategory(@"\P{IsEnclosedCJKLettersandMonths}");
            public static ICharacterClass IsNotCJKCompatibility { get; } = new UnicodeCategory(@"\P{IsCJKCompatibility}");
            public static ICharacterClass IsNotCJKUnifiedIdeographsExtensionA { get; } = new UnicodeCategory(@"\P{IsCJKUnifiedIdeographsExtensionA}");
            public static ICharacterClass IsNotYijingHexagramSymbols { get; } = new UnicodeCategory(@"\P{IsYijingHexagramSymbols}");
            public static ICharacterClass IsNotCJKUnifiedIdeographs { get; } = new UnicodeCategory(@"\P{IsCJKUnifiedIdeographs}");
            public static ICharacterClass IsNotYiSyllables { get; } = new UnicodeCategory(@"\P{IsYiSyllables}");
            public static ICharacterClass IsNotYiRadicals { get; } = new UnicodeCategory(@"\P{IsYiRadicals}");
            public static ICharacterClass IsNotHangulSyllables { get; } = new UnicodeCategory(@"\P{IsHangulSyllables}");
            public static ICharacterClass IsNotHighSurrogates { get; } = new UnicodeCategory(@"\P{IsHighSurrogates}");
            public static ICharacterClass IsNotHighPrivateUseSurrogates { get; } = new UnicodeCategory(@"\P{IsHighPrivateUseSurrogates}");
            public static ICharacterClass IsNotLowSurrogates { get; } = new UnicodeCategory(@"\P{IsLowSurrogates}");
            public static ICharacterClass IsNotPrivateUse { get; } = new UnicodeCategory(@"\P{IsPrivateUse}");
            public static ICharacterClass IsNotPrivateUseArea { get; } = new UnicodeCategory(@"\P{IsPrivateUseArea}");
            public static ICharacterClass IsNotCJKCompatibilityIdeographs { get; } = new UnicodeCategory(@"\P{IsCJKCompatibilityIdeographs}");
            public static ICharacterClass IsNotAlphabeticPresentationForms { get; } = new UnicodeCategory(@"\P{IsAlphabeticPresentationForms}");
            public static ICharacterClass IsNotArabicPresentationFormsA { get; } = new UnicodeCategory(@"\P{IsArabicPresentationForms-A}");
            public static ICharacterClass IsNotVariationSelectors { get; } = new UnicodeCategory(@"\P{IsVariationSelectors}");
            public static ICharacterClass IsNotCombiningHalfMarks { get; } = new UnicodeCategory(@"\P{IsCombiningHalfMarks}");
            public static ICharacterClass IsNotCJKCompatibilityForms { get; } = new UnicodeCategory(@"\P{IsCJKCompatibilityForms}");
            public static ICharacterClass IsNotSmallFormVariants { get; } = new UnicodeCategory(@"\P{IsSmallFormVariants}");
            public static ICharacterClass IsNotArabicPresentationFormsB { get; } = new UnicodeCategory(@"\P{IsArabicPresentationForms-B}");
            public static ICharacterClass IsNotHalfwidthAndFullwidthForms { get; } = new UnicodeCategory(@"\P{IsHalfwidthandFullwidthForms}");
            public static ICharacterClass IsNotSpecials { get; } = new UnicodeCategory(@"\P{IsSpecials}");
        }
    }
}
