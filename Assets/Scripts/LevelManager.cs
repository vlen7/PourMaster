using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject waterGlassPrefab;
    [SerializeField] private GameObject waterGlassPool;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TextMeshProUGUI goalText;

    private readonly List<GameObject> _glasses = new();
    private WaterGlassDisplay _selectedGlass;

    private void Start()
    {
        InitGlass();
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) // 检测鼠标左键点击
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (_selectedGlass)
                {
                    _selectedGlass.ShowGlass();
                    _selectedGlass = null;
                }
            }
        }
    }


    public void InitGlass()
    {
        ClearPool();
        var waterGlasses = Conf.Instance.GetLevelSetting();
        for (var i = 0; i < waterGlasses.Count; i++)
        {
            var glass = Instantiate(waterGlassPrefab, waterGlassPool.transform, true);
            glass.name = $"WaterGlass {Conf.Instance.level}-{i}-{Random.Range(0, 100)}";
            glass.GetComponent<WaterGlassDisplay>().waterGlass = waterGlasses[i];
            _glasses.Add(glass);
        }

        goalText.text = $"目标: {Conf.Instance.GetGoal()}";

        nextButton.SetActive(false);
    }

    public void SetSelectedGlass(WaterGlassDisplay glass)
    {
        if (!_selectedGlass)
        {
            _selectedGlass = glass;
            _selectedGlass.transform.localScale *= 1.1f;
        }
        else
        {
            Pour(_selectedGlass, glass);
            _selectedGlass = null;
        }

        if (CheckPass())
        {
            nextButton.SetActive(true);
        }
    }

    private bool CheckPass()
    {
        var goal = Conf.Instance.GetGoal();
        foreach (var glass in _glasses)
        {
            var waterGlass = glass.GetComponent<WaterGlassDisplay>().waterGlass;
            if (Mathf.Approximately(waterGlass.waterHeight, goal)) return true;
        }

        return false;
    }

    private void Pour(WaterGlassDisplay from, WaterGlassDisplay to)
    {
        var remain = Math.Min(to.waterGlass.Remain(), from.waterGlass.waterHeight);
        from.AddWater(-remain);
        to.AddWater(remain);
    }


    public void NextLevel()
    {
        Conf.Instance.NextLevel();
        InitGlass();
    }

    private void ClearPool()
    {
        foreach (var glass in _glasses)
        {
            Destroy(glass);
        }

        _glasses.Clear();
    }
}