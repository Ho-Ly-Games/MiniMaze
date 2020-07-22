using System.Collections;
using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        private float time;
        private bool stop;


        public void StartTimer()
        {
            StartCoroutine(TimerClock());
        }

        private IEnumerator TimerClock()
        {
            time = 0;
            do
            {
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
            } while (!stop);
        }

        public float Stop()
        {
        
            stop = true;
            return time;
        }
    }
}