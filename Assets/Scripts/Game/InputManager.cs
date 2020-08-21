using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        public static Action<float, float> Direction;

        private Controls controls;

        private void Awake()
        {
            controls = new Controls();
        }

        private void OnEnable()
        {
            if (GameManager.Settings.ControlType == Settings.Control.Accelerometer)
            {
                InputSystem.EnableDevice(Accelerometer.current);
            }
            else if (GameManager.Settings.ControlType == Settings.Control.Gamepad ||
                     GameManager.Settings.ControlType == Settings.Control.Keyboard)

            {
                controls.Enable();
                controls.Ball.Enable();
            }

#if UNITY_ANDROID
#endif
        }


        private void FixedUpdate()
        {
            if (GameManager.Settings.ControlType == Settings.Control.Accelerometer)
            {
                var acceleration = Accelerometer.current.acceleration.ReadValue().normalized;
                Direction?.Invoke(acceleration.x, acceleration.y);
            }
            else if (GameManager.Settings.ControlType == Settings.Control.Gamepad ||
                     GameManager.Settings.ControlType == Settings.Control.Keyboard)

            {
                var direction = controls.Ball.Movement.ReadValue<Vector2>();
                //todo process inversion
                Direction?.Invoke(direction.x, direction.y);
            }
        }


        private void OnDisable()
        {
            if (GameManager.Settings.ControlType == Settings.Control.Accelerometer)
            {
                InputSystem.DisableDevice(Accelerometer.current);
            }
            else if (GameManager.Settings.ControlType == Settings.Control.Gamepad ||
                     GameManager.Settings.ControlType == Settings.Control.Keyboard)

            {
                controls.Ball.Disable();
                controls.Disable();
            }
        }
    }
}