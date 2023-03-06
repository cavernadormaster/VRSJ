using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    SpriteRenderer rend;

    public float fadeToRedAmount = 0f;
    public float fadingSpeed = 0.05f;
    IEnumerator FadeIn()
    {
        for (float f = 1f; f <= fadeToRedAmount; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.g = f;
            c.b = f;
            rend.material.color = c;
            yield return new WaitForSeconds(fadingSpeed);
        }
    }
    IEnumerator FadeOut()
    {
        for (float f = 1f; f <= fadeToRedAmount; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.g = f;
            c.b = f;
            rend.material.color = c;
            yield return new WaitForSeconds(fadingSpeed);
        }
    }

    void Start()
    {
        // pega a cor do material para dar fade na tela
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.g = 1f;
        c.b = 1f;
        rend.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if(Walk.safe)
        {
            StartFadinOut();
        }else if(!Walk.safe)
        {
            StarFadingIn();
        }
    }
    public void StarFadingIn()
    {
        StartCoroutine("FadeIn");
    }
    public void StartFadinOut()
    {
        StartCoroutine("FadeOut");
    }
}
