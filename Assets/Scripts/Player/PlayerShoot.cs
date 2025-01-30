using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speedBullet;
    [SerializeField] private Transform spawnShoot;
    //[SerializeField] private ParticleSystem particleShot;


    public Transform cam;
    public float rango;
    public LayerMask ignoreMask;
    public Crosshair crosshair; // Referencia al script de la mira
    public Image pointerHUD;
    private Vector3 shootDirection;

    private GameObject poolParent;

    private List<GameObject> bulletActive = new();
    private List<GameObject> bulletPool = new();

    // Start is called before the first frame update
    void Start()
    {
        poolParent = new GameObject("Pool Parent");
    }

    // Update is called once per frame
    void Update()
    {

        DetectarObjetivo();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Disparar();
        }
    }

    private void DetectarObjetivo()
    {
        Vector3 pointerPosition = crosshair.pointer.position; // Posición de la mira
        Vector3 direction = (pointerPosition - spawnShoot.position).normalized; // Dirección desde la nave hasta la mira

        RaycastHit hit;
        if (Physics.Raycast(spawnShoot.position, direction, out hit, rango, ~ignoreMask))
        {
            shootDirection = (hit.point - spawnShoot.position).normalized; // Apuntar al impacto
            pointerHUD.color = hit.collider.CompareTag("Objetos") ? Color.red : Color.white;
        }
        else
        {
            shootDirection = direction; // Si no hay colisión, sigue la dirección de la mira
            pointerHUD.color = Color.white;
        }
    }

    public void Disparar()
    {

        // Recuperamos la posición del pointer
        Vector3 targetPosition = crosshair.pointer.position;

        // Calculamos la dirección del disparo (de spawnShoot hacia el pointer)
        Vector3 direction = (targetPosition - spawnShoot.position).normalized;

        //particleShot.gameObject.SetActive(true);

        // Comprobamos qué balas de la lista de balas en uso
        // se han descativado al colisionar y las devolvemos a la pool.
        foreach (GameObject bullet in bulletActive)
        {
            if (bullet.activeInHierarchy) continue;

            bulletPool.Add(bullet);
            bulletActive.Remove(bullet);
            break;
        }

        // Creamos una variable para almacenar la bala elegida.
        GameObject chosenBullet;


        // Si hay balas en la pool...
        if (bulletPool.Count > 0)
        {
            // Sacamos la primera y la movemos de la pool a la lista de balas en uso.
            chosenBullet = bulletPool[0];
            bulletPool.Remove(chosenBullet);
            bulletActive.Add(chosenBullet);
        }
        // Si no hay ninguna disponible...
        else
        {
            // La instanciamos para nunca quedarnos sin balas.
            chosenBullet = Instantiate(bulletPrefab, spawnShoot.position, Quaternion.identity, poolParent.transform);
            bulletActive.Add(chosenBullet);
        }


        // Configurar bala
        chosenBullet.transform.position = spawnShoot.position;
        chosenBullet.transform.rotation = Quaternion.LookRotation(shootDirection); // Rotamos la bala hacia el objetivo
        chosenBullet.SetActive(true);
        chosenBullet.GetComponent<Bullet>().Init(speedBullet, shootDirection);

    }

    private void OnDrawGizmos()
    {
        if (spawnShoot == null || crosshair == null) return;

        Gizmos.color = Color.red;
        Vector3 pointerPosition = crosshair.pointer.position;
        Vector3 direction = (pointerPosition - spawnShoot.position).normalized;

        // Dibujar la línea desde la nave hasta la mira
        Gizmos.DrawLine(spawnShoot.position, spawnShoot.position + direction * rango);

        // Dibujar un pequeño punto en la posición de la mira
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pointerPosition, 0.2f);
    }
}
