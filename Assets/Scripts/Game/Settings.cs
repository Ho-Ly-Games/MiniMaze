using UnityEngine;

namespace Game
{
    public class Settings
    {
        public enum ControlType
        {
            Accelerometer, Keyboard, Gamepad, OnScreenJoystick
        }
        
        public ControlType controlType;

        public int qualitySetting;
    }
}