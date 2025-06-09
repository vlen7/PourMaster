using System;
using UnityEngine;
using UnityEngine.UI;

public class WaterGlassDisplay : MonoBehaviour
{
    [SerializeField] private Image cup;
    [SerializeField] private Image water;

    public WaterGlass waterGlass = new WaterGlass(2, 2);

    private void Start()
    {
        ShowGlass();
    }

    private void Update()
    {
        ShowGlass();
    }

    public void ShowGlass()
    {
        var cupScale = 1 + (waterGlass.cupHeight / 5);
        transform.localScale = new Vector2(cupScale, cupScale);
        var childWater = transform.Find("Water");
        childWater.transform.localScale = new Vector2(1, waterGlass.waterHeight/waterGlass.cupHeight);
    }
}
