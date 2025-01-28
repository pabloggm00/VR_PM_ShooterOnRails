using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Player Settings")]
    public int speed;
    public float lookSpeed;
    public Transform playerModel;
    public bool keyboard;

    [Header("Limits & target")]
    public Transform aimTarget;
    public float offsetX = 0.05f;
    public float offsetY = 0.05f;


    float inputX, inputY;
    Vector3 velocity;

    private void Start()
    {
        
    }

    private void Update()
    {
        inputX = keyboard ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        inputY = keyboard ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        LocalMove(inputX, inputY, speed);
        RotationLook(inputX, inputY, lookSpeed);
        HorizontalLean(playerModel, inputX, 50, 0.1f);

        
    }

    void LocalMove(float x, float y, float _speed)
    {
        transform.localPosition += new Vector3(x,y,0) * _speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0 + offsetX, 1 - offsetX);
        pos.y = Mathf.Clamp(pos.y, 0 + offsetY, 1 - offsetY);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float x, float y, float _speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(x, y, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * _speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }



}
