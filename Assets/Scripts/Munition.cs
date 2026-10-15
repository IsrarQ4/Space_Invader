using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Munition : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float _spawnSpeed;
    [SerializeField] private float _baseDamage;
    [SerializeField] private float _lifeTime;
    private bool _disabled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._disabled = false;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * _spawnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Deleting munitions that move out of camera
        float cameraScale = 5f;
        float destructionOffset = 2f;

        // pls dont freak out over this unconventional formatting i dont know a cleaner way to check position ranges :cry face emoji:
        // disable munitions that move out of the camera frame - prevents sniping enemies that haven't entered yet
        if (transform.position.y >= cameraScale                
            || transform.position.y <= -(cameraScale)
            || transform.position.x >= 16f/9f * cameraScale
            || transform.position.x <= -(16f/9f * cameraScale))
        {this._disabled = true;}      // prevent killing enemies outside of the frame (disabled functionality yet to be implemented)
        
        // delete munitions after they move a little further out of the camera frame
        // prevents munitions from dissapearing while partially visible on screen (more visually appealing)
        if (transform.position.y >= cameraScale + destructionOffset
            || transform.position.y <= -(cameraScale + destructionOffset)
            || transform.position.x >= 16f/9f * cameraScale + destructionOffset
            || transform.position.x <= -(16f/9f * cameraScale + destructionOffset))
        {Destroy(gameObject);}
    }

}


