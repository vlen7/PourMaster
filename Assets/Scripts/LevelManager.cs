using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject waterGlassPrefab;
    [SerializeField] private GameObject waterGlassPool;

    private Dictionary<int, WaterGlass[]> levelMap = new();
    private List<GameObject> glasses = new();
    private int level = 0;

    private WaterGlassDisplay selectedGlass;


    private void Awake()
    {
        levelMap.Add(1, new WaterGlass[] { new(3, 10), new(1, 5) });
        levelMap.Add(2, new WaterGlass[] { new(3, 3), new(4, 4) });
        levelMap.Add(3, new WaterGlass[] { new(5, 5), new(6, 6) });
        levelMap.Add(4, new WaterGlass[] { new(7, 7), new(8, 8) });
    }

    public void SetSelectedGlass(WaterGlassDisplay glass)
    {
        if (!selectedGlass)
        {
            selectedGlass = glass;
            selectedGlass.transform.localScale *= 1.1f;
        }
        else
        {
            Pour(selectedGlass, glass);
            selectedGlass = null;
        }
    }

    private void Pour(WaterGlassDisplay from, WaterGlassDisplay to)
    {
        var remain = Math.Min(to.waterGlass.Remain(), from.waterGlass.waterHeight);
        from.AddWater(-remain);
        to.AddWater(remain);
    }


    public void NextLevel()
    {
        ClearPool();
        level++;
        var waterGlasses = levelMap[level];
        foreach (var waterGlass in waterGlasses)
        {
            var glass = Instantiate(waterGlassPrefab, waterGlassPool.transform, true);
            glass.GetComponent<WaterGlassDisplay>().waterGlass = waterGlass;
            glasses.Add(glass);
        }
    }

    private void ClearPool()
    {
        foreach (var glass in glasses)
        {
            Destroy(glass);
        }
    }
}