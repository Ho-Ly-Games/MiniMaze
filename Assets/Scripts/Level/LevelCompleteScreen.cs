﻿using System;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelCompleteScreen : MonoBehaviour
    {
        [SerializeField] private Image starsImage;

        [SerializeField] private Animator anim;

        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI nextStarAtText;
        
        [SerializeField] private Timer timer;
        [SerializeField] private MazeManager mazeManager;

        private void OnEnable()
        {
            mazeManager.DisablePause();
            time.text = TimeSpan.FromSeconds(timer.Time).ToString("mm':'ss'.'f");
            anim.SetInteger("stars", LevelInfo.StarsAchieved(timer.Time, GameManager.currentLevel.expectedTime));
        }

        public void NextStarAt(float nextStarAt)
        {
            if (nextStarAt == 0)
                nextStarAtText.text = "Congratulations!";
            else
                nextStarAtText.text = $"Next star in {TimeSpan.FromSeconds(nextStarAt).ToString("mm':'ss'.'f")}";
        }
    }
}