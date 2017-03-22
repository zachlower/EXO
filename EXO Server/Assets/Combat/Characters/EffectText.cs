using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectText : MonoBehaviour {

    Text effectText;
    bool occupied = false;

    private void Start()
    {
        effectText = GetComponent<Text>();

    }

    public void ShowEffect(string effect)
    {
        if (!occupied)
        {
            effectText.text = effect;
            effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, 50);

            StartCoroutine(TextFade());
            
        }
    }
    private IEnumerator TextFade()
    {
        occupied = true;

        float totalTime = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < totalTime)
        {
            float alpha = 50; // (1.0f - elapsedTime) / totalTime * 255;
            Debug.Log("ALPHA: " + alpha);

            effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        occupied = false;
    }

    
}
