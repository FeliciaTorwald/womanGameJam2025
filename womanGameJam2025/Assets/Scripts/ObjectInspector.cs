using UnityEngine;
using System.Collections.Generic;

public class ObjectInspector : MonoBehaviour
{
    public Transform inspectPoint;
    public float rotationSpeed = 5f;
    public Light sceneLight;
    public float dimAmountPerObject = 0.1f; // how much darker each time
    public float minLightIntensity = 0.1f;   // prevent full black

    [Header("Sounds")]
    public AudioClip pickUpSound;
    public AudioClip putBackSound;

    private AudioSource audioSource;

    private GameObject inspectedObject;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isInspecting = false;

    private HashSet<GameObject> inspectedObjects = new HashSet<GameObject>();

    void Awake()
    {
        // Ensure we have an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isInspecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Inspectable"))
                {
                    inspectedObject = hit.collider.gameObject;
                    originalPosition = inspectedObject.transform.position;
                    originalRotation = inspectedObject.transform.rotation;

                    inspectedObject.GetComponent<Rigidbody>().isKinematic = true;
                    inspectedObject.transform.position = inspectPoint.position;
                    inspectedObject.transform.rotation = Quaternion.identity;

                    isInspecting = true;

                    // Play pick-up sound
                    if (pickUpSound != null)
                        audioSource.PlayOneShot(pickUpSound);

                    // Dim light only once per object
                    if (!inspectedObjects.Contains(inspectedObject))
                    {
                        inspectedObjects.Add(inspectedObject);
                        sceneLight.intensity = Mathf.Max(minLightIntensity, sceneLight.intensity - dimAmountPerObject);
                    }
                }
            }
        }

        if (isInspecting)
        {
            if (Input.GetMouseButton(0))
            {
                float x = Input.GetAxis("Mouse X") * rotationSpeed;
                float y = Input.GetAxis("Mouse Y") * rotationSpeed;

                inspectedObject.transform.Rotate(Vector3.up, -x, Space.World);
                inspectedObject.transform.Rotate(Vector3.right, y, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inspectedObject.transform.position = originalPosition;
                inspectedObject.transform.rotation = originalRotation;

                // Play put-back sound
                if (putBackSound != null)
                    audioSource.PlayOneShot(putBackSound);

                inspectedObject = null;
                isInspecting = false;
            }
        }
    }
}
