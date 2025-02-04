using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{

    public static VFXController instance;

    public ParticleSystem particleCollisionBullet;

    private void Awake()
    {
        instance = this;
    }

    public void CollisionBullet(Vector3 position)
    {
        Instantiate(particleCollisionBullet, position, Quaternion.identity);
    }
}
