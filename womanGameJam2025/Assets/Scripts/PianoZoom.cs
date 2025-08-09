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

    // Threshold to consider zoom done
    private float zoomThreshold = 0.01f;

    void Start()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;

        if (myPianoLid != null)
            myPianoLid.canOpen = false; // start disabled
    }

    void Update()
    {
        if (zoomedIn)
        {
            transform.position = Vector3.Lerp(transform.position, pianoView.position, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, pianoView.rotation, Time.deltaTime * zoomSpeed);

            // Check if zoom is basically done
            float posDiff = Vector3.Distance(transform.position, pianoView.position);
            float rotDiff = Quaternion.Angle(transform.rotation, pianoView.rotation);

            if (posDiff < zoomThreshold && rotDiff < 0.1f)
            {
                if (myPianoLid != null && !myPianoLid.canOpen)
                    myPianoLid.canOpen = true; // enable lid interaction only when zoom done
            }

            if (Input.GetKeyDown(exitKey))
            {
                zoomedIn = false;
                if (myPianoLid != null)
                    myPianoLid.canOpen = false; // disable lid interaction when zoomed out
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, defaultPos, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, Time.deltaTime * zoomSpeed);

            if (myPianoLid != null)
                myPianoLid.canOpen = false; // ensure lid interaction disabled while zoomed out
        }
    }

    public void ZoomIn()
    {
        zoomedIn = true;
        // Don't enable lid interaction here immediately — wait until zoom finishes in Update()
    }
}
