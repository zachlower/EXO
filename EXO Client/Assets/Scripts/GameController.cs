using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public ClientBroadcast broadcast;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Broadcast()
    {
        broadcast.Broadcast();
        SceneManager.LoadScene("character choosing", LoadSceneMode.Single);
    }

    public void SelectCharacter(int index)
    {
        broadcast.cl.sendUpdateToServer("character:" + index);
        /*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Select Button"))
        {
            obj.GetComponent<SelectButton>().Disable();
        }*/
    }

    public void SwitchToAbilityScreen()
    {
        SceneManager.LoadScene("abilities", LoadSceneMode.Single);
    }

    public void SwitchToPlasmidScreen()
    {
        SceneManager.LoadScene("plasmid-gen", LoadSceneMode.Single);
    }

    public void SwitchToNav()
    {
        SceneManager.LoadScene("nav-map", LoadSceneMode.Single);
    }
}
