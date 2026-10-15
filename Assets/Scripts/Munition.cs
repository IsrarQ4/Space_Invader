using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Munition : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _baseDamage;
    private bool _disabled = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new UnityEngine.Vector3(0, 0, UnityEngine.Random.Range(-90, 90));
        rb.linearVelocity = transform.up * _startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Sphaghetti if statements below - make despawn ranges cleaner sometime

        if (transform.position.y >= 5 | transform.position.y <=-5 | transform.position.x >= 16f/9f * 5f | transform.position.x <= -16f/9f * 5f)     // outside the frame
        {
            this._disabled = true;      // prevent killing enemies outside of the frame (disabled functionality yet to be implemented)
        }    // yea, no bullet is going to be over 1.5 times as tall as the camera height
        if (transform.position.y >= 10 | transform.position.y <= -10 | transform.position.x >= 16f/9f * 10f | transform.position.x <= -16f/9f * 10f)
        {
            Destroy(gameObject);        // deletes objects outside the frame
        }
    }
    public void Pluh()
    {
        Debug.Log("*does literally nothing*");
    }
}
