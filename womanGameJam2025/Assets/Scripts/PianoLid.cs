using UnityEngine;

public class PianoLid : MonoBehaviour
{
    public Transform lid;             
    public float openAngle = 70f;     
    public float speed = 2f;          
    public bool isOpen = false;       
    public bool canOpen = false;      

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (lid == null)
        {
            lid = transform; 
        }

        closedRotation = lid.localRotation;
        openRotation = Quaternion.Euler(openAngle, lid.localEulerAngles.y, lid.localEulerAngles.z);
        lid.localRotation = closedRotation;
    }

    void Update()
    {
        if (isOpen)
            lid.localRotation = Quaternion.Lerp(lid.localRotation, openRotation, Time.deltaTime * speed);
        else
            lid.localRotation = Quaternion.Lerp(lid.localRotation, closedRotation, Time.deltaTime * speed);
    }

    void OnMouseDown()
    {
        if (!canOpen) return; 
        isOpen = !isOpen;
    }
}
