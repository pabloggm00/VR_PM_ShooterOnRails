using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{


    public Camera mainCamera; // C�mara principal (controlada por Cinemachine)
    public float planeZ = 0f; // Coordenada Z del plano donde se proyectar� el puntero

    void Update()
    {
        // Obt�n la posici�n del mouse en pantalla
        Vector3 mousePosition = Input.mousePosition;

        // Convierte la posici�n del mouse a un rayo en el espacio 3D
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        // Calcula d�nde el rayo intersecta el plano Z especificado
        float distanceToPlane = (planeZ - ray.origin.z) / ray.direction.z;
        Vector3 worldPosition = ray.origin + ray.direction * distanceToPlane;

        // Calcula los l�mites de la c�mara en el plano Z
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, planeZ - mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, planeZ - mainCamera.transform.position.z));

        // Limita el puntero dentro de los bordes de la c�mara
        worldPosition.x = Mathf.Clamp(worldPosition.x, bottomLeft.x, topRight.x);
        worldPosition.y = Mathf.Clamp(worldPosition.y, bottomLeft.y, topRight.y);

        // Actualiza la posici�n del puntero
        transform.position = worldPosition;
    }

    // (Opcional) Dibuja los l�mites en la escena para visualizaci�n
    private void OnDrawGizmosSelected()
    {
        if (mainCamera == null) return;

        // Calcula los l�mites de la c�mara en el plano Z
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, planeZ - mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, planeZ - mainCamera.transform.position.z));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, new Vector3(bottomLeft.x, topRight.y, planeZ));
        Gizmos.DrawLine(new Vector3(bottomLeft.x, topRight.y, planeZ), topRight);
        Gizmos.DrawLine(topRight, new Vector3(topRight.x, bottomLeft.y, planeZ));
        Gizmos.DrawLine(new Vector3(topRight.x, bottomLeft.y, planeZ), bottomLeft);
    }
}
