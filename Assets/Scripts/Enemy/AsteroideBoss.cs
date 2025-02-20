using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroideBoss : Enemy
{
    public Transform player; // Referencia al jugador
    public Vector2 playerMoveRange = new Vector2(5, 5); // Área de movimiento del jugador
    public float speed = 5f;

    public GameObject asteroide;
    public ParticleSystem estela;

    private Vector3 target;
    private Vector3 rotationSpeed;

    void Start()
    {



        // Asignar una posición aleatoria dentro del área de movimiento del jugador
        target = player.position + new Vector3(
            Random.Range(-playerMoveRange.x / 2, playerMoveRange.x / 2),
            Random.Range(-playerMoveRange.y / 2, playerMoveRange.y / 2),
            0
        );

       // Calcular dirección hacia la que se mueve el asteroide
        Vector3 direction = (target - transform.position).normalized;

        // Posicionar la estela detrás del asteroide
        estela.transform.position = transform.position;

        // Orientar la estela en la dirección opuesta al movimiento
        estela.transform.rotation = Quaternion.LookRotation(-direction);

        asteroide.transform.rotation = Random.rotation;

        float rotSpeedX = Random.Range(-50f, 50f);
        float rotSpeedY = Random.Range(-50f, 50f);
        float rotSpeedZ = Random.Range(-50f, 50f);
        rotationSpeed = new Vector3(rotSpeedX, rotSpeedY, rotSpeedZ);
    }

    void Update()
    {
        // Mover el asteroide hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        asteroide.transform.Rotate(rotationSpeed * Time.deltaTime);

    }


    public override void Die()
    {
        base.Die();
        VFXController.instance.EnemyDeath(transform.position);
    }
}
