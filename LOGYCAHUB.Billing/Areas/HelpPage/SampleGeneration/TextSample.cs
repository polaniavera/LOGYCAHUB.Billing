using System;
using System.Diagnostics.CodeAnalysis;

namespace LOGYCAHUB.Billing.Areas.HelpPage
{
    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    public class TextSample
    {
        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            Text = text;
        }

        public string Text { get; private set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            TextSample other = obj as TextSample;
            return other != null && Text == other.Text;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Text;
        }
    }
}