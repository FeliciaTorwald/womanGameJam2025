using UnityEngine;

public class PianoZoom : MonoBehaviour
{
    public Transform pianoView;
    public float zoomSpeed = 5f;
    public KeyCode exitKey = KeyCode.Escape;

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    private bool zoomedIn = false;

    public PianoLid myPianoLid;

    private float zoomThreshold = 0.01f;

    void Start()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;

        if (myPianoLid != null)
            myPianoLid.canOpen = false; 
    }

    void Update()
    {
        if (zoomedIn)
        {
            transform.position = Vector3.Lerp(transform.position, pianoView.position, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, pianoView.rotation, Time.deltaTime * zoomSpeed);

            float posDiff = Vector3.Distance(transform.position, pianoView.position);
            float rotDiff = Quaternion.Angle(transform.rotation, pianoView.rotation);

            if (posDiff < zoomThreshold && rotDiff < 0.1f)
            {
                if (myPianoLid != null && !myPianoLid.canOpen)
                    myPianoLid.canOpen = true; 
            }

            if (Input.GetKeyDown(exitKey))
            {
                zoomedIn = false;
                if (myPianoLid != null)
                    myPianoLid.canOpen = false; 
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, defaultPos, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, Time.deltaTime * zoomSpeed);

            if (myPianoLid != null)
                myPianoLid.canOpen = false; 
        }
    }

    public void ZoomIn()
    {
        zoomedIn = true;
    }
}
