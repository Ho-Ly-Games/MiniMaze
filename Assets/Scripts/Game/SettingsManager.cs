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

        [SerializeField] private TextMeshProUGUI controlChooser;
        private int index;

        private void OnEnable()
        {
            if (GameManager.Settings == null) GameManager.Settings = new Settings();

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
            controlChooser.text = chosenDevice.name;
        }


        public void ChangeControlSelection(int direction)
        {
            
        }

        //todo when settings are updated, update class
        //todo when update class update db
    }
}