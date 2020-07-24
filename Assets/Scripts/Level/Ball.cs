using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Level;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] internal Rigidbody2D rgd;

    [SerializeField] private float speed;

    [SerializeField] private GameObject arrow;

    internal Goal goal;

    private void OnEnable()
    {
        InputManager.Direction += Direction;
    }

    private void OnDisable()
    {
        InputManager.Direction -= Direction;
    }

    private void Direction(float x, float y)
    {
        rgd.velocity = Vector2.Lerp(rgd.velocity, new Vector2(x, y) * speed, 0.1f);
    }

    private void FixedUpdate()
    {
        if (goal != null)
        {
            Vector2 direction = new Vector2(goal.transform.position.x - rgd.position.x,
                goal.transform.position.y - rgd.position.y);
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}