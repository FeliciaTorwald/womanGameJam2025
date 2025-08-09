using UnityEngine;

public class InteractionPiano : MonoBehaviour
{
    private void OnMouseDown()
    {
        Camera.main.GetComponent<PianoZoom>().ZoomIn();

    }
}
