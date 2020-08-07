using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelCard : MonoBehaviour
    {
        public LevelInfo levelInfo;

        [SerializeField] private TextMeshProUGUI
            levelName,
            bestTime,
            size,
            seed;

        [SerializeField] private Image stars;

        [SerializeField] private Button button;

        public void Init(LevelInfo levelInfo)
        {
            button.onClick.AddListener(delegate { GameManager.gameManagerRef.PlayLevel(levelInfo); });

            //Copy over the stats for the level
            this.levelInfo = levelInfo;
            //show these stats on the card

            //level name
            levelName.text = levelInfo.levelName;

            if (levelInfo.time < Single.PositiveInfinity)
            {
                TimeSpan time = TimeSpan.FromSeconds(levelInfo.time);
                bestTime.text = time.ToString("mm':'ss'.'f");
            }
            else bestTime.text = "Incomplete";

            switch (levelInfo.stars)
            {
                case LevelInfo.StarsCount.ZeroStar:
                    stars.sprite = GameManager.gameManagerRef.stars0;
                    break;
                case LevelInfo.StarsCount.OneStar:
                    stars.sprite = GameManager.gameManagerRef.stars1;
                    break;
                case LevelInfo.StarsCount.TwoStar:
                    stars.sprite = GameManager.gameManagerRef.stars2;
                    break;
                case LevelInfo.StarsCount.ThreeStar:
                    stars.sprite = GameManager.gameManagerRef.stars3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            size.text = $"{levelInfo.width}x{levelInfo.height}";
            seed.text = levelInfo.seed;
        }
    }
}