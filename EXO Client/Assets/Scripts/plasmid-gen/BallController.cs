using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float topSpeed = 5.0f;
    public bool mobileControls = true;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2();

        if (mobileControls)
        {
            //phone accelerometer movement
            input.x = Input.acceleration.x;
            input.y = Input.acceleration.y;
        }
        else
        {
            //keyboard movement
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
        }

        rb.AddForce(input * topSpeed);

    }
}
