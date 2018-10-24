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
        public static IUnicodeCategory Wildcard { get; } = new UnicodeCategory(@".");

        /// <summary>
        /// Matches any word character.
        /// </summary>
        public static IUnicodeCategory Word { get; } = new UnicodeCategory(@"\w");

        /// <summary>
        /// Matches any non-word character.
        /// </summary>
        public static IUnicodeCategory NonWord { get; } = new UnicodeCategory(@"\W");

        /// <summary>
        /// Matches any white-space character.
        /// </summary>
        public static IUnicodeCategory Whitespace { get; } = new UnicodeCategory(@"\s");

        /// <summary>
        /// Matches any non-white-space character.
        /// </summary>
        public static IUnicodeCategory NonWhitespace { get; } = new UnicodeCategory(@"\S");

        /// <summary>
        /// Matches any digit character.
        /// </summary>
        public static IUnicodeCategory Digit { get; } = new UnicodeCategory(@"\d");

        /// <summary>
        /// Matches any non-digit character.
        /// </summary>
        public static IUnicodeCategory NonDigit { get; } = new UnicodeCategory(@"\D");

        /// <summary>
        /// General unicode category definitions.
        /// </summary>
        public static class UnicodeCategories
        {
            public static IUnicodeCategory IsLetterUppercase { get; } = new UnicodeCategory(@"\p{Lu}");
            public static IUnicodeCategory IsLetterLowercase { get; } = new UnicodeCategory(@"\p{Ll}");
            public static IUnicodeCategory IsLetterTitlecase { get; } = new UnicodeCategory(@"\p{Lt}");
            public static IUnicodeCategory IsLetterModifier { get; } = new UnicodeCategory(@"\p{Lm}");
            public static IUnicodeCategory IsLetterOther { get; } = new UnicodeCategory(@"\p{Lo}");
            public static IUnicodeCategory IsLetter { get; } = new UnicodeCategory(@"\p{L}");

            public static IUnicodeCategory IsNotLetterUppercase { get; } = new UnicodeCategory(@"\P{Lu}");
            public static IUnicodeCategory IsNotLetterLowercase { get; } = new UnicodeCategory(@"\P{Ll}");
            public static IUnicodeCategory IsNotLetterTitlecase { get; } = new UnicodeCategory(@"\P{Lt}");
            public static IUnicodeCategory IsNotLetterModifier { get; } = new UnicodeCategory(@"\P{Lm}");
            public static IUnicodeCategory IsNotLetterOther { get; } = new UnicodeCategory(@"\P{Lo}");
            public static IUnicodeCategory IsNotLetter { get; } = new UnicodeCategory(@"\P{L}");

            public static IUnicodeCategory IsMarkNonspacing { get; } = new UnicodeCategory(@"\p{Mn}");
            public static IUnicodeCategory IsMarkSpacing { get; } = new UnicodeCategory(@"\p{Mc}");
            public static IUnicodeCategory IsMarkEnclosing { get; } = new UnicodeCategory(@"\p{Me}");
            public static IUnicodeCategory IsMark { get; } = new UnicodeCategory(@"\p{M}");

            public static IUnicodeCategory IsNotMarkNonspacing { get; } = new UnicodeCategory(@"\P{Mn}");
            public static IUnicodeCategory IsNotMarkSpacing { get; } = new UnicodeCategory(@"\P{Mc}");
            public static IUnicodeCategory IsNotMarkEnclosing { get; } = new UnicodeCategory(@"\P{Me}");
            public static IUnicodeCategory IsNotMark { get; } = new UnicodeCategory(@"\P{M}");

            public static IUnicodeCategory IsNumberDecimalDigit { get; } = new UnicodeCategory(@"\p{Nd}");
            public static IUnicodeCategory IsNumberLetter { get; } = new UnicodeCategory(@"\p{Nl}");
            public static IUnicodeCategory IsNumberOther { get; } = new UnicodeCategory(@"\p{No}");
            public static IUnicodeCategory IsNumber { get; } = new UnicodeCategory(@"\p{N}");

            public static IUnicodeCategory IsNotNumberDecimalDigit { get; } = new UnicodeCategory(@"\P{Nd}");
            public static IUnicodeCategory IsNotNumberLetter { get; } = new UnicodeCategory(@"\P{Nl}");
            public static IUnicodeCategory IsNotNumberOther { get; } = new UnicodeCategory(@"\P{No}");
            public static IUnicodeCategory IsNotNumber { get; } = new UnicodeCategory(@"\P{N}");

            public static IUnicodeCategory IsPunctuationConnector { get; } = new UnicodeCategory(@"\p{Pc}");
            public static IUnicodeCategory IsPunctuationDash { get; } = new UnicodeCategory(@"\p{Pd}");
            public static IUnicodeCategory IsPunctuationOpen { get; } = new UnicodeCategory(@"\p{Ps}");
            public static IUnicodeCategory IsPunctuationClose { get; } = new UnicodeCategory(@"\p{Pe}");
            public static IUnicodeCategory IsPunctuationInitialQuote { get; } = new UnicodeCategory(@"\p{Pi}");
            public static IUnicodeCategory IsPunctuationFinalQuote { get; } = new UnicodeCategory(@"\p{Pf}");
            public static IUnicodeCategory IsPunctuationOther { get; } = new UnicodeCategory(@"\p{Po}");
            public static IUnicodeCategory IsPunctuation { get; } = new UnicodeCategory(@"\p{P}");

            public static IUnicodeCategory IsNotPunctuationConnector { get; } = new UnicodeCategory(@"\P{Pc}");
            public static IUnicodeCategory IsNotPunctuationDash { get; } = new UnicodeCategory(@"\P{Pd}");
            public static IUnicodeCategory IsNotPunctuationOpen { get; } = new UnicodeCategory(@"\P{Ps}");
            public static IUnicodeCategory IsNotPunctuationClose { get; } = new UnicodeCategory(@"\P{Pe}");
            public static IUnicodeCategory IsNotPunctuationInitialQuote { get; } = new UnicodeCategory(@"\P{Pi}");
            public static IUnicodeCategory IsNotPunctuationFinalQuote { get; } = new UnicodeCategory(@"\P{Pf}");
            public static IUnicodeCategory IsNotPunctuationOther { get; } = new UnicodeCategory(@"\P{Po}");
            public static IUnicodeCategory IsNotPunctuation { get; } = new UnicodeCategory(@"\P{P}");

            public static IUnicodeCategory IsSymbolMath { get; } = new UnicodeCategory(@"\p{Sm}");
            public static IUnicodeCategory IsSymbolCurrency { get; } = new UnicodeCategory(@"\p{Sc}");
            public static IUnicodeCategory IsSymbolModifier { get; } = new UnicodeCategory(@"\p{Sk}");
            public static IUnicodeCategory IsSymbolOther { get; } = new UnicodeCategory(@"\p{So}");
            public static IUnicodeCategory IsSymbol { get; } = new UnicodeCategory(@"\p{S}");

            public static IUnicodeCategory IsNotSymbolMath { get; } = new UnicodeCategory(@"\P{Sm}");
            public static IUnicodeCategory IsNotSymbolCurrency { get; } = new UnicodeCategory(@"\P{Sc}");
            public static IUnicodeCategory IsNotSymbolModifier { get; } = new UnicodeCategory(@"\P{Sk}");
            public static IUnicodeCategory IsNotSymbolOther { get; } = new UnicodeCategory(@"\P{So}");
            public static IUnicodeCategory IsNotSymbol { get; } = new UnicodeCategory(@"\P{S}");

            public static IUnicodeCategory IsSeparatorSpace { get; } = new UnicodeCategory(@"\p{Zs}");
            public static IUnicodeCategory IsSeparatorLine { get; } = new UnicodeCategory(@"\p{Zl}");
            public static IUnicodeCategory IsSeparatorParagraph { get; } = new UnicodeCategory(@"\p{Zp}");
            public static IUnicodeCategory IsSeparator { get; } = new UnicodeCategory(@"\p{Z}");

            public static IUnicodeCategory IsNotSeparatorSpace { get; } = new UnicodeCategory(@"\P{Zs}");
            public static IUnicodeCategory IsNotSeparatorLine { get; } = new UnicodeCategory(@"\P{Zl}");
            public static IUnicodeCategory IsNotSeparatorParagraph { get; } = new UnicodeCategory(@"\P{Zp}");
            public static IUnicodeCategory IsNotSeparator { get; } = new UnicodeCategory(@"\P{Z}");

            public static IUnicodeCategory IsOtherControl { get; } = new UnicodeCategory(@"\p{Cc}");
            public static IUnicodeCategory IsOtherFormat { get; } = new UnicodeCategory(@"\p{Cf}");
            public static IUnicodeCategory IsOtherSurrogate { get; } = new UnicodeCategory(@"\p{Cs}");
            public static IUnicodeCategory IsOtherPrivateUse { get; } = new UnicodeCategory(@"\p{Co}");
            public static IUnicodeCategory IsOther { get; } = new UnicodeCategory(@"\p{Cn}");
            public static IUnicodeCategory IsControl { get; } = new UnicodeCategory(@"\p{C}");

            public static IUnicodeCategory IsNotOtherControl { get; } = new UnicodeCategory(@"\P{Cc}");
            public static IUnicodeCategory IsNotOtherFormat { get; } = new UnicodeCategory(@"\P{Cf}");
            public static IUnicodeCategory IsNotOtherSurrogate { get; } = new UnicodeCategory(@"\P{Cs}");
            public static IUnicodeCategory IsNotOtherPrivateUse { get; } = new UnicodeCategory(@"\P{Co}");
            public static IUnicodeCategory IsNotOther { get; } = new UnicodeCategory(@"\P{Cn}");
            public static IUnicodeCategory IsNotControl { get; } = new UnicodeCategory(@"\P{C}");
        }

        /// <summary>
        /// Unicode named block definitions.
        /// </summary>
        public static class UnicodeNamedBlocks
        {
            public static IUnicodeCategory IsBasicLatin { get; } = new UnicodeCategory(@"\p{IsBasicLatin}");
            public static IUnicodeCategory IsLatin1Supplement { get; } = new UnicodeCategory(@"\p{IsLatin-1Supplement}");
            public static IUnicodeCategory IsLatinExtendedA { get; } = new UnicodeCategory(@"\p{IsLatinExtended-A}");
            public static IUnicodeCategory IsLatinExtendedB { get; } = new UnicodeCategory(@"\p{IsLatinExtended-B}");
            public static IUnicodeCategory IsIPAExtensions { get; } = new UnicodeCategory(@"\p{IsIPAExtensions}");
            public static IUnicodeCategory IsSpacingModifierLetters { get; } = new UnicodeCategory(@"\p{IsSpacingModifierLetters}");
            public static IUnicodeCategory IsCombiningDiacriticalMarks { get; } = new UnicodeCategory(@"\p{IsCombiningDiacriticalMarks}");
            public static IUnicodeCategory IsGreek { get; } = new UnicodeCategory(@"\p{IsGreek}");
            public static IUnicodeCategory IsGreekAndCoptic { get; } = new UnicodeCategory(@"\p{IsGreekandCoptic}");
            public static IUnicodeCategory IsCyrillic { get; } = new UnicodeCategory(@"\p{IsCyrillic}");
            public static IUnicodeCategory IsCyrillicSupplement { get; } = new UnicodeCategory(@"\p{IsCyrillicSupplement}");
            public static IUnicodeCategory IsArmenian { get; } = new UnicodeCategory(@"\p{IsArmenian}");
            public static IUnicodeCategory IsHebrew { get; } = new UnicodeCategory(@"\p{IsHebrew}");
            public static IUnicodeCategory IsArabic { get; } = new UnicodeCategory(@"\p{IsArabic}");
            public static IUnicodeCategory IsSyriac { get; } = new UnicodeCategory(@"\p{IsSyriac}");
            public static IUnicodeCategory IsThaana { get; } = new UnicodeCategory(@"\p{IsThaana}");
            public static IUnicodeCategory IsDevanagari { get; } = new UnicodeCategory(@"\p{IsDevanagari}");
            public static IUnicodeCategory IsBengali { get; } = new UnicodeCategory(@"\p{IsBengali}");
            public static IUnicodeCategory IsGurmukhi { get; } = new UnicodeCategory(@"\p{IsGurmukhi}");
            public static IUnicodeCategory IsGujarati { get; } = new UnicodeCategory(@"\p{IsGujarati}");
            public static IUnicodeCategory IsOriya { get; } = new UnicodeCategory(@"\p{IsOriya}");
            public static IUnicodeCategory IsTamil { get; } = new UnicodeCategory(@"\p{IsTamil}");
            public static IUnicodeCategory IsTelugu { get; } = new UnicodeCategory(@"\p{IsTelugu}");
            public static IUnicodeCategory IsKannada { get; } = new UnicodeCategory(@"\p{IsKannada}");
            public static IUnicodeCategory IsMalayalam { get; } = new UnicodeCategory(@"\p{IsMalayalam}");
            public static IUnicodeCategory IsSinhala { get; } = new UnicodeCategory(@"\p{IsSinhala}");
            public static IUnicodeCategory IsThai { get; } = new UnicodeCategory(@"\p{IsThai}");
            public static IUnicodeCategory IsLao { get; } = new UnicodeCategory(@"\p{IsLao}");
            public static IUnicodeCategory IsTibetan { get; } = new UnicodeCategory(@"\p{IsTibetan}");
            public static IUnicodeCategory IsMyanmar { get; } = new UnicodeCategory(@"\p{IsMyanmar}");
            public static IUnicodeCategory IsGeorgian { get; } = new UnicodeCategory(@"\p{IsGeorgian}");
            public static IUnicodeCategory IsHangulJamo { get; } = new UnicodeCategory(@"\p{IsHangulJamo}");
            public static IUnicodeCategory IsEthiopic { get; } = new UnicodeCategory(@"\p{IsEthiopic}");
            public static IUnicodeCategory IsCherokee { get; } = new UnicodeCategory(@"\p{IsCherokee}");
            public static IUnicodeCategory IsUnifiedCanadianAboriginalSyllabics { get; } = new UnicodeCategory(@"\p{IsUnifiedCanadianAboriginalSyllabics}");
            public static IUnicodeCategory IsOgham { get; } = new UnicodeCategory(@"\p{IsOgham}");
            public static IUnicodeCategory IsRunic { get; } = new UnicodeCategory(@"\p{IsRunic}");
            public static IUnicodeCategory IsTagalog { get; } = new UnicodeCategory(@"\p{IsTagalog}");
            public static IUnicodeCategory IsHanunoo { get; } = new UnicodeCategory(@"\p{IsHanunoo}");
            public static IUnicodeCategory IsBuhid { get; } = new UnicodeCategory(@"\p{IsBuhid}");
            public static IUnicodeCategory IsTagbanwa { get; } = new UnicodeCategory(@"\p{IsTagbanwa}");
            public static IUnicodeCategory IsKhmer { get; } = new UnicodeCategory(@"\p{IsKhmer}");
            public static IUnicodeCategory IsMongolian { get; } = new UnicodeCategory(@"\p{IsMongolian}");
            public static IUnicodeCategory IsLimbu { get; } = new UnicodeCategory(@"\p{IsLimbu}");
            public static IUnicodeCategory IsTaiLe { get; } = new UnicodeCategory(@"\p{IsTaiLe}");
            public static IUnicodeCategory IsKhmerSymbols { get; } = new UnicodeCategory(@"\p{IsKhmerSymbols}");
            public static IUnicodeCategory IsPhoneticExtensions { get; } = new UnicodeCategory(@"\p{IsPhoneticExtensions}");
            public static IUnicodeCategory IsLatinExtendedAdditional { get; } = new UnicodeCategory(@"\p{IsLatinExtendedAdditional}");
            public static IUnicodeCategory IsGreekExtended { get; } = new UnicodeCategory(@"\p{IsGreekExtended}");
            public static IUnicodeCategory IsGeneralPunctuation { get; } = new UnicodeCategory(@"\p{IsGeneralPunctuation}");
            public static IUnicodeCategory IsSuperscriptsAndSubscripts { get; } = new UnicodeCategory(@"\p{IsSuperscriptsandSubscripts}");
            public static IUnicodeCategory IsCurrencySymbols { get; } = new UnicodeCategory(@"\p{IsCurrencySymbols}");
            public static IUnicodeCategory IsCombiningDiacriticalMarksForSymbols { get; } = new UnicodeCategory(@"\p{IsCombiningDiacriticalMarksforSymbols}");
            public static IUnicodeCategory IsCombiningMarksForSymbols { get; } = new UnicodeCategory(@"\p{IsCombiningMarksforSymbols}");
            public static IUnicodeCategory IsLetterLikeSymbols { get; } = new UnicodeCategory(@"\p{IsLetterlikeSymbols}");
            public static IUnicodeCategory IsNumberForms { get; } = new UnicodeCategory(@"\p{IsNumberForms}");
            public static IUnicodeCategory IsArrows { get; } = new UnicodeCategory(@"\p{IsArrows}");
            public static IUnicodeCategory IsMathematicalOperators { get; } = new UnicodeCategory(@"\p{IsMathematicalOperators}");
            public static IUnicodeCategory IsMiscellaneousTechnical { get; } = new UnicodeCategory(@"\p{IsMiscellaneousTechnical}");
            public static IUnicodeCategory IsControlPictures { get; } = new UnicodeCategory(@"\p{IsControlPictures}");
            public static IUnicodeCategory IsOpticalCharacterRecognition { get; } = new UnicodeCategory(@"\p{IsOpticalCharacterRecognition}");
            public static IUnicodeCategory IsEnclosedAlphanumerics { get; } = new UnicodeCategory(@"\p{IsEnclosedAlphanumerics}");
            public static IUnicodeCategory IsBoxDrawing { get; } = new UnicodeCategory(@"\p{IsBoxDrawing}");
            public static IUnicodeCategory IsBlockElements { get; } = new UnicodeCategory(@"\p{IsBlockElements}");
            public static IUnicodeCategory IsGeometricShapes { get; } = new UnicodeCategory(@"\p{IsGeometricShapes}");
            public static IUnicodeCategory IsMiscellaneousSymbols { get; } = new UnicodeCategory(@"\p{IsMiscellaneousSymbols}");
            public static IUnicodeCategory IsDingbats { get; } = new UnicodeCategory(@"\p{IsDingbats}");
            public static IUnicodeCategory IsMiscellaneousMathematicalSymbolsA { get; } = new UnicodeCategory(@"\p{IsMiscellaneousMathematicalSymbols-A}");
            public static IUnicodeCategory IsSupplementalArrowsA { get; } = new UnicodeCategory(@"\p{IsSupplementalArrows-A}");
            public static IUnicodeCategory IsBraillePatterns { get; } = new UnicodeCategory(@"\p{IsBraillePatterns}");
            public static IUnicodeCategory IsSupplementalArrowsB { get; } = new UnicodeCategory(@"\p{IsSupplementalArrows-B}");
            public static IUnicodeCategory IsMiscellaneousMathematicalSymbolsB { get; } = new UnicodeCategory(@"\p{IsMiscellaneousMathematicalSymbols-B}");
            public static IUnicodeCategory IsSupplementalMathematicalOperators { get; } = new UnicodeCategory(@"\p{IsSupplementalMathematicalOperators}");
            public static IUnicodeCategory IsMiscellaneousSymbolsAndArrows { get; } = new UnicodeCategory(@"\p{IsMiscellaneousSymbolsandArrows}");
            public static IUnicodeCategory IsCJKRadicalsSupplement { get; } = new UnicodeCategory(@"\p{IsCJKRadicalsSupplement}");
            public static IUnicodeCategory IsKangxiRadicals { get; } = new UnicodeCategory(@"\p{IsKangxiRadicals}");
            public static IUnicodeCategory IsIdeographicDescriptionCharacters { get; } = new UnicodeCategory(@"\p{IsIdeographicDescriptionCharacters}");
            public static IUnicodeCategory IsCJKSymbolsAndPunctuation { get; } = new UnicodeCategory(@"\p{IsCJKSymbolsandPunctuation}");
            public static IUnicodeCategory IsHiragana { get; } = new UnicodeCategory(@"\p{IsHiragana}");
            public static IUnicodeCategory IsKatakana { get; } = new UnicodeCategory(@"\p{IsKatakana}");
            public static IUnicodeCategory IsBopomofo { get; } = new UnicodeCategory(@"\p{IsBopomofo}");
            public static IUnicodeCategory IsHangulCompatibilityJamo { get; } = new UnicodeCategory(@"\p{IsHangulCompatibilityJamo}");
            public static IUnicodeCategory IsKanbun { get; } = new UnicodeCategory(@"\p{IsKanbun}");
            public static IUnicodeCategory IsBopomofoExtended { get; } = new UnicodeCategory(@"\p{IsBopomofoExtended}");
            public static IUnicodeCategory IsKatakanaPhoneticExtensions { get; } = new UnicodeCategory(@"\p{IsKatakanaPhoneticExtensions}");
            public static IUnicodeCategory IsEnclosedCJKLettersAndMonths { get; } = new UnicodeCategory(@"\p{IsEnclosedCJKLettersandMonths}");
            public static IUnicodeCategory IsCJKCompatibility { get; } = new UnicodeCategory(@"\p{IsCJKCompatibility}");
            public static IUnicodeCategory IsCJKUnifiedIdeographsExtensionA { get; } = new UnicodeCategory(@"\p{IsCJKUnifiedIdeographsExtensionA}");
            public static IUnicodeCategory IsYijingHexagramSymbols { get; } = new UnicodeCategory(@"\p{IsYijingHexagramSymbols}");
            public static IUnicodeCategory IsCJKUnifiedIdeographs { get; } = new UnicodeCategory(@"\p{IsCJKUnifiedIdeographs}");
            public static IUnicodeCategory IsYiSyllables { get; } = new UnicodeCategory(@"\p{IsYiSyllables}");
            public static IUnicodeCategory IsYiRadicals { get; } = new UnicodeCategory(@"\p{IsYiRadicals}");
            public static IUnicodeCategory IsHangulSyllables { get; } = new UnicodeCategory(@"\p{IsHangulSyllables}");
            public static IUnicodeCategory IsHighSurrogates { get; } = new UnicodeCategory(@"\p{IsHighSurrogates}");
            public static IUnicodeCategory IsHighPrivateUseSurrogates { get; } = new UnicodeCategory(@"\p{IsHighPrivateUseSurrogates}");
            public static IUnicodeCategory IsLowSurrogates { get; } = new UnicodeCategory(@"\p{IsLowSurrogates}");
            public static IUnicodeCategory IsPrivateUse { get; } = new UnicodeCategory(@"\p{IsPrivateUse}");
            public static IUnicodeCategory IsPrivateUseArea { get; } = new UnicodeCategory(@"\p{IsPrivateUseArea}");
            public static IUnicodeCategory IsCJKCompatibilityIdeographs { get; } = new UnicodeCategory(@"\p{IsCJKCompatibilityIdeographs}");
            public static IUnicodeCategory IsAlphabeticPresentationForms { get; } = new UnicodeCategory(@"\p{IsAlphabeticPresentationForms}");
            public static IUnicodeCategory IsArabicPresentationFormsA { get; } = new UnicodeCategory(@"\p{IsArabicPresentationForms-A}");
            public static IUnicodeCategory IsVariationSelectors { get; } = new UnicodeCategory(@"\p{IsVariationSelectors}");
            public static IUnicodeCategory IsCombiningHalfMarks { get; } = new UnicodeCategory(@"\p{IsCombiningHalfMarks}");
            public static IUnicodeCategory IsCJKCompatibilityForms { get; } = new UnicodeCategory(@"\p{IsCJKCompatibilityForms}");
            public static IUnicodeCategory IsSmallFormVariants { get; } = new UnicodeCategory(@"\p{IsSmallFormVariants}");
            public static IUnicodeCategory IsArabicPresentationFormsB { get; } = new UnicodeCategory(@"\p{IsArabicPresentationForms-B}");
            public static IUnicodeCategory IsHalfwidthAndFullwidthForms { get; } = new UnicodeCategory(@"\p{IsHalfwidthandFullwidthForms}");
            public static IUnicodeCategory IsSpecials { get; } = new UnicodeCategory(@"\p{IsSpecials}");

            public static IUnicodeCategory IsNotBasicLatin { get; } = new UnicodeCategory(@"\P{IsBasicLatin}");
            public static IUnicodeCategory IsNotLatin1Supplement { get; } = new UnicodeCategory(@"\P{IsLatin-1Supplement}");
            public static IUnicodeCategory IsNotLatinExtendedA { get; } = new UnicodeCategory(@"\P{IsLatinExtended-A}");
            public static IUnicodeCategory IsNotLatinExtendedB { get; } = new UnicodeCategory(@"\P{IsLatinExtended-B}");
            public static IUnicodeCategory IsNotIPAExtensions { get; } = new UnicodeCategory(@"\P{IsIPAExtensions}");
            public static IUnicodeCategory IsNotSpacingModifierLetters { get; } = new UnicodeCategory(@"\P{IsSpacingModifierLetters}");
            public static IUnicodeCategory IsNotCombiningDiacriticalMarks { get; } = new UnicodeCategory(@"\P{IsCombiningDiacriticalMarks}");
            public static IUnicodeCategory IsNotGreek { get; } = new UnicodeCategory(@"\P{IsGreek}");
            public static IUnicodeCategory IsNotGreekAndCoptic { get; } = new UnicodeCategory(@"\P{IsGreekandCoptic}");
            public static IUnicodeCategory IsNotCyrillic { get; } = new UnicodeCategory(@"\P{IsCyrillic}");
            public static IUnicodeCategory IsNotCyrillicSupplement { get; } = new UnicodeCategory(@"\P{IsCyrillicSupplement}");
            public static IUnicodeCategory IsNotArmenian { get; } = new UnicodeCategory(@"\P{IsArmenian}");
            public static IUnicodeCategory IsNotHebrew { get; } = new UnicodeCategory(@"\P{IsHebrew}");
            public static IUnicodeCategory IsNotArabic { get; } = new UnicodeCategory(@"\P{IsArabic}");
            public static IUnicodeCategory IsNotSyriac { get; } = new UnicodeCategory(@"\P{IsSyriac}");
            public static IUnicodeCategory IsNotThaana { get; } = new UnicodeCategory(@"\P{IsThaana}");
            public static IUnicodeCategory IsNotDevanagari { get; } = new UnicodeCategory(@"\P{IsDevanagari}");
            public static IUnicodeCategory IsNotBengali { get; } = new UnicodeCategory(@"\P{IsBengali}");
            public static IUnicodeCategory IsNotGurmukhi { get; } = new UnicodeCategory(@"\P{IsGurmukhi}");
            public static IUnicodeCategory IsNotGujarati { get; } = new UnicodeCategory(@"\P{IsGujarati}");
            public static IUnicodeCategory IsNotOriya { get; } = new UnicodeCategory(@"\P{IsOriya}");
            public static IUnicodeCategory IsNotTamil { get; } = new UnicodeCategory(@"\P{IsTamil}");
            public static IUnicodeCategory IsNotTelugu { get; } = new UnicodeCategory(@"\P{IsTelugu}");
            public static IUnicodeCategory IsNotKannada { get; } = new UnicodeCategory(@"\P{IsKannada}");
            public static IUnicodeCategory IsNotMalayalam { get; } = new UnicodeCategory(@"\P{IsMalayalam}");
            public static IUnicodeCategory IsNotSinhala { get; } = new UnicodeCategory(@"\P{IsSinhala}");
            public static IUnicodeCategory IsNotThai { get; } = new UnicodeCategory(@"\P{IsThai}");
            public static IUnicodeCategory IsNotLao { get; } = new UnicodeCategory(@"\P{IsLao}");
            public static IUnicodeCategory IsNotTibetan { get; } = new UnicodeCategory(@"\P{IsTibetan}");
            public static IUnicodeCategory IsNotMyanmar { get; } = new UnicodeCategory(@"\P{IsMyanmar}");
            public static IUnicodeCategory IsNotGeorgian { get; } = new UnicodeCategory(@"\P{IsGeorgian}");
            public static IUnicodeCategory IsNotHangulJamo { get; } = new UnicodeCategory(@"\P{IsHangulJamo}");
            public static IUnicodeCategory IsNotEthiopic { get; } = new UnicodeCategory(@"\P{IsEthiopic}");
            public static IUnicodeCategory IsNotCherokee { get; } = new UnicodeCategory(@"\P{IsCherokee}");
            public static IUnicodeCategory IsNotUnifiedCanadianAboriginalSyllabics { get; } = new UnicodeCategory(@"\P{IsUnifiedCanadianAboriginalSyllabics}");
            public static IUnicodeCategory IsNotOgham { get; } = new UnicodeCategory(@"\P{IsOgham}");
            public static IUnicodeCategory IsNotRunic { get; } = new UnicodeCategory(@"\P{IsRunic}");
            public static IUnicodeCategory IsNotTagalog { get; } = new UnicodeCategory(@"\P{IsTagalog}");
            public static IUnicodeCategory IsNotHanunoo { get; } = new UnicodeCategory(@"\P{IsHanunoo}");
            public static IUnicodeCategory IsNotBuhid { get; } = new UnicodeCategory(@"\P{IsBuhid}");
            public static IUnicodeCategory IsNotTagbanwa { get; } = new UnicodeCategory(@"\P{IsTagbanwa}");
            public static IUnicodeCategory IsNotKhmer { get; } = new UnicodeCategory(@"\P{IsKhmer}");
            public static IUnicodeCategory IsNotMongolian { get; } = new UnicodeCategory(@"\P{IsMongolian}");
            public static IUnicodeCategory IsNotLimbu { get; } = new UnicodeCategory(@"\P{IsLimbu}");
            public static IUnicodeCategory IsNotTaiLe { get; } = new UnicodeCategory(@"\P{IsTaiLe}");
            public static IUnicodeCategory IsNotKhmerSymbols { get; } = new UnicodeCategory(@"\P{IsKhmerSymbols}");
            public static IUnicodeCategory IsNotPhoneticExtensions { get; } = new UnicodeCategory(@"\P{IsPhoneticExtensions}");
            public static IUnicodeCategory IsNotLatinExtendedAdditional { get; } = new UnicodeCategory(@"\P{IsLatinExtendedAdditional}");
            public static IUnicodeCategory IsNotGreekExtended { get; } = new UnicodeCategory(@"\P{IsGreekExtended}");
            public static IUnicodeCategory IsNotGeneralPunctuation { get; } = new UnicodeCategory(@"\P{IsGeneralPunctuation}");
            public static IUnicodeCategory IsNotSuperscriptsAndSubscripts { get; } = new UnicodeCategory(@"\P{IsSuperscriptsandSubscripts}");
            public static IUnicodeCategory IsNotCurrencySymbols { get; } = new UnicodeCategory(@"\P{IsCurrencySymbols}");
            public static IUnicodeCategory IsNotCombiningDiacriticalMarksForSymbols { get; } = new UnicodeCategory(@"\P{IsCombiningDiacriticalMarksforSymbols}");
            public static IUnicodeCategory IsNotCombiningMarksForSymbols { get; } = new UnicodeCategory(@"\P{IsCombiningMarksforSymbols}");
            public static IUnicodeCategory IsNotLetterLikeSymbols { get; } = new UnicodeCategory(@"\P{IsLetterlikeSymbols}");
            public static IUnicodeCategory IsNotNumberForms { get; } = new UnicodeCategory(@"\P{IsNumberForms}");
            public static IUnicodeCategory IsNotArrows { get; } = new UnicodeCategory(@"\P{IsArrows}");
            public static IUnicodeCategory IsNotMathematicalOperators { get; } = new UnicodeCategory(@"\P{IsMathematicalOperators}");
            public static IUnicodeCategory IsNotMiscellaneousTechnical { get; } = new UnicodeCategory(@"\P{IsMiscellaneousTechnical}");
            public static IUnicodeCategory IsNotControlPictures { get; } = new UnicodeCategory(@"\P{IsControlPictures}");
            public static IUnicodeCategory IsNotOpticalCharacterRecognition { get; } = new UnicodeCategory(@"\P{IsOpticalCharacterRecognition}");
            public static IUnicodeCategory IsNotEnclosedAlphanumerics { get; } = new UnicodeCategory(@"\P{IsEnclosedAlphanumerics}");
            public static IUnicodeCategory IsNotBoxDrawing { get; } = new UnicodeCategory(@"\P{IsBoxDrawing}");
            public static IUnicodeCategory IsNotBlockElements { get; } = new UnicodeCategory(@"\P{IsBlockElements}");
            public static IUnicodeCategory IsNotGeometricShapes { get; } = new UnicodeCategory(@"\P{IsGeometricShapes}");
            public static IUnicodeCategory IsNotMiscellaneousSymbols { get; } = new UnicodeCategory(@"\P{IsMiscellaneousSymbols}");
            public static IUnicodeCategory IsNotDingbats { get; } = new UnicodeCategory(@"\P{IsDingbats}");
            public static IUnicodeCategory IsNotMiscellaneousMathematicalSymbolsA { get; } = new UnicodeCategory(@"\P{IsMiscellaneousMathematicalSymbols-A}");
            public static IUnicodeCategory IsNotSupplementalArrowsA { get; } = new UnicodeCategory(@"\P{IsSupplementalArrows-A}");
            public static IUnicodeCategory IsNotBraillePatterns { get; } = new UnicodeCategory(@"\P{IsBraillePatterns}");
            public static IUnicodeCategory IsNotSupplementalArrowsB { get; } = new UnicodeCategory(@"\P{IsSupplementalArrows-B}");
            public static IUnicodeCategory IsNotMiscellaneousMathematicalSymbolsB { get; } = new UnicodeCategory(@"\P{IsMiscellaneousMathematicalSymbols-B}");
            public static IUnicodeCategory IsNotSupplementalMathematicalOperators { get; } = new UnicodeCategory(@"\P{IsSupplementalMathematicalOperators}");
            public static IUnicodeCategory IsNotMiscellaneousSymbolsAndArrows { get; } = new UnicodeCategory(@"\P{IsMiscellaneousSymbolsandArrows}");
            public static IUnicodeCategory IsNotCJKRadicalsSupplement { get; } = new UnicodeCategory(@"\P{IsCJKRadicalsSupplement}");
            public static IUnicodeCategory IsNotKangxiRadicals { get; } = new UnicodeCategory(@"\P{IsKangxiRadicals}");
            public static IUnicodeCategory IsNotIdeographicDescriptionCharacters { get; } = new UnicodeCategory(@"\P{IsIdeographicDescriptionCharacters}");
            public static IUnicodeCategory IsNotCJKSymbolsAndPunctuation { get; } = new UnicodeCategory(@"\P{IsCJKSymbolsandPunctuation}");
            public static IUnicodeCategory IsNotHiragana { get; } = new UnicodeCategory(@"\P{IsHiragana}");
            public static IUnicodeCategory IsNotKatakana { get; } = new UnicodeCategory(@"\P{IsKatakana}");
            public static IUnicodeCategory IsNotBopomofo { get; } = new UnicodeCategory(@"\P{IsBopomofo}");
            public static IUnicodeCategory IsNotHangulCompatibilityJamo { get; } = new UnicodeCategory(@"\P{IsHangulCompatibilityJamo}");
            public static IUnicodeCategory IsNotKanbun { get; } = new UnicodeCategory(@"\P{IsKanbun}");
            public static IUnicodeCategory IsNotBopomofoExtended { get; } = new UnicodeCategory(@"\P{IsBopomofoExtended}");
            public static IUnicodeCategory IsNotKatakanaPhoneticExtensions { get; } = new UnicodeCategory(@"\P{IsKatakanaPhoneticExtensions}");
            public static IUnicodeCategory IsNotEnclosedCJKLettersAndMonths { get; } = new UnicodeCategory(@"\P{IsEnclosedCJKLettersandMonths}");
            public static IUnicodeCategory IsNotCJKCompatibility { get; } = new UnicodeCategory(@"\P{IsCJKCompatibility}");
            public static IUnicodeCategory IsNotCJKUnifiedIdeographsExtensionA { get; } = new UnicodeCategory(@"\P{IsCJKUnifiedIdeographsExtensionA}");
            public static IUnicodeCategory IsNotYijingHexagramSymbols { get; } = new UnicodeCategory(@"\P{IsYijingHexagramSymbols}");
            public static IUnicodeCategory IsNotCJKUnifiedIdeographs { get; } = new UnicodeCategory(@"\P{IsCJKUnifiedIdeographs}");
            public static IUnicodeCategory IsNotYiSyllables { get; } = new UnicodeCategory(@"\P{IsYiSyllables}");
            public static IUnicodeCategory IsNotYiRadicals { get; } = new UnicodeCategory(@"\P{IsYiRadicals}");
            public static IUnicodeCategory IsNotHangulSyllables { get; } = new UnicodeCategory(@"\P{IsHangulSyllables}");
            public static IUnicodeCategory IsNotHighSurrogates { get; } = new UnicodeCategory(@"\P{IsHighSurrogates}");
            public static IUnicodeCategory IsNotHighPrivateUseSurrogates { get; } = new UnicodeCategory(@"\P{IsHighPrivateUseSurrogates}");
            public static IUnicodeCategory IsNotLowSurrogates { get; } = new UnicodeCategory(@"\P{IsLowSurrogates}");
            public static IUnicodeCategory IsNotPrivateUse { get; } = new UnicodeCategory(@"\P{IsPrivateUse}");
            public static IUnicodeCategory IsNotPrivateUseArea { get; } = new UnicodeCategory(@"\P{IsPrivateUseArea}");
            public static IUnicodeCategory IsNotCJKCompatibilityIdeographs { get; } = new UnicodeCategory(@"\P{IsCJKCompatibilityIdeographs}");
            public static IUnicodeCategory IsNotAlphabeticPresentationForms { get; } = new UnicodeCategory(@"\P{IsAlphabeticPresentationForms}");
            public static IUnicodeCategory IsNotArabicPresentationFormsA { get; } = new UnicodeCategory(@"\P{IsArabicPresentationForms-A}");
            public static IUnicodeCategory IsNotVariationSelectors { get; } = new UnicodeCategory(@"\P{IsVariationSelectors}");
            public static IUnicodeCategory IsNotCombiningHalfMarks { get; } = new UnicodeCategory(@"\P{IsCombiningHalfMarks}");
            public static IUnicodeCategory IsNotCJKCompatibilityForms { get; } = new UnicodeCategory(@"\P{IsCJKCompatibilityForms}");
            public static IUnicodeCategory IsNotSmallFormVariants { get; } = new UnicodeCategory(@"\P{IsSmallFormVariants}");
            public static IUnicodeCategory IsNotArabicPresentationFormsB { get; } = new UnicodeCategory(@"\P{IsArabicPresentationForms-B}");
            public static IUnicodeCategory IsNotHalfwidthAndFullwidthForms { get; } = new UnicodeCategory(@"\P{IsHalfwidthandFullwidthForms}");
            public static IUnicodeCategory IsNotSpecials { get; } = new UnicodeCategory(@"\P{IsSpecials}");
        }
    }
}
