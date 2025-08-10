using UnityEngine;
using System.Collections;

public class SusTeacup : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offsetFromCamera = new Vector3(0, -0.2f, 1f);
    public float moveSpeed = 5f;

    [Header("Drink Effects")]
    public AudioClip slurpClip;
    public ParticleSystem funEffect;

    public float throwForce = 5f;
    public float throwUpForce = 2f;
    public float throwTorque = 300f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isInteracting = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        // Ensure we have an AudioSource for playing sounds
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void OnMouseDown()
    {
        if (!isInteracting)
        {
            StartCoroutine(DrinkAndThrow());
        }
    }

    IEnumerator DrinkAndThrow()
    {
        isInteracting = true;

        // Disable physics while moving to camera
        rb.isKinematic = true;
        rb.useGravity = false;

        // Move to camera front
        Vector3 targetPos = cameraTransform.position + cameraTransform.TransformDirection(offsetFromCamera);
        Quaternion targetRot = Quaternion.LookRotation(cameraTransform.forward);

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;

        // Rotate forward to simulate drinking
        Quaternion drinkRotation = targetRot * Quaternion.Euler(-45f, 0f, 0f);

        float rotateSpeed = 2f;
        float t = 0f;
        while (t < 1f)
        {
            transform.rotation = Quaternion.Slerp(targetRot, drinkRotation, t);
            t += Time.deltaTime * rotateSpeed;
            yield return null;
        }
        transform.rotation = drinkRotation;

        // Play slurp sound
        if (slurpClip != null)
            audioSource.PlayOneShot(slurpClip);

        // Play particle effect
        if (funEffect != null)
            funEffect.Play();

        // Wait for sound length (or default 1 sec)
        yield return new WaitForSeconds(slurpClip != null ? slurpClip.length : 1f);

        // Enable physics for throwing
        rb.isKinematic = false;
        rb.useGravity = true;

        // Throw forward + up with spin
        Vector3 throwDirection = cameraTransform.forward * throwForce + Vector3.up * throwUpForce;
        rb.AddForce(throwDirection, ForceMode.VelocityChange);
        rb.AddTorque(Random.insideUnitSphere * throwTorque);

        // Optional: destroy after a delay
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

        isInteracting = false;
    }
}
