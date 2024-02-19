using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;

    private bool isDragging;
    public GameObject ObjectToActive;
    public string Tag;
    public GameObject ParticleEffect;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(rectTransform.position, rectTransform.sizeDelta, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(Tag))
            {
                GameObject effect = GameObject.Instantiate(ParticleEffect, collider.gameObject.transform);
                effect.transform.SetParent(canvas.transform);
                Destroy(collider.transform.parent.gameObject);
                if (ObjectToActive != null)
                {
                    ObjectToActive.SetActive(true);
                }
                break;
            }
        }
    }
}