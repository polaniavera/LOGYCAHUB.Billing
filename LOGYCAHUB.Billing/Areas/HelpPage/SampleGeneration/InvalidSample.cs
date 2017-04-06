using System;
using System.Diagnostics.CodeAnalysis;

namespace LOGYCAHUB.Billing.Areas.HelpPage
{
    /// <summary>
    /// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with this class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    public class InvalidSample
    {
        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            InvalidSample other = obj as InvalidSample;
            return other != null && ErrorMessage == other.ErrorMessage;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}