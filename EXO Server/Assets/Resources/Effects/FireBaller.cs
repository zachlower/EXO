using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBaller : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < .5f)
            {
                Destroy(gameObject);
            }
           else transform.position += Vector3.Normalize(target.transform.position - transform.position) * Time.deltaTime * 8.0f;
        }
        else Destroy(gameObject);
	}
}
