using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendController : MonoBehaviour {

    private PlasmidController pc;
    private float rotateSpeed = 60.0f;

    private void Start()
    {
        pc = GameObject.Find("PlasmidController").GetComponent<PlasmidController>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pc.gameState == PlasmidController.GameState.Collecting && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Send triggered");
            pc.TriggerSend();
        }
    }
}
