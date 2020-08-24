using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private Button resumeButton;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(resumeButton.gameObject);
    }
}