using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class SettingsManager : MonoBehaviour
    {
        public struct Device
        {
            public string name;
            public Settings.ControlType binding;
        }

        private List<Device> availableDevices;

        [SerializeField] private TextMeshProUGUI controlChooser, qualityChooser;
        private int index;
        private List<string> qualities;
        private int qIndex;

        private void OnEnable()
        {
            if (GameManager.Settings == null) GameManager.Settings = new Settings();

            #region Controls

            availableDevices = new List<Device>();
            //query device control options
            var devices = InputSystem.devices.ToList();

            if (devices.Exists(d => d is Accelerometer))
                availableDevices.Add(new Device
                {
                    name = "Accelerometer", binding = Settings.ControlType.Accelerometer,
                });
            if (devices.Exists(d => d is Gamepad))
                availableDevices.Add(new Device
                {
                    name = "Gamepad", binding = Settings.ControlType.Gamepad
                });
            if (devices.Exists(d => d is Keyboard))
                availableDevices.Add(new Device
                {
                    name = "Keyboard", binding = Settings.ControlType.Keyboard
                });
            if (devices.Exists(d => d is Touchscreen))
                availableDevices.Add(new Device
                {
                    name = "OnScreen Joystick", binding = Settings.ControlType.OnScreenJoystick
                });


            var chosenDevice = availableDevices.Find(d => d.binding == GameManager.Settings.controlType);
            if (chosenDevice.name == null)
            {
                chosenDevice = availableDevices.First();
            }

            index = availableDevices.FindIndex(c => c.name == chosenDevice.name);
            controlChooser.text = availableDevices[index].name;

            #endregion

            #region Quality

            qualities = QualitySettings.names.ToList();

            qIndex = GameManager.Settings.qualitySetting;
            if (qIndex < 0)
            {
                qIndex = QualitySettings.GetQualityLevel();
            }

            qualityChooser.text = qualities[qIndex];

            QualitySettings.SetQualityLevel(qIndex);

            #endregion
        }


        public void ChangeControlSelection(int direction)
        {
            if (index + direction < 0)
                index = availableDevices.Count - 1;
            else if (index + direction >= availableDevices.Count)
                index = 0;
            else index += direction;

            //todo update settings
            controlChooser.text = availableDevices[index].name;
            GameManager.Settings.controlType = availableDevices[index].binding;
        }


        public void ChangeQualitySelection(int direction)
        {
            if (qIndex + direction < 0)
                qIndex = qualities.Count - 1;
            else if (qIndex + direction >= qualities.Count)
                qIndex = 0;
            else qIndex += direction;

            //todo update settings
            qualityChooser.text = qualities[qIndex];
            QualitySettings.SetQualityLevel(qIndex);
            GameManager.Settings.qualitySetting = qIndex;
        }

        public void BackToMain()
        {
            GameManager.gameManagerRef.GoToMain();
        }

        //todo when settings are updated, update class
        //todo when update class update db
    }
}