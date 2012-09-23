// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheTraceListener.cs" company="OCTGN">
//   GNU Stuff
// </copyright>
// <summary>
//   Defines the TraceEvent type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Octgn.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A Trace Listener that caches all the Traces as TraceEvents <see cref="TraceEvent(System.Diagnostics.TraceEventCache,string,System.Diagnostics.TraceEventType,int,string,object[])"/>
    /// </summary>
    public class CacheTraceListener : TraceListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheTraceListener"/> class.
        /// </summary>
        public CacheTraceListener()
        {
            this.Events = new List<TraceEvent>();
        }

        #region Delegates

        /// <summary>
        /// The event added delegate.
        /// </summary>
        /// <param name="te">
        /// The trace event
        /// </param>
        public delegate void EventAdded(TraceEvent te);

        #endregion

        /// <summary>
        /// Fires when an event is added.
        /// </summary>
        public event EventAdded OnEventAdd;

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        public List<TraceEvent> Events { get; set; }

        /// <summary>
        /// Write a trace message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public override void Write(string message)
        {
            var te = new TraceEvent
                { Cache = new TraceEventCache(), Message = message, Type = TraceEventType.Information };
            this.Events.Add(te);
            try
            {
                if (this.Events.Count > 1000)
                {
                    this.Events.RemoveAt(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            if (this.OnEventAdd != null)
            {
                this.OnEventAdd.Invoke(te);
            }
        }

        /// <summary>
        /// Write a trace line.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public override void WriteLine(string message)
        {
            var te = new TraceEvent
                         {
                             Cache = new TraceEventCache(),
                             Message = message + Environment.NewLine,
                             Type = TraceEventType.Information
                         };
            try
            {
                if (this.Events.Count > 1000)
                {
                    this.Events.RemoveAt(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            if (this.OnEventAdd != null)
            {
                this.OnEventAdd.Invoke(te);
            }
        }

        /// <summary>
        /// Trace an event.
        /// </summary>
        /// <param name="eventCache">
        /// The event cache.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public override void TraceEvent(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            var te = new TraceEvent {Cache = eventCache, Source = source, Type = eventType, Id = id, Message = message};
            this.Events.Add(te);
            try
            {
                if (this.Events.Count > 1000)
                {
                    this.Events.RemoveAt(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            if (this.OnEventAdd != null)
            {
                this.OnEventAdd.Invoke(te);
            }
        }

        /// <summary>
        /// The trace event.
        /// </summary>
        /// <param name="eventCache">
        /// The event cache.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            var te = new TraceEvent {Cache = eventCache, Source = source, Type = eventType, Id = id};
            this.Events.Add(te);
            try
            {
                if (this.Events.Count > 1000)
                {
                    this.Events.RemoveAt(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            if (this.OnEventAdd != null)
            {
                this.OnEventAdd.Invoke(te);
            }
        }

        /// <summary>
        /// The trace event.
        /// </summary>
        /// <param name="eventCache">
        /// The event cache.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public override void TraceEvent(
            TraceEventCache eventCache,
            string source,
            TraceEventType eventType,
            int id,
            string format,
            params object[] args)
        {
            var te = new TraceEvent
                         {Cache = eventCache, Source = source, Type = eventType, Id = id, Format = format, Args = args};
            this.Events.Add(te);
            try
            {
                if (this.Events.Count > 1000)
                {
                    this.Events.RemoveAt(0);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            if (this.OnEventAdd != null)
            {
                this.OnEventAdd.Invoke(te);
            }
        }
    }
}