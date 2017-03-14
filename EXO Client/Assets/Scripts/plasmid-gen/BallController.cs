using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float topSpeed = 5.0f;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Screen.orientation = ScreenOrientation.Landscape;
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2();

        //keyboard movement
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        //phone accelerometer movement
        //input.x = Input.acceleration.x; 
        //input.y = Input.acceleration.y;

        rb.AddForce(input * topSpeed);

    }
}
