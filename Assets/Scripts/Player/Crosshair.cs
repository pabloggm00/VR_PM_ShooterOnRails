using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [Header("Pointer Settings")]
    public Transform pointer; 
    public Camera mainCamera; 
    public float distanceFromCamera = 20f;
    public Vector2 pointerOffset;
    public RectTransform pointerHUD;


    void Update()
    {
        MovePointer();
    }

    void MovePointer()
    {

        Vector3 mousePosition = Input.mousePosition;


        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Vector3 targetPosition = ray.GetPoint(distanceFromCamera);

  
        Vector3 clampedPosition = ClampToScreenBounds(targetPosition);


        pointer.position = clampedPosition;

        UpdatePointerPosition();
    }

    Vector3 ClampToScreenBounds(Vector3 worldPosition)
    {
  
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(worldPosition);


        viewportPosition.x = Mathf.Clamp01(viewportPosition.x);
        viewportPosition.y = Mathf.Clamp01(viewportPosition.y);

        viewportPosition.z = distanceFromCamera;

        Vector3 clampedWorldPosition = mainCamera.ViewportToWorldPoint(viewportPosition);


        return clampedWorldPosition;
    }

    //Actualizamos el pointer en el hud
    void UpdatePointerPosition()
    {
        if (pointerHUD == null) return;

     
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(pointer.position);

        screenPosition.x += pointerOffset.x;
        screenPosition.y += pointerOffset.y;

        pointerHUD.position = screenPosition;
    }

    private void OnDrawGizmos()
    {
        if (pointer != null)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireSphere(pointer.position, 0.2f);
        }
    }
}
