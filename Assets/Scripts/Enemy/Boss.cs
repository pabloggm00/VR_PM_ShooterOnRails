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

    private bool canExploit;
    private bool canRotate = true;
    private int contParticlesTime;

    public void Init()
    {
        StartCoroutine(RotateAndExploit());
        //empieza a girar fuerte con un camshake
        canRotate = true;
    }

    private void Update()
    {

        if (canRotate)
        {
            contParticlesTime++;
            effect.SetFloat("SpawnRate", Mathf.Clamp(contParticlesTime * speedEffect, 0, 550000f));
            effect.SetFloat("TrailSpawnRate", Mathf.Clamp(contParticlesTime * speedEffect / speedOffsetTrails, 0, 60f));

            if (effect.GetFloat("SpawnRate") == 1000000f)
            {
                sizeBoss.GetComponent<Animator>().enabled = true;
            }
        }

    }

    IEnumerator RotateAndExploit()
    {
        while (!canExploit)
        {
            // Aumentar la velocidad de rotación progresivamente
            rotationSpeed = Mathf.Min(rotationSpeed + (rotationAcceleration * Time.deltaTime), maxRotationSpeed);

            // Aplicar rotación
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

            // Cuando la velocidad alcanza el máximo, activar explosión
            if (rotationSpeed >= maxRotationSpeed)
            {
                canExploit = true;
                Explode();
            }

            yield return null; // Esperar un frame antes de continuar
        }

        yield return new WaitForSeconds(0.2f);

        //dejar el humo puesto
        //lanzar asteroides
    }

    void Explode()
    {
        Debug.Log("Explota");
        //VFXController.instance.ExplosionPlanet(planeta.transform.position);
        planeta.SetActive(false);
    }
}
