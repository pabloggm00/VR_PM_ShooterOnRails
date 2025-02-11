using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{

    public static VFXController instance;
    [Header("Player")]
    public ParticleSystem diePlayer;

    [Header("Bullet")]
    public ParticleSystem particleCollisionBullet;

    [Header("Asteroide")]
    public ParticleSystem die;
    public ParticleSystem[] text;

    private void Awake()
    {
        instance = this;
    }

    public void CollisionBullet(Vector3 position)
    {
        Instanciar(particleCollisionBullet ,position);
        
    }

    public void EnemyDeath(Vector3 position)
    {
        Instanciar(die, position);

        int rnd = Random.Range(0, text.Length);

        GameObject particle = Instanciar(text[rnd], position);

        Destroy(particle, 2f);
    }

    GameObject Instanciar(ParticleSystem particula, Vector3 position)
    {
        return Instantiate(particula, position, Quaternion.identity).gameObject;
    }

    public void PlayerDeath(Vector3 position)
    {
        Instanciar(diePlayer, position);
    }
}
