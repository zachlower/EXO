using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    private PlasmidController pc;
    public PlasmidController.PlasmidType type { get; private set; }
    private SpriteRenderer sprite;


    private void Start()
    {
        pc = GameObject.Find("PlasmidController").GetComponent<PlasmidController>();
        sprite = GetComponent<SpriteRenderer>();

        Regenerate();
    }

    public void Regenerate()
    {
        //choose type and set sprite color accordingly
        type = (PlasmidController.PlasmidType)Random.Range(0, 3);
        switch (type)
        {
            case PlasmidController.PlasmidType.Red:
                sprite.color = Color.red;
                break;
            case PlasmidController.PlasmidType.Green:
                sprite.color = Color.green;
                break;
            case PlasmidController.PlasmidType.Blue:
                sprite.color = Color.blue;
                break;
        }

        //set random rotation
        float rotation = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pc.gameState == PlasmidController.GameState.Collecting && collision.gameObject.CompareTag("Player"))
        {
            //item has been collected
            pc.Collect(gameObject);
        }
    }
}
