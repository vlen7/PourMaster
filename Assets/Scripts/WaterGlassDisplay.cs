using System;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaterGlassDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image cup;
    [SerializeField] private Image water;
    [SerializeField] private TextMeshProUGUI number;

    public WaterGlass waterGlass;

    private void Start()
    {
        ShowGlass();
    }

    public void AddWater(float height)
    {
        waterGlass.waterHeight += height;
        ShowGlass();
    }

    private void Update()
    {
    }

    public void ShowGlass()
    {
        var cupScale = 1 + (waterGlass.cupHeight / 5);
        transform.localScale = new Vector2(cupScale, cupScale);
        var childWater = transform.Find("Water");
        childWater.transform.localScale = new Vector2(1, waterGlass.waterHeight / waterGlass.cupHeight);
        number.text = $"{waterGlass.waterHeight}/{waterGlass.cupHeight}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.SetSelectedGlass(this);
    }
}