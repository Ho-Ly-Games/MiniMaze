using System;
using Game;
using UnityEngine;

namespace Level
{
    public class LevelListManager : MonoBehaviour
    {
        [SerializeField] private LevelCard levelCard;
        [SerializeField] private LevelHeader levelHeader;
        [SerializeField] private GameObject content;

        private void Awake()
        {
            GameObject currentHeader = content;
            foreach (var level in GameManager.StoryLevels)
            {
                if (level.ID % 10 == 1)
                {
                    //0, 10, 20, 30
                    var lH = Instantiate(levelHeader, Vector3.zero, Quaternion.identity, content.transform);
                    lH.SetName($"Level {level.ID}-{level.ID + 9}");
                    currentHeader = lH.content.gameObject;
                }

                var lC = Instantiate(levelCard, Vector3.zero, Quaternion.identity, currentHeader.transform);
                lC.Init(level);
            }
        }

        public void ToMainMenu()
        {
            GameManager.gameManagerRef.GoToMain();
        }
    }
}