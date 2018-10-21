namespace RegexUp
{
    public static class UnicodeCategories
    {
        public static ICharacterCategory IsLetterUppercase { get; } = new Literal(@"\p{Lu}", true);
        public static ICharacterCategory IsLetterLowercase { get; } = new Literal(@"\p{Ll}", true);
        public static ICharacterCategory IsLetterTitlecase { get; } = new Literal(@"\p{Lt}", true);
        public static ICharacterCategory IsLetterModifier { get; } = new Literal(@"\p{Lm}", true);
        public static ICharacterCategory IsLetterOther { get; } = new Literal(@"\p{Lo}", true);
        public static ICharacterCategory IsLetter { get; } = new Literal(@"\p{L}", true);

        public static ICharacterCategory IsNotLetterUppercase { get; } = new Literal(@"\P{Lu}", true);
        public static ICharacterCategory IsNotLetterLowercase { get; } = new Literal(@"\P{Ll}", true);
        public static ICharacterCategory IsNotLetterTitlecase { get; } = new Literal(@"\P{Lt}", true);
        public static ICharacterCategory IsNotLetterModifier { get; } = new Literal(@"\P{Lm}", true);
        public static ICharacterCategory IsNotLetterOther { get; } = new Literal(@"\P{Lo}", true);
        public static ICharacterCategory IsNotLetter { get; } = new Literal(@"\P{L}", true);

        public static ICharacterCategory IsMarkNonspacing { get; } = new Literal(@"\p{Mn}", true);
        public static ICharacterCategory IsMarkSpacing { get; } = new Literal(@"\p{Mc}", true);
        public static ICharacterCategory IsMarkEnclosing { get; } = new Literal(@"\p{Me}", true);
        public static ICharacterCategory IsMark { get; } = new Literal(@"\p{M}", true);

        public static ICharacterCategory IsNotMarkNonspacing { get; } = new Literal(@"\P{Mn}", true);
        public static ICharacterCategory IsNotMarkSpacing { get; } = new Literal(@"\P{Mc}", true);
        public static ICharacterCategory IsNotMarkEnclosing { get; } = new Literal(@"\P{Me}", true);
        public static ICharacterCategory IsNotMark { get; } = new Literal(@"\P{M}", true);

        public static ICharacterCategory IsNumberDecimalDigit { get; } = new Literal(@"\p{Nd}", true);
        public static ICharacterCategory IsNumberLetter { get; } = new Literal(@"\p{Nl}", true);
        public static ICharacterCategory IsNumberOther { get; } = new Literal(@"\p{No}", true);
        public static ICharacterCategory IsNumber { get; } = new Literal(@"\p{N}", true);

        public static ICharacterCategory IsNotNumberDecimalDigit { get; } = new Literal(@"\P{Nd}", true);
        public static ICharacterCategory IsNotNumberLetter { get; } = new Literal(@"\P{Nl}", true);
        public static ICharacterCategory IsNotNumberOther { get; } = new Literal(@"\P{No}", true);
        public static ICharacterCategory IsNotNumber { get; } = new Literal(@"\P{N}", true);

        public static ICharacterCategory IsPunctuationConnector { get; } = new Literal(@"\p{Pc}", true);
        public static ICharacterCategory IsPunctuationDash { get; } = new Literal(@"\p{Pd}", true);
        public static ICharacterCategory IsPunctuationOpen { get; } = new Literal(@"\p{Ps}", true);
        public static ICharacterCategory IsPunctuationClose { get; } = new Literal(@"\p{Pe}", true);
        public static ICharacterCategory IsPunctuationInitialQuote { get; } = new Literal(@"\p{Pi}", true);
        public static ICharacterCategory IsPunctuationFinalQuote { get; } = new Literal(@"\p{Pf}", true);
        public static ICharacterCategory IsPunctuationOther { get; } = new Literal(@"\p{Po}", true);
        public static ICharacterCategory IsPunctuation { get; } = new Literal(@"\p{P}", true);

        public static ICharacterCategory IsNotPunctuationConnector { get; } = new Literal(@"\P{Pc}", true);
        public static ICharacterCategory IsNotPunctuationDash { get; } = new Literal(@"\P{Pd}", true);
        public static ICharacterCategory IsNotPunctuationOpen { get; } = new Literal(@"\P{Ps}", true);
        public static ICharacterCategory IsNotPunctuationClose { get; } = new Literal(@"\P{Pe}", true);
        public static ICharacterCategory IsNotPunctuationInitialQuote { get; } = new Literal(@"\P{Pi}", true);
        public static ICharacterCategory IsNotPunctuationFinalQuote { get; } = new Literal(@"\P{Pf}", true);
        public static ICharacterCategory IsNotPunctuationOther { get; } = new Literal(@"\P{Po}", true);
        public static ICharacterCategory IsNotPunctuation { get; } = new Literal(@"\P{P}", true);

        public static ICharacterCategory IsSymbolMath { get; } = new Literal(@"\p{Sm}", true);
        public static ICharacterCategory IsSymbolCurrency { get; } = new Literal(@"\p{Sc}", true);
        public static ICharacterCategory IsSymbolModifier { get; } = new Literal(@"\p{Sk}", true);
        public static ICharacterCategory IsSymbolOther { get; } = new Literal(@"\p{So}", true);
        public static ICharacterCategory IsSymbol { get; } = new Literal(@"\p{S}", true);

        public static ICharacterCategory IsNotSymbolMath { get; } = new Literal(@"\P{Sm}", true);
        public static ICharacterCategory IsNotSymbolCurrency { get; } = new Literal(@"\P{Sc}", true);
        public static ICharacterCategory IsNotSymbolModifier { get; } = new Literal(@"\P{Sk}", true);
        public static ICharacterCategory IsNotSymbolOther { get; } = new Literal(@"\P{So}", true);
        public static ICharacterCategory IsNotSymbol { get; } = new Literal(@"\P{S}", true);

        public static ICharacterCategory IsSeparatorSpace { get; } = new Literal(@"\p{Zs}", true);
        public static ICharacterCategory IsSeparatorLine { get; } = new Literal(@"\p{Zl}", true);
        public static ICharacterCategory IsSeparatorParagraph { get; } = new Literal(@"\p{Zp}", true);
        public static ICharacterCategory IsSeparator { get; } = new Literal(@"\p{Z}", true);

        public static ICharacterCategory IsNotSeparatorSpace { get; } = new Literal(@"\P{Zs}", true);
        public static ICharacterCategory IsNotSeparatorLine { get; } = new Literal(@"\P{Zl}", true);
        public static ICharacterCategory IsNotSeparatorParagraph { get; } = new Literal(@"\P{Zp}", true);
        public static ICharacterCategory IsNotSeparator { get; } = new Literal(@"\P{Z}", true);

        public static ICharacterCategory IsOtherControl { get; } = new Literal(@"\p{Cc}", true);
        public static ICharacterCategory IsOtherFormat { get; } = new Literal(@"\p{Cf}", true);
        public static ICharacterCategory IsOtherSurrogate { get; } = new Literal(@"\p{Cs}", true);
        public static ICharacterCategory IsOtherPrivateUse { get; } = new Literal(@"\p{Co}", true);
        public static ICharacterCategory IsOther { get; } = new Literal(@"\p{Cn}", true);
        public static ICharacterCategory IsControl { get; } = new Literal(@"\p{C}", true);

        public static ICharacterCategory IsNotOtherControl { get; } = new Literal(@"\P{Cc}", true);
        public static ICharacterCategory IsNotOtherFormat { get; } = new Literal(@"\P{Cf}", true);
        public static ICharacterCategory IsNotOtherSurrogate { get; } = new Literal(@"\P{Cs}", true);
        public static ICharacterCategory IsNotOtherPrivateUse { get; } = new Literal(@"\P{Co}", true);
        public static ICharacterCategory IsNotOther { get; } = new Literal(@"\P{Cn}", true);
        public static ICharacterCategory IsNotControl { get; } = new Literal(@"\P{C}", true);
    }
}
