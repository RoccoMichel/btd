using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LodeLevel : MonoBehaviour
{
    //TMP_Text
    public List<TMP_Text> LevelScor;

    private void Start()
    {
        for (int i = 0; i < LevelScor.Count; i++)
            if (PlayerPrefs.HasKey((i + 1).ToString() + "Highscore") && PlayerPrefs.GetInt((i + 1).ToString() + "Highscore") > 0)
                LevelScor[i].text = "Best Wave:\n" + PlayerPrefs.GetInt((i + 1).ToString() + "Highscore").ToString();
            else LevelScor[i].text = "Not played";
    }
}
