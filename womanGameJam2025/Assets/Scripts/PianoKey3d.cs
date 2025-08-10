using System.Collections;
using UnityEngine;
using TMPro;

public class PianoKey3D : MonoBehaviour
{
    public string noteName = "C";
    public AudioClip noteClip;
    public float audioStartTime = 0.5f;

    public Color pressedColor = Color.yellow;
    private Color originalColor;
    private Renderer rend;

    public GameObject noteLabelPrefab;   // Prefab with TextMeshPro 3D text
    public float labelHeightOffset = 0.2f; // How much above key to spawn label
    public float textLifetime = 1f;

    private AudioSource audioSource;
    private GameObject currentLabel;
    private Coroutine fadeCoroutine;  // Track the fade coroutine

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void OnMouseDown()
    {
        PlayNote();
    }

    public void PlayNote()
    {
        if (noteClip == null)
        {
            Debug.LogWarning("No audio clip assigned!");
            return;
        }

        audioSource.clip = noteClip;
        audioSource.Play();

        StartCoroutine(SetAudioTimeAfterPlay(audioStartTime));

        rend.material.color = pressedColor;
        Invoke(nameof(ResetColor), 0.2f);

        SpawnOrUpdateNoteLabel();
    }

    private IEnumerator SetAudioTimeAfterPlay(float startTime)
    {
        yield return null;
        audioSource.time = startTime;
    }

    void ResetColor()
    {
        rend.material.color = originalColor;
    }

    void SpawnOrUpdateNoteLabel()
    {
        if (noteLabelPrefab == null)
            return;

        Vector3 spawnPos = rend.bounds.center + new Vector3(0, rend.bounds.extents.y + labelHeightOffset, 0);

        if (currentLabel == null)
        {
            currentLabel = Instantiate(noteLabelPrefab, spawnPos, Quaternion.identity);
            currentLabel.transform.SetParent(transform); // Keep label with key
        }
        else
        {
            currentLabel.transform.position = spawnPos;

            // Stop any ongoing fade coroutine and reset alpha
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                TextMeshPro tmp = currentLabel.GetComponentInChildren<TextMeshPro>();
                if (tmp != null)
                    tmp.color = Color.white;
            }
        }

        TextMeshPro textMesh = currentLabel.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = noteName;
            textMesh.color = Color.white; // reset alpha in case it was fading before
        }

        fadeCoroutine = StartCoroutine(FadeAndDestroyLabel(currentLabel, textLifetime));
    }

    private IEnumerator FadeAndDestroyLabel(GameObject label, float duration)
    {
        TextMeshPro tmp = label.GetComponentInChildren<TextMeshPro>();
        if (tmp == null) yield break;

        float elapsed = 0f;
        Color originalColor = tmp.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            tmp.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(label);
        currentLabel = null;
        fadeCoroutine = null;
    }
}
