using System;
using Game;
using UnityEngine;

namespace Level
{
    public class LevelListManager : MonoBehaviour
    {
        [SerializeField] private LevelCard levelCard;
        [SerializeField] private GameObject content;

        private void Awake()
        {
            foreach (var level in GameManager.StoryLevels)
            {
                var lC = Instantiate(levelCard, Vector3.zero, Quaternion.identity, content.transform);
                lC.Init(level);
            }
        }

        public void ToMainMenu()
        {
            GameManager.gameManagerRef.GoToMain();
        }
    }
}