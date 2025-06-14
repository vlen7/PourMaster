using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Conf : Singleton<Conf>
{
    private readonly Dictionary<int, LevelSetting> _levelMap = new();
    public int level { get; private set; } = 1;


    private struct LevelSetting
    {
        public List<WaterGlass> glasses;
        public float waterGoal;

        public LevelSetting(float[][] glass, float goal)
        {
            glasses = new List<WaterGlass>();
            foreach (var g in glass)
            {
                glasses.Add(new WaterGlass(g[0], g[1]));
            }

            waterGoal = goal;
        }
    }

    private void Awake()
    {
        _levelMap.Add(1, new LevelSetting(new[] { new[] { 5f, 10f }, new[] { 0f, 5f }, new[] { 6f, 6f } }, 8));
        _levelMap.Add(2, new LevelSetting(new[] { new[] { 1f, 2f }, new[] { 1f, 2f } }, 2));
        _levelMap.Add(3, new LevelSetting(new[] { new[] { 1f, 2f }, new[] { 1f, 2f } }, 2));
        _levelMap.Add(4, new LevelSetting(new[] { new[] { 1f, 2f }, new[] { 1f, 2f } }, 2));
    }

    public void NextLevel()
    {
        level++;
    }

    public bool HasNoNext()
    {
        return level >= _levelMap.Count;
    }

    public List<WaterGlass> GetLevelSetting()
    {
        var waters = _levelMap[level];
        return waters.glasses.Select(water => water.DeepCopy()).ToList();
    }

    public float GetGoal()
    {
        return _levelMap[level].waterGoal;
    }
}