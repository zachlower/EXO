using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPixel : MonoBehaviour {

    public Texture2D reference;

    private Texture2D copy;
    private Rect rect;
    Vector2 size;


    private void Start()
    {
        copy = Instantiate(reference);
        size = new Vector2(reference.width, reference.height);

        rect = new Rect(Vector2.zero, size);
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(copy, rect, new Vector2(0.5f, 0.5f));

        GetComponent<BoxCollider>().size = (Vector3)size / 100;
        Debug.Log("RESOLUTION: " + Screen.currentResolution);
    }

    private void Update()
    {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            || Input.GetMouseButton(0))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if(Physics.Raycast(mRay)) //writing on the texture
            {

                Vector3 pixelPos = Camera.main.WorldToScreenPoint(transform.position);

                Debug.Log(Input.mousePosition.y);
                int xScreen = (int)((Input.mousePosition.x - pixelPos.x) / Screen.width * size.x * 2 + size.x / 2);
                int yScreen = (int)((Input.mousePosition.y - pixelPos.y) / Screen.height * size.y * 2 + size.y / 2);

                Paint(xScreen, yScreen, 10);

                copy.Apply();
                //GetComponent<SpriteRenderer>().sprite = Sprite.Create(newBlank, rect, Vector2.zero);
            }
        }
    }

    private void Paint(int x, int y, int brushSize)
    {
        for(int i=0; i<brushSize; i++)
        {
            for(int j=0; j<brushSize; j++)
            {
                copy.SetPixel(x + i, y + j, Color.red);
            }
        }
    }
}
