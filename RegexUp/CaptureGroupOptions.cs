namespace RegexUp
{
    /// <summary>
    /// Holds the options for creating a capture group.
    /// </summary>
    public sealed class CaptureGroupOptions
    {
        /// <summary>
        /// Gets or sets the name to give the capture group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets whether to wrap the name using quotes instead of angle brackets.
        /// </summary>
        public bool UseQuotes { get; set; }
    }
}
