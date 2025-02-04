using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroide : Enemy
{
    // Velocidad de rotación en cada eje
    private Vector3 rotationSpeed;

    public GameObject asteroide;

    void Start()
    {
        // Asigna una rotación inicial aleatoria
        asteroide.transform.rotation = Random.rotation;

        // Genera una velocidad de rotación aleatoria para cada eje
        // Puedes ajustar los rangos según la velocidad deseada
        float rotSpeedX = Random.Range(-50f, 50f);
        float rotSpeedY = Random.Range(-50f, 50f);
        float rotSpeedZ = Random.Range(-50f, 50f);
        rotationSpeed = new Vector3(rotSpeedX, rotSpeedY, rotSpeedZ);
    }

    void Update()
    {
        // Rota el asteroide de forma continua
        asteroide.transform.Rotate(rotationSpeed * Time.deltaTime);
        base.UpdateHP();
    }

  
}
