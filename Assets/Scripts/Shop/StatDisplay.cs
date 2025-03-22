using UnityEngine;
using System.Collections.Generic;

public class StatDisplay : MonoBehaviour
{
    [SerializeField] protected GameObject stat;

    public List<GameObject> stats = new();

    public virtual void CreateStats(string[] names, float[] values, bool preview)
    {
        if (names.Length != values.Length) throw new System.NotSupportedException("names[] and values[] need to be of same Length");

        DestroyStats();

        for (int i = 0; i < names.Length; i++)
        {
            stats.Add(Instantiate(stat, transform));
            if (preview) stats[i].GetComponent<StatisticsBar>().SetValues(names[i], values[i]);
            else stats[i].GetComponent<StatisticsBar>().SetValues(names[i], values[i], 0.5f );
        }
    }

    protected void DestroyStats()
    {
        foreach (GameObject stat in stats)
            Destroy(stat);

        stats.Clear();
    }
}