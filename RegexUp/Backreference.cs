using System;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating backreferences.
    /// </summary>
    public sealed class Backreference : IBackreference, IExpressionEncoder
    {
        /// <summary>
        /// Creates a backreference for the group in given position.
        /// </summary>
        /// <param name="number">The position of group being referenced, starting at 1.</param>
        /// <returns>The backreference.</returns>
        public static IBackreference For(int number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number), number, Resources.InvalidBackreferenceNumber); 
            }
            return new Backreference() { Reference = number.ToString(), IsNamed = false }; 
        }
        
        /// <summary>
        /// Creates a backreference for the group with the given name.
        /// </summary>
        /// <param name="name">The name of the group being referenced.</param>
        /// <param name="useQuotes">Indicates whether the backreference should be displayed using quotes instead of angle brackets.</param>
        /// <returns>The backreference.</returns>
        public static IBackreference For(string name, bool useQuotes = false)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.InvalidCaptureGroupName, nameof(name));
            }
            Group.ValidateCaptureGroupName(nameof(name), name);
            return new Backreference() { Reference = name, IsNamed = true, UseQuotes = useQuotes };
        }

        private Backreference()
        {
        }

        public string Reference { get; set; }

        public bool IsNamed { get; set; }

        public bool UseQuotes { get; set; }

        bool IExpression.NeedsGroupedToQuantify() => false;

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            if (IsNamed)
            {
                return UseQuotes ? $@"\k'{Reference}'" : $@"\k<{Reference}>";
            }
            else
            {
                return $@"\{Reference}";
            }
        }
    }
}
