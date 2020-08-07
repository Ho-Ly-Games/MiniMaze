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
            controls.Enable();

            controls.Ball.Enable();
#if UNITY_ANDROID
            InputSystem.EnableDevice(Accelerometer.current);
#endif
        }


        private void FixedUpdate()
        {
#if UNITY_ANDROID
            var acceleration = Accelerometer.current.acceleration.ReadValue().normalized;
            Direction?.Invoke(acceleration.x, acceleration.y);
#else
            var direction = controls.Ball.Movement.ReadValue<Vector2>();
            //todo process inversion
            Direction?.Invoke(direction.x, direction.y);
#endif
        }


        private void OnDisable()
        {
#if UNITY_ANDROID
            InputSystem.DisableDevice(Accelerometer.current);
#endif
            controls.Ball.Disable();
            controls.Disable();
        }
    }
}