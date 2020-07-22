using System;
using UnityEngine;

namespace Level
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] internal Ball ball;
        [SerializeField] private Rigidbody2D rgd;

        private void LateUpdate()
        {
            Vector3 pos = Vector3.zero;
            if (ball != null)
                pos = ball.rgd.position + ball.rgd.velocity / 4;


            var position = transform.position;
            pos.z = position.z;
            position = Vector3.Lerp(position, pos, 0.05f);
            transform.position = position;
        }
    }
}