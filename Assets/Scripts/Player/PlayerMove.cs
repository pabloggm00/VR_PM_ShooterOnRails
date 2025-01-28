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

    [Header("Dash Settings")]
    public float dashDistance;
    public float dashSpeed;

    float inputX, inputY;
    Vector3 velocity;
    bool isDashing;

    private void Start()
    {
        
    }

    private void Update()
    {
        inputX = keyboard ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        inputY = keyboard ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (inputX < 0)
            {
                StartCoroutine(Dash(-1, playerModel));

            }else if (inputX > 0)
            {
                StartCoroutine(Dash(1, playerModel));
            }
        }


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

    IEnumerator Dash(int direction, Transform target)
    {
        isDashing = true;

        //Position
        Vector3 targetPosition = transform.localPosition + new Vector3(dashDistance * direction, 0, 0);

        //Rotation
        Vector3 targetEulerAngels = target.localEulerAngles;
        float startZRotation =targetEulerAngels.z;
        float currentZRotation = startZRotation;

        float distanceCovered = 0f; 

        while (distanceCovered < dashDistance)
        {
        
            float step = dashSpeed * Time.deltaTime;
            distanceCovered += step;

            //position
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, step);
            //lerp en un while no puede ser porque está continuamente empezando la interpolación

            //rotation
            //float t = distanceCovered / dashDistance;
            currentZRotation = Mathf.LerpAngle(startZRotation, startZRotation + 280 * direction, step);
            target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, currentZRotation);

            ClampPosition();

            yield return null; 
        }

        isDashing = false; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }



}
