using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        Camera cam = Camera.main;
        if (cam != null)
            transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
