namespace RegexUp
{
    /// <summary>
    /// An alternation construct that attempts to match the first alternative if the preceding expression matches.
    /// </summary>
    public interface IConditionalAlternation : IExpression
    {
        /// <summary>
        /// Gets the expression that must match in order for the 'yes' option to be matched -or- the name or number of the capture group.
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
}
