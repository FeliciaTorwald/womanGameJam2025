using UnityEngine;

public class PianoZoom : MonoBehaviour
{
    public Transform pianoView;   
    public float zoomSpeed = 5f;  
    public KeyCode exitKey = KeyCode.Escape;

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    private bool zoomedIn = false;

    void Start()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;
    }

    void Update()
    {
        if (zoomedIn)
        {
            transform.position = Vector3.Lerp(transform.position, pianoView.position, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, pianoView.rotation, Time.deltaTime * zoomSpeed);

            if (Input.GetKeyDown(exitKey))
            {
                zoomedIn = false;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, defaultPos, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, Time.deltaTime * zoomSpeed);
        }
    }

    public void ZoomIn()
    {
        zoomedIn = true;
    }
}
