using CityBuilder.Timers;
using CityBuilder.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace CityBuilder.Controllers
{
    /// <summary>
    /// Controller in charge of the timer events
    /// </summary>
    public class TimerController : MonoBehaviourSingleton<TimerController>
    {
        /// <summary>
        /// Implementation of the initialize method
        /// </summary>
        public override void InitializeSingleton()
        {
            SetAsPersistentSingleton();
        }

        /// <summary>
        /// Start the timer event for a ITimed object
        /// </summary>
        /// <param name="timer">Timed suscribed object to be scheduled</param>
        /// <param name="duration">Timer duration</param>
        /// <param name="updateRate">Update rate of the timer</param>
        public void StartTimer(ITimed timer, float duration, float updateRate = 1)
        {
            timer?.OnTimerBegin();
            StartCoroutine(UpdateCoroutine(timer, duration, updateRate));
        }

        /// <summary>
        /// Updates the timer event in a determined time
        /// </summary>
        /// <param name="timer">Timed suscribed object to be updated ib a determined time</param>
        /// <param name="duration">Total duration of the timer</param>
        /// <param name="updateRate">Update rate of the timed object</param>
        /// <returns></returns>
        private IEnumerator UpdateCoroutine(ITimed timer, float duration, float updateRate)
        {
            DateTime endTime = DateTime.UtcNow.AddSeconds(duration);
            TimeSpan remainingTime = endTime - DateTime.UtcNow;
            while(remainingTime.TotalSeconds > 0)
            {
                timer?.OnTimerUpdate((float)remainingTime.TotalSeconds, duration);
                remainingTime = endTime - DateTime.UtcNow;
                yield return new WaitForSeconds(updateRate);
            } // end while

            timer?.OnTimerEnd();
        }
    }
}


