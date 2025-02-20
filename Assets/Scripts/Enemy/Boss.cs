using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Boss : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f; // Velocidad inicial de rotación
    [SerializeField] private float maxRotationSpeed = 200f; // Velocidad a la que explotará
    [SerializeField] private float rotationAcceleration = 5f; // Cuánto acelera la rotación

    public VisualEffect effect;
    public float speedEffect;
    public float speedOffsetTrails;
    public float timeToSizeMin;

    public GameObject planeta;
    public GameObject sizeBoss;
    public GameObject panelExplosion;
    public GameObject asteroidesParent;

    private bool canExploit;
    private bool canRotate;
    private int contParticlesTime;

    [Header("ShakeCamera")]
    public float intensity, frequency, duration;

    [Header("SFX")]
    public AudioClip explosion;
    public AudioClip whooshIn;

    public void Init()
    {
        StartCoroutine(RotateAndExploit());
        //empieza a girar fuerte con un camshake
        canRotate = true;
        AudioManager.instance.PlaySFX(whooshIn, false);
    }

    private void Update()
    {

        if (canRotate)
        {
            contParticlesTime++;
            effect.SetFloat("SpawnRate", Mathf.Clamp(contParticlesTime * speedEffect, 0, 1313500f));
            effect.SetFloat("TrailSpawnRate", Mathf.Clamp(contParticlesTime * speedEffect / speedOffsetTrails, 0, 60f));

        }

    }

    IEnumerator RotateAndExploit()
    {
        while (!canExploit)
        {
            rotationSpeed = Mathf.Min(rotationSpeed + (rotationAcceleration * Time.deltaTime), maxRotationSpeed);

            planeta.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
              

            if (rotationSpeed >= maxRotationSpeed)
            {
                canExploit = true;
                sizeBoss.GetComponent<Animator>().enabled = true;
              
            }

            yield return null; 
        }

        yield return new WaitForSeconds(1f);

        planeta.SetActive(false);
        CameraShake.Instance.Shake(intensity, frequency, duration);
        sizeBoss.GetComponent<Animator>().SetTrigger("Big");
        AudioManager.instance.PlaySFX(explosion, false);


        yield return new WaitForSeconds(0.2f);

        Explode();
      

        yield return new WaitForSeconds(0.5f);
            
        asteroidesParent.SetActive(true);
        
        //lanzar asteroides
    }

    public void HideBall()
    {
        sizeBoss.SetActive(false);
    }

    void Explode()
    {
        
        panelExplosion.GetComponent<Animator>().enabled = true;
        Debug.Log("Explota");
 
        //VFXController.instance.ExplosionPlanet(planeta.transform.position);
        
    }
}
