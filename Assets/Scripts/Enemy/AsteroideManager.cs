using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AsteroideManager : MonoBehaviour
{
    public CinemachinePathBase recorrido;
    public GameObject asteroideDollyCarPrefab;
    public float timeEntreAsteroides = 2f;
    public float velocidadAsteroides = 20f;

    private int contadorAsteroides;
    private int maxAsteroides = 2;

    private void Start()
    {
        InvokeRepeating("LanzarAsteroide", 0f, timeEntreAsteroides);
    }

    void LanzarAsteroide()
    {

        if (contadorAsteroides >= maxAsteroides)
        {
            CancelInvoke("LanzarAsteroide");
        }

        contadorAsteroides++;

        GameObject asteroide = Instantiate(asteroideDollyCarPrefab, transform.position, Quaternion.identity);

        asteroide.GetComponent<CinemachineDollyCart>().m_Path = recorrido;
        asteroide.GetComponent<CinemachineDollyCart>().m_Speed = velocidadAsteroides;

        asteroide.GetComponentInChildren<Asteroide>().Init();

    }


}
