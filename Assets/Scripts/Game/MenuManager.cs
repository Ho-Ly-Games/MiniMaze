using UnityEngine;

namespace Game
{
    public class MenuManager : MonoBehaviour
    {
        public void StartGame()
        {
        }

        public void SwitchToMenu(Canvas newMenu)
        {
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}