using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    SelectController sc;
    bool isEnabled;
    public Vector3 youPos;
    public int ID;

	// Use this for initialization
	void Start () {
        isEnabled = true;
        gameObject.GetComponentInChildren<Text>().text = "Select";
        gameObject.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;
        sc = GameObject.Find("SelectController").GetComponent<SelectController>();
    }

    public void Disable()
    {
        isEnabled = false;
    }

    public void TaskOnClick()
    {
        Debug.Log("selected alien " + ID);
        if (isEnabled)
        {
            sc.Select(ID);
        }
    }
}
