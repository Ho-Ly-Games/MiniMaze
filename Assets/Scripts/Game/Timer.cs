using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        private float time;
        private bool stop;


        [SerializeField] private TextMeshProUGUI timer;

        public float Time
        {
            get => time;
            set
            {
                time = value;
                timer.text = TimeSpan.FromSeconds(value).ToString("mm':'ss");
            }
        }

        public void StartTimer()
        {
            StartCoroutine(TimerClock());
        }

        private IEnumerator TimerClock()
        {
            Time = 0;
            do
            {
                yield return new WaitForSeconds(0.1f);
                Time += 0.1f;
            } while (!stop);
        }

        public float Stop()
        {
            stop = true;
            return Time;
        }
    }
}