// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraceEvent.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   A Trace Event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Utils
{
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// A Trace Event.
    /// </summary>
    public class TraceEvent
    {
        /// <summary>
        /// Gets or sets The arguments for a String.Format
        /// <see cref="string.Format(string,object)"/>
        /// </summary>
        public object[] Args { get; set; }

        /// <summary>
        /// Gets or sets See <see cref="TraceEventCache"/>
        /// </summary>
        public TraceEventCache Cache { get; set; }

        /// <summary>
        /// Gets or sets The format for a String.Format
        /// <see cref="string.Format(string,object)"/>
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets The id of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets The message of the event.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets The source of the event.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets The type of the trace event.
        /// <see cref="TraceEventType"/>
        /// </summary>
        public TraceEventType Type { get; set; }

        /// <summary>
        /// Convert this trace event to a string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.Cache != null)
            {
                sb.AppendFormat("[{0} {1}]", this.Cache.DateTime.ToShortTimeString(), this.Cache.DateTime.ToShortDateString());
            }

            if (this.Source != null)
            {
                sb.AppendFormat("'{0}'", this.Source);
            }

            sb.Append(" - ");
            if (this.Message == null && this.Args != null && this.Format != null)
            {
                sb.AppendFormat(this.Format, this.Args);
            }
            else
            {
                sb.Append(this.Message);
            }

            return sb.ToString();
        }
    }

}
