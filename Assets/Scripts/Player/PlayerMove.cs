using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Player Settings")]
    public Rigidbody rb;
    public int speed;

    [Header("Limits & target")]
    public Transform lookTarget;
    public Vector3 minRotation;
    public Vector3 maxRotation;

    float inputX, inputY;
    Vector3 velocity;

    private void Start()
    {
        
    }

    private void Update()
    {
        CaptureInput();
        LockRotation();
        //transform.LookAt(lookTarget);
    }

    void CaptureInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        velocity = new Vector3(inputX, inputY, 0f) * speed;
    }

    void LockRotation()
    {
        float x = Mathf.Clamp(transform.eulerAngles.x, minRotation.x, maxRotation.x);
        float y = Mathf.Clamp(transform.eulerAngles.y, minRotation.y, maxRotation.y);
        float z = Mathf.Clamp(transform.eulerAngles.z, minRotation.z, maxRotation.z);

        transform.eulerAngles = new Vector3(x, y, z);
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = velocity * Time.fixedDeltaTime;
    }


}
