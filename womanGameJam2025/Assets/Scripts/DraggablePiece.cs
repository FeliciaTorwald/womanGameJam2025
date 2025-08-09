using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 startPos;

    [Header("Snap Settings")]
    public Vector3 correctPosition;
    public float correctRotation; 
    public float snapDistance = 20f;
    public float rotationSnap = 10f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        startPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TrySnap();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            rectTransform.Rotate(0, 0, -90);
            TrySnap();
        }
    }

    private void TrySnap()
    {
        float dist = Vector3.Distance(rectTransform.anchoredPosition, correctPosition);
        float rotDiff = Mathf.Abs(Mathf.DeltaAngle(rectTransform.eulerAngles.z, correctRotation));

        if (dist < snapDistance && rotDiff < rotationSnap)
        {
            rectTransform.anchoredPosition = correctPosition;
            rectTransform.rotation = Quaternion.Euler(0, 0, correctRotation);
            Destroy(this); 
        }
    }
}
