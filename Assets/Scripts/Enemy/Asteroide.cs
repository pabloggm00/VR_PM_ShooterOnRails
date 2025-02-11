using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroide : Enemy
{
    public bool isRotate;

    // Velocidad de rotación en cada eje
    private Vector3 rotationSpeed;

    public GameObject? asteroide;

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

    void Update()
    {
        if (isRotate)
        {
            asteroide.transform.Rotate(rotationSpeed * Time.deltaTime);
        }
       
        base.UpdateHP();
    }

    public override void Die()
    {
        base.Die();
        VFXController.instance.EnemyDeath(transform.position);
    }

}
