using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float speed;
    private Vector3 direction;

    public void Init(float _speed, Vector3 _direction)
    {
        speed = _speed;
        direction = _direction;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
