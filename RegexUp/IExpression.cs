namespace RegexUp
{
    /// <summary>
    /// Represents one or more sub-expressions.
    /// </summary>
    public interface IExpression
    {
        bool NeedsGroupedToQuantify();
    }
}
