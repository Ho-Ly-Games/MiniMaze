using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class Settings
    {
        public enum ControlType
        {
            Accelerometer,
            Keyboard,
            Gamepad,
            OnScreenJoystick
        }

        public ControlType controlType;

        public int qualitySetting = -1;

        public Tuple<SettingsManager.Device, List<SettingsManager.Device>> GetControlType()
        {
            var availableDevices = new List<SettingsManager.Device>();
            //query device control options
            var devices = InputSystem.devices.ToList();

            if (devices.Exists(d => d is Accelerometer))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "Accelerometer", binding = Settings.ControlType.Accelerometer,
                });
            if (devices.Exists(d => d is Gamepad))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "Gamepad", binding = Settings.ControlType.Gamepad
                });
            if (devices.Exists(d => d is Keyboard))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "Keyboard", binding = Settings.ControlType.Keyboard
                });
            if (devices.Exists(d => d is Touchscreen))
                availableDevices.Add(new SettingsManager.Device
                {
                    name = "OnScreen Joystick", binding = Settings.ControlType.OnScreenJoystick
                });


            var chosenDevice = availableDevices.Find(d => d.binding == GameManager.Settings.controlType);
            if (chosenDevice.name == null)
            {
                chosenDevice = availableDevices.First();
            }

            return new Tuple<SettingsManager.Device, List<SettingsManager.Device>>(chosenDevice, availableDevices);
        }
    }
}