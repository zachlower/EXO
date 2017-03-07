using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    private GameController gc;
    public GameController.PlasmidType type { get; private set; }
    private SpriteRenderer sprite;


    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        sprite = GetComponent<SpriteRenderer>();

        //choose type and set sprite color accordingly
        type = (GameController.PlasmidType)Random.Range(0, 3);
        switch (type)
        {
            case GameController.PlasmidType.Red:
                sprite.color = Color.red;
                break;
            case GameController.PlasmidType.Green:
                sprite.color = Color.green;
                break;
            case GameController.PlasmidType.Blue:
                sprite.color = Color.blue;
                break;
        }

        //set random rotation
        float rotation = 15;
        transform.rotation.Set(0, 0, 1, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //item has been collected
            gc.Collect(gameObject);
        }
    }
}
