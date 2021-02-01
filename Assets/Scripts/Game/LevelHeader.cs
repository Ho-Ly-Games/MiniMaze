using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelHeader : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI showStatus;
        [SerializeField] private TextMeshProUGUI levelHeader;

        [SerializeField] private bool open = false;


        public RectTransform scrollContent;
        public Transform content;

        public void SetName(string name)
        {
            levelHeader.text = name;
        }

        public void Clicked()
        {
            if (open)
            {
                open = false;
                showStatus.text = "<";
            }
            else
            {
                open = true;
                showStatus.text = "v";
            }

            content.gameObject.SetActive(open);

            StartCoroutine(Refresh());
        }

        private IEnumerator Refresh()
        {
            yield return null;
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }
    }
}