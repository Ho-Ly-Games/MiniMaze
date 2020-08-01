using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelCompleteScreen : MonoBehaviour
    {
        [SerializeField] private Image starsImage;

        [SerializeField] private TextMeshProUGUI time;

        [SerializeField] private Timer timer;
        private void OnEnable()
        {
            time.text = TimeSpan.FromSeconds(timer.Time).ToString("mm':'ss");
        }
    }
}