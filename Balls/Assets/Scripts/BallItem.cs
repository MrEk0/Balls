using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BallItem : MonoBehaviour
{
    [SerializeField] Ball ball;
    
    private float pointsForDestroy;

    private Sprite sprite;
    private Rigidbody2D rd;
    private Vector2 speed;

    public float startMultiplier {private get; set; } = 1f;
    public event Action<float> OnTouch;

    private void OnEnable()
    {
        GameManager.instance.OnTimeDown += SetSpeed;
    }

    private void OnDisable()
    {
        GameManager.instance.OnTimeDown -= SetSpeed;
    }

    private void Awake()
    {
        pointsForDestroy = ball.pointsForDestroy;
        GetComponent<SpriteRenderer>().sprite = ball.sprite;
        rd = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = new Vector2(0, ball.speed * startMultiplier);
    }

    private void FixedUpdate()
    {
        rd.velocity = speed;
    }

    private void OnMouseDown()
    {
        OnTouch(pointsForDestroy);
        Destroy(gameObject);
    }

    public void SetSpeed(float multiplier)
    {
        float newSpeed = speed.y * multiplier;
        speed = new Vector2(0, newSpeed);
    }
}
