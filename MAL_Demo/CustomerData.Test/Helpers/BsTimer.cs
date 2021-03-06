﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerData.Test.Helpers
{

    /// <summary>
    /// Handy Helper to Time Executions of Tests
    /// <example>
    /// Here is a typical snippet, because the class supports <see cref="IDisposable"/> the time can be started at the top 
    /// of the using statement and automatically stopped in the dispose
    /// <code>
    /// // Stop Watch Created and Started
    /// using (FcTimer myTimer = new FcTimer( ... )) {
    ///     // Do something you want timed
    ///     var elapsed = myTimer.ElapsedMilliseconds;
    ///     // Stop Watch stopped in DTOR
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class BsTimer : IDisposable
    {
        /// <summary>
        /// CTOR
        /// <para>Also starts timer</para>
        /// </summary>
        public BsTimer()
        {
            Start();
        }

        #region "Stop Watch

        private System.Diagnostics.Stopwatch stopWatch;
        /// <summary>
        /// Stop watch instance
        /// <para>Do not access directly if possible</para>
        /// </summary>
        private System.Diagnostics.Stopwatch StopWatch
        {
            get { return stopWatch; }
            set { stopWatch = value; }
        }

        /// <summary>
        /// Determine if the stop watch is running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                bool b = false;
                if ((stopWatch != null) && (stopWatch.IsRunning)) b = true;
                return b;
            }
        }

        /// <summary>
        /// Returns milliseconds from a running timer
        /// </summary>
        public long ElapsedMilliseconds
        {
            get
            {
                long ms = 0;
                if ((stopWatch != null) && (stopWatch.IsRunning)) ms = stopWatch.ElapsedMilliseconds;
                return ms;
            }
        }

        /// <summary>
        /// Returns the elapsed ticks from a running timer
        /// </summary>
        public long ElaspsedTicks
        {
            get
            {
                long ticks = 0;
                if ((stopWatch != null) && (stopWatch.IsRunning)) ticks = stopWatch.ElapsedTicks;
                return ticks;
            }
        }

        #endregion

        #region "Elapsed"

        /// <summary>
        /// Display milliseconds in a nice string
        /// </summary>
        /// <param name="milliseconds">milliseconds</param>
        /// <returns>nice string</returns>
        public static string DisplayElaspsedTime(long milliseconds)
        {
            var ts = TimeSpan.FromMilliseconds(milliseconds);
            return DisplayElaspsedTime(ts);
        }

        /// <summary>
        /// Display TimeSpan in a nice string
        /// </summary>
        /// <param name="ts">TimeSpan</param>
        /// <returns>nice string</returns>
        public static string DisplayElaspsedTime(TimeSpan ts)
        {
            return string.Format("{0:dd\\.hh\\:mm\\:ss\\.fff}", ts);
        }


        #endregion

        #region "Stop Watch Methods"

        /// <summary>
        /// Called by constructor, creates a new stop watch and starts it
        /// Try not to call explictly
        /// </summary>
        public void Start()
        {
            if (stopWatch == null) stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Reset();
            stopWatch.Start();
        }

        /// <summary>
        /// Called by destructor, stops stopwatch writes log (optionally)
        /// </summary>
        /// <returns>Milliseconds Elapsed</returns>
        public long Stop()
        {
            long ms = 0;
            if (stopWatch != null)
            {
                if (stopWatch.IsRunning) stopWatch.Stop();
                ms = stopWatch.ElapsedMilliseconds;
            }
            return ms;
        }

        /// <summary>
        /// Reset but do not destroy the timer
        /// </summary>
        public void Reset()
        {
            if (this.stopWatch != null)
            {
                this.StopWatch.Reset();
            }
        }

        /// <summary>
        /// Stop and destroy the timer
        /// </summary>
        public void Cancel()
        {
            // stop any running timer
            if ((this.stopWatch != null) && (this.stopWatch.IsRunning)) stopWatch.Stop();
            // kill timer object
            this.StopWatch = null;

        }
        #endregion

        #region IDisposable Members

        bool disposed;

        /// <summary>
        /// Generic destructor
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if ((stopWatch != null) && (stopWatch.IsRunning)) Stop();
                }
            }
            disposed = true;
        }
        #endregion
    }

}
