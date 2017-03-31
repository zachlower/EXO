using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float topSpeed = 5.0f;
    public bool mobileControls = true;
    public PlasmidController pc;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (pc.gameState == PlasmidController.GameState.Sending)
            return;
        Vector2 input = new Vector2();

        if (mobileControls)
        {
            //phone accelerometer movement
            input.x = Input.acceleration.x * 2;
            input.y = Input.acceleration.y * 2;
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
