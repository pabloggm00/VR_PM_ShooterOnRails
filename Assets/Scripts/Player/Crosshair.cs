using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{

    [Header("Pointer Settings")]
    public Transform pointer; 
    public RectTransform pointerHUD;
    public Image pointerImage;
    public LayerMask myLayerMask;

    private void Start()
    {
        Cursor.visible = false; 
    }

    void Update()
    {
        MovePointer();
    }

    void MovePointer()
    {
        Vector3 mousePos = Input.mousePosition;

    
       
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        pointerHUD.position = mousePos;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(pointer.position, Camera.main.transform.position));
        pointer.transform.position = new Vector3(rayPoint.x, rayPoint.y, pointer.transform.position.z);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, myLayerMask))
        {
            pointerImage.color = Color.red;
        }
        else
        {
            pointerImage.color = Color.white;
        }

    }



}
