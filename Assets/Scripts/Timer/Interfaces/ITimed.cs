using System;

namespace CityBuilder.Timers
{
    /// <summary>
    /// Interface that measures events in a determined range of time
    /// </summary>
    public interface ITimed
    {
        /// <summary>
        /// Timer begin event
        /// </summary>
        void OnTimerBegin();
        /// <summary>
        /// Timer end event
        /// </summary>
        void OnTimerEnd();
        /// <summary>
        /// Timer update event
        /// </summary>
        /// <param name="remainingTime">Remaining time in timespan format</param>
        /// <param name="originalTime">original time in seconds</param>
        void OnTimerUpdate(float remainingSeconds, float originalTime);
    }
}


