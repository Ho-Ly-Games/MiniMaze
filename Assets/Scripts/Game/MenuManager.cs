using UnityEngine;

namespace Game
{
    public class MenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.gameManagerRef.LoadLevelView();
        }

        public void Settings()
        {
        }

        public void QuitGame()
        {
            GameManager.gameManagerRef.QuitGame();
        }
    }
}