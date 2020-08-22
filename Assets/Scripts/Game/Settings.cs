using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Settings
    {
        public enum Control
        {
            Accelerometer,
            Keyboard,
            Gamepad,
            OnScreenJoystick
        }

        [PrimaryKey] public int ID { get; set; } = 0;
        public Control ControlType { get; set; }

        public int qualitySetting = -1;

        public Settings()
        {
            ControlType = GetControlType().Item1.binding;
        }


        public Tuple<SettingsManager.Device, List<SettingsManager.Device>> GetControlType()
        {
            var availableDevices = new List<SettingsManager.Device>();
            //query device control options
            var devices = InputSystem.devices.ToList();

            if (devices.Exists(d => d is Accelerometer))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "Accelerometer", binding = Settings.Control.Accelerometer,
                });
            if (devices.Exists(d => d is Gamepad))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "Gamepad", binding = Settings.Control.Gamepad
                });
            if (devices.Exists(d => d is Keyboard))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "Keyboard", binding = Settings.Control.Keyboard
                });
#if false
            if (devices.Exists(d => d is Touchscreen))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "OnScreen Joystick", binding = Settings.Control.OnScreenJoystick
                });
#endif


            var chosenDevice = availableDevices.Find(d => d.binding == GameManager.Settings?.ControlType);
            if (chosenDevice.name == null)
            {
                chosenDevice = availableDevices.First();
            }

            return new Tuple<SettingsManager.Device, List<SettingsManager.Device>>(chosenDevice, availableDevices);
        }
    }
}