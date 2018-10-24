namespace RegexUp
{
    /// <summary>
    /// Match any character falling within a standard Unicode general category.
    /// </summary>
    public interface IUnicodeCategory : ICharacterClass
    {
        /// <summary>
        /// Gets the literal value of the unicode category.
        /// </summary>
        string Value { get; }
    }
}
