using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroide : Enemy
{
    public bool isRotate;
    public float velocidadAsteroide = 5f;

    private Vector3 direccion;
    // Velocidad de rotación en cada eje
    private Vector3 rotationSpeed;

    public GameObject? asteroide;
    public ParticleSystem? estela;

    void Start()
    {

        if (asteroide != null && isRotate)
        {
            asteroide.transform.rotation = Random.rotation;

            float rotSpeedX = Random.Range(-50f, 50f);
            float rotSpeedY = Random.Range(-50f, 50f);
            float rotSpeedZ = Random.Range(-50f, 50f);
            rotationSpeed = new Vector3(rotSpeedX, rotSpeedY, rotSpeedZ);
        }

    }

    public void Init(Vector3 posicionNave, float velocidadNave)
    {
        direccion = CalcularDireccionImpacto(posicionNave, transform.position, velocidadNave);
        estela.transform.rotation = Quaternion.LookRotation(-direccion);
        estela.transform.position = transform.position;
        //transform.right = direccion;
    }

    void Update()
    {
        if (isRotate)
        {
            asteroide.transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        transform.position += direccion * velocidadAsteroide * Time.deltaTime;
       
        base.UpdateHP();
    }

    Vector3 CalcularDireccionImpacto(Vector3 posicionNave, Vector3 posicionAsteroide, float velocidadNave)
    {

        Vector3 distancia = posicionNave - posicionAsteroide;
        float distanciaTotal = distancia.magnitude;

        // **Tiempo estimado de impacto**
        float t = distanciaTotal / velocidadAsteroide;

        // **Predecimos la posición futura de la nave**
        Vector3 posicionFuturaNave = posicionNave + (Vector3.forward * velocidadNave * t);

        // **Obtenemos la dirección hacia la posición futura**
        return (posicionFuturaNave - posicionAsteroide).normalized;
    }

    public override void Die()
    {
        base.Die();
        VFXController.instance.EnemyDeath(transform.position);
    }

}
