using System;
using UnityEngine;

namespace Level
{
    public class Goal : MonoBehaviour
    {
        internal MazeManager _mazeManager;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Ball>())
            {
                _mazeManager.Completed();
            }
        }
    }
}