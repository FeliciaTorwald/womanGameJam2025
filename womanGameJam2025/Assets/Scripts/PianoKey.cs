using TMPro;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string noteName; // e.g. "C#", "F", etc.
    public GameObject noteLabelPrefab; // prefab with a TextMeshPro text object
    public Transform labelSpawnPoint;  // empty transform above the key where label appears

    private GameObject currentLabel;

    public void PlayNote()
    {
        // TODO: Insert your existing sound-playing code here

        // Spawn or reuse note label
        if (currentLabel == null)
        {
            currentLabel = Instantiate(noteLabelPrefab, labelSpawnPoint.position, Quaternion.identity);
            currentLabel.transform.SetParent(transform); // parent to key so it moves with it if needed
        }

        var tmp = currentLabel.GetComponent<TextMeshPro>();
        if (tmp != null)
        {
            tmp.text = noteName;
        }

        // Optionally, start a coroutine here to fade out or destroy the label after a short time
    }
}
