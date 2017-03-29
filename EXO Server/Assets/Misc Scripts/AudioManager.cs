using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //music
    public AudioSource roamingMusic;
    public AudioSource combatIntroMusic;
    public AudioSource combatLoopMusic;

    //sound FX
    public AudioSource plasmidsSent;



    public void EnterCombat()
    {
        roamingMusic.Pause();
        StartCoroutine(CombatLoop());
    } 
    private IEnumerator CombatLoop()
    {
        //play combat intro, then loop
        combatIntroMusic.Play();

        yield return new WaitForSeconds(combatIntroMusic.clip.length);

        combatLoopMusic.Play();
    }
    public void ExitCombat()
    {
        combatIntroMusic.Stop();
        combatLoopMusic.Stop();

        roamingMusic.UnPause();
    }

    public void PlasmidSent()
    {
        plasmidsSent.Play();
    }
}
