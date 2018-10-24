namespace RegexUp
{
    /// <summary>
    /// Allows each object in a regular expression to be visited.
    /// </summary>
    public abstract class ExpressionVisitor
    {
        /// <summary>
        /// Visit the given expression.
        /// </summary>
        /// <param name="expression">The expression to visit.</param>
        public void Visit(IVisitableExpression expression)
        {
            expression.Accept(this);
        }

        public virtual void Visit(IAlternation instance) { }

        public virtual void Visit(IAnchor instance) { }

        public virtual void Visit(IBackreference instance) { }

        public virtual void Visit(IBalancedGroup instance) { }

        public virtual void Visit(ICaptureGroup instance) { }

        public virtual void Visit(ICharacterEscape instance) { }

        public virtual void Visit(ICharacterGroup instance) { }

        public virtual void Visit(ICompoundExpression instance) { }

        public virtual void Visit(ICompoundLiteral instance) { }

        public virtual void Visit(IConditionalAlternation instance) { }

        public virtual void Visit(IControlCharacterEscape instance) { }

        public virtual void Visit(IHexidecimalEscape instance) { }

        public virtual void Visit(IInlineComment instance) { }

        public virtual void Visit(IInlineOptions instance) { }

        public virtual void Visit(ILiteral instance) { }

        public virtual void Visit(INegativeLookaheadAssertion instance) { }

        public virtual void Visit(INegativeLookbehindAssertion instance) { }

        public virtual void Visit(INonbacktrackingAssertion instance) { }

        public virtual void Visit(INonCaptureGroup instance) { }

        public virtual void Visit(IOctalEscape instance) { }

        public virtual void Visit(IOptionsGroup instance) { }

        public virtual void Visit(IPositiveLookaheadAssertion instance) { }

        public virtual void Visit(IPositiveLookbehindAssertion instance) { }

        public virtual void Visit(IQuantifiedExpression instance) { }

        public virtual void Visit(IRange instance) { }

        public virtual void Visit(IRegularExpression instance) { }

        public virtual void Visit(IUnicodeCategory instance) { }

        public virtual void Visit(IUnicodeEscape instance) { }
    }
}
