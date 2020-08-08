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
            GameManager.gameManagerRef.GoToSettings();
        }

        public void QuitGame()
        {
            GameManager.gameManagerRef.QuitGame();
        }
    }
}