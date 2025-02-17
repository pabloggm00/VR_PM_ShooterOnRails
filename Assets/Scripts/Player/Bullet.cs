using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float speed;
    int dmg;
    private Vector3 direction;

    public void Init(float _speed, Vector3 _direction, int _dmg)
    {
        speed = _speed;
        direction = _direction;
        dmg = _dmg;
        StartCoroutine(HideBullet());
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemigo))
        {
            enemigo.TakeDamage(dmg);
            VFXController.instance.CollisionBullet(transform.position);
            this.gameObject.SetActive(false);
        }
   
    }

    IEnumerator HideBullet()
    {
        yield return new WaitForSeconds(1.5f);

        this.gameObject.SetActive(false);
    }
}
