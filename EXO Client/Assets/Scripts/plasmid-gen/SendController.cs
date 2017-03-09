using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendController : MonoBehaviour {

    private GameController gc;
    private float rotateSpeed = 60.0f;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gc.gameState == GameController.GameState.Collecting && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Send triggered");
            gc.TriggerSend();
        }
    }
}
