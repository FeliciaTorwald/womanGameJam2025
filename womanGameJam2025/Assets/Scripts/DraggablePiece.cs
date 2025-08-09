using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [Header("Snap Settings")]
    public Vector3 correctPosition;
    public float correctRotation; 
    public float snapDistance = 20f;
    public float rotationSnap = 10f;

    [Header("Scatter Settings")]
    public Vector2 scatterMin = new Vector2(-200, -200);
    public Vector2 scatterMax = new Vector2(200, 200);
    public bool randomizeAtStart = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (randomizeAtStart)
        {
            ScatterPiece();
        }
    }

    void ScatterPiece()
    {
        rectTransform.anchoredPosition = new Vector2(
            Random.Range(scatterMin.x, scatterMax.x),
            Random.Range(scatterMin.y, scatterMax.y)
        );

        int randomRot = Random.Range(0, 4) * 90;
        rectTransform.rotation = Quaternion.Euler(0, 0, randomRot);
    }

    public void OnBeginDrag(PointerEventData eventData) { }

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
