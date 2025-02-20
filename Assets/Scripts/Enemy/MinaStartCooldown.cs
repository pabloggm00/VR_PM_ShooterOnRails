using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinaStartCooldown : MonoBehaviour
{
    public Mina mina;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mina.canStartCooldown = true;
    
        }
    }
}
