using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroide : Enemy
{
    public bool isRotate;

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

    public void Init()
    {

        Vector3 position = new Vector3(Random.Range(-8,8), Random.Range(-5, 5), 0);

        transform.localPosition = position;
       // estela.transform.rotation = Quaternion.LookRotation(-direccion);
        estela.transform.position = transform.position;

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
