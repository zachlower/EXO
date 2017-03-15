using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPixel : MonoBehaviour {

    public Texture2D reference;

    private Texture2D copy;
    private Rect rect;
    private Vector2 size;
    private Vector3 pixelPos;
    private int oldX, oldY;



    private void Start()
    {
        oldX = oldY = 0;
        copy = Instantiate(reference);
        size = new Vector2(reference.width, reference.height);

        pixelPos = Camera.main.WorldToScreenPoint(transform.position);
        rect = new Rect(Vector2.zero, size);
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(copy, rect, new Vector2(0.5f, 0.5f));

        GetComponent<BoxCollider>().size = (Vector3)size / 100;
    }

    private void Update()
    {
        // beginning a new line, set oldX and oldY
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            || Input.GetMouseButtonDown(0))
        {
            oldX = (int)((Input.mousePosition.x - pixelPos.x) / Screen.width * size.x * 2.2 + size.x / 2);
            oldY = (int)((Input.mousePosition.y - pixelPos.y) / Screen.height * size.y * 1.6 + size.y / 2);
        }
        // continuing line, paint between old coordinates and new coordinates
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            || Input.GetMouseButton(0))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(mRay)) //writing on the texture
            {
                int xScreen = (int)((Input.mousePosition.x - pixelPos.x) / Screen.width * size.x * 2.2 + size.x / 2);
                int yScreen = (int)((Input.mousePosition.y - pixelPos.y) / Screen.height * size.y * 1.6 + size.y / 2);

                Line(oldX, oldY, xScreen, yScreen);
                copy.Apply();

                oldX = xScreen;
                oldY = yScreen;
            }
        }
    }

    // paint a patch around (x, y) based on brush size
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

    // create a solid line between old and new coordinates
    private void Line(int oldX, int oldY, int newX, int newY)
    {
        int xDiff = newX - oldX;
        int yDiff = newY - oldY;
        int dist = (int)Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);
        float xUnit = (float)xDiff / dist;
        float yUnit = (float)yDiff / dist;

        for(int i=0; i<=dist; i++)
        {
            int x = oldX + (int)(xUnit * i);
            int y = oldY + (int)(yUnit * i);
            Paint(x, y, 30);
        }
    }

    // evaluate the percentage of pixels that are the same between ref and copy
    public float Difference()
    {
        Color[] refPix = reference.GetPixels();
        Color[] cpyPix = copy.GetPixels();

        float same = 0;
        int total = 0;
        for(int i=0; i<refPix.Length; i++)
        {
            if(refPix[i] == Color.black)
            {
                if (cpyPix[i] == Color.red) //correctly drawn pixel
                    same++;
                total++;
            }else if(cpyPix[i] == Color.red) //penalty for drawing over line
            {
                same -= 0.2f;
            }
        }

        return (float)same / total;
    }
}
