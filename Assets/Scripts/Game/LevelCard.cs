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

        [SerializeField] private Sprite
            zeroStars,
            oneStar,
            twoStars,
            threeStars;

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
                bestTime.text = time.ToString("mm':'ss");
            }
            else bestTime.text = "Incomplete";

            if (false)
                switch (levelInfo.stars)
                {
                    case LevelInfo.StartCount.ZeroStar:
                        stars.sprite = zeroStars;
                        break;
                    case LevelInfo.StartCount.OneStar:
                        stars.sprite = oneStar;
                        break;
                    case LevelInfo.StartCount.TwoStar:
                        stars.sprite = twoStars;
                        break;
                    case LevelInfo.StartCount.ThreeStar:
                        stars.sprite = threeStars;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            size.text = $"{levelInfo.width}x{levelInfo.height}";
            seed.text = levelInfo.seed;
        }
    }
}