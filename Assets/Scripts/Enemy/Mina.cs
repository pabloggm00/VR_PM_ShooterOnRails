using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : Enemy
{
    [Header("Explosión")]
    public float timeToExploit;
    public BoxCollider colliderDamage;

    [Header("Parpadeo")]
    public Material material;
    public Color emissionColor = Color.white;
    public float minIntensity = 0.2f; 
    public float maxIntensity = 2f; 
    public float blinkSpeed = 1f;
    public float baseBlinkSpeed = 1f; 
    public float maxBlinkSpeed = 0.1f; 
    private float timeOn; 
    private float timeOff;

    private bool isEmissionOn;

    [HideInInspector]
    public bool canStartCooldown;
    private float timePassed;

    private void Start()
    {
        StartCoroutine(BlinkEmission());

        InitDescription();
    }

    private void Update()
    {
        timeOff = blinkSpeed;
        timeOn = blinkSpeed;

        if (canStartCooldown)
        {
            timePassed += Time.deltaTime;

            float newBlinkSpeed = 0.1f;
            timeOn = newBlinkSpeed;
            timeOff = newBlinkSpeed;

            if (timePassed >= timeToExploit)
            {
                Exploit();
                timePassed = 0;
                canStartCooldown = false;
            }
        }

        base.UpdateHP();
    }

    void Exploit()
    {
        //activar collider
        StartCoroutine(ShowHideCollider());
        //activar particulas
        VFXController.instance.MinaExploit(transform.position);
        //ocultar mina
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator ShowHideCollider()
    {
        colliderDamage.enabled = true;

        yield return new WaitForSeconds(0.5f);

        colliderDamage.enabled = false;
    }


    public override void Die()
    {
        base.Die();
        VFXController.instance.MinaDie(transform.position);
    }
    IEnumerator BlinkEmission()
    {
        while (true)
        {
            if (isEmissionOn)
            {
                material.SetColor("_EmissionColor", emissionColor * maxIntensity);
                yield return new WaitForSeconds(timeOn);
            }
            else
            {
                material.SetColor("_EmissionColor", emissionColor * minIntensity);
                yield return new WaitForSeconds(timeOff);
            }

            isEmissionOn = !isEmissionOn; //Cambia el estado
        }
    }


}
