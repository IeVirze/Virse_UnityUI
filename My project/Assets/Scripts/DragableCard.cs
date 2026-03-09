using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCard : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public ClothingItem clothingItem;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private GameObject dragGhost;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        Apply();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Create ghost that follows cursor
        dragGhost = new GameObject("DragGhost");
        dragGhost.transform.SetParent(canvas.transform, false);
        dragGhost.transform.SetAsLastSibling();

        RawImage ghostImg = dragGhost.AddComponent<RawImage>();
        ghostImg.texture = GetComponent<RawImage>().texture;

        RectTransform ghostRT = dragGhost.GetComponent<RectTransform>();
        ghostRT.sizeDelta = new Vector2(150, 150);

        CanvasGroup ghostCG = dragGhost.AddComponent<CanvasGroup>();
        ghostCG.alpha = 0.75f;
        ghostCG.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragGhost == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint);

        dragGhost.GetComponent<RectTransform>().localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (dragGhost != null)
        {
            Destroy(dragGhost);
            dragGhost = null;
        }

        if (eventData.position.x < Screen.width * 0.33f)
            Apply();
    }

    void Apply()
    {
        if (clothingItem == null) return;
        CharacterCreationManager manager = FindObjectOfType<CharacterCreationManager>();
        if (manager != null)
            manager.ApplyItemByCategory(clothingItem, clothingItem.category);
    }
}