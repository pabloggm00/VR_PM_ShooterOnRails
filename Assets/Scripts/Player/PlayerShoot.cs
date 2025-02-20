using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private int dmg;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speedBullet;
    [SerializeField] private Transform spawnShoot;
    [SerializeField] private float bulletFireRate;
    //[SerializeField] private ParticleSystem particleShot;
    public LayerMask myLayerMask;
    public Transform pointer;
    private Vector3 shootDirection;

    private GameObject poolParent;

    private List<GameObject> bulletActive = new();
    private List<GameObject> bulletPool = new();

    [Header("SFX")]
    public AudioClip shootSFX;

    // Start is called before the first frame update
    void Start()
    {
        poolParent = new GameObject("Pool Parent");
    }

    // Update is called once per frame
    void Update()
    {
        //shootDirection = crosshair.DetectarEnemigo();

        if (Input.GetMouseButtonDown(0) && !GameplayController.instance.onPause)
        {
            InvokeRepeating("Disparar", 0, bulletFireRate);
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Disparar");
        }

        if (GameplayController.instance.onPause)
        {
            CancelInvoke("Disparar");
        }
    }

    public void Disparar()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask);

        Vector3 hitPoint = hit.point;
     
        if (hit.collider == null)
        {
            hitPoint = pointer.position;
        }
       

        shootDirection = hitPoint - spawnShoot.position;



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
        chosenBullet.transform.LookAt(hitPoint);
        chosenBullet.SetActive(true);
        chosenBullet.GetComponent<Bullet>().Init(speedBullet, shootDirection, dmg);
        AudioManager.instance.PlaySFX(shootSFX, false);

    }

    /*private void OnDrawGizmos()
    {
        if (spawnShoot == null || crosshair == null) return;

        Gizmos.color = Color.red;
        Vector3 pointerPosition = crosshair.pointer.position;
        Vector3 direction = (pointerPosition - spawnShoot.position).normalized;

        // Dibujar la línea desde la nave hasta la mira
        Gizmos.DrawLine(Camera.main.transform.position, direction * rango);

        // Dibujar un pequeño punto en la posición de la mira
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pointerPosition, 0.2f);
    }*/
}
