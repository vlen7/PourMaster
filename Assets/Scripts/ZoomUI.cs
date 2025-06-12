using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scale = 1.1f;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale *= scale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale /= scale;
    }
}
