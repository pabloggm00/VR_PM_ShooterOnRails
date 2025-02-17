using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AsteroideManager : MonoBehaviour
{
    public GameObject asteroidePrefab;
    public Transform nave;
    public CinemachineDollyCart dollyCart;
    public float timeEntreAsteroides = 2f;

    private void Start()
    {
        InvokeRepeating("LanzarAsteroide", 1f, timeEntreAsteroides);
    }

    void LanzarAsteroide()
    {
        GameObject asteroide = Instantiate(asteroidePrefab, transform.position, Quaternion.identity);
        Debug.Log(dollyCart.m_Speed);
        asteroide.GetComponent<Asteroide>().Init(nave.position, dollyCart.m_Speed);
    }
}
