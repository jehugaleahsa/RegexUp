namespace RegexUp
{
    /// <summary>
    /// Alternation constructs modify a regular expression to enable either/or matching.
    /// </summary>
    public interface IAlternation : IExpression
    {
        /// <summary>
        /// Adds the expression as an alternative.
        /// </summary>
        void Add(IExpression member);
    }

    /// <summary>
    /// An alternation construct that attempts to match the first alternative if the preceding expression matches.
    /// </summary>
    public interface IExpressionBasedConditionalAlternation : IExpression
    {
        /// <summary>
        /// Gets the expression that must match in order for the 'yes' option to be matched.
        /// </summary>
        IExpression Expression { get; }

        /// <summary>
        /// Gets the option to match if the expression matches.
        /// </summary>
        IExpression YesOption { get; }

        /// <summary>
        /// Gets the options to match if the expression does not match.
        /// </summary>
        IExpression NoOption { get; }
    }

    /// <summary>
    /// An alternation construct that attempts to matche the first alternative if the preceding capture was found.
    /// </summary>
    public interface ICaptureBasedConditionalAlternation : IExpression
    {
        /// <summary>
        /// Gets the name or number of the capture group that must match in order for the 'yes' option to be matched.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets whether the capture group is a name.
        /// </summary>
        bool IsNamed { get; }

        /// <summary>
        /// Gets the option to match if the expression matches.
        /// </summary>
        IExpression YesOption { get; }

        /// <summary>
        /// Gets the options to match if the expression does not match.
        /// </summary>
        IExpression NoOption { get; }
    }
}
