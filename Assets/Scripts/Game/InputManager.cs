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
            InputSystem.EnableDevice(Accelerometer.current);
        }


        private void FixedUpdate()
        {
            //var direction = controls.Ball.Movement.ReadValue<Vector2>();
            //todo process inversion
            //Direction?.Invoke(direction.x, direction.y);

            var acceleration = Accelerometer.current.acceleration.ReadValue().normalized;
            Direction?.Invoke(acceleration.x, acceleration.y);
        }


        private void OnDisable()
        {
            InputSystem.DisableDevice(Accelerometer.current);
            controls.Ball.Disable();
            controls.Disable();
        }
    }
}