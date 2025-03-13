using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LodeLevel : MonoBehaviour
{
    //TMP_Text
    public List<TMP_Text> LevelScor;

    public void LodeCene(string seneName) { SceneManager.LoadScene(seneName); }
    private void Start()
    {
        for (int i = 0; i < 4; i++)
            if (PlayerPrefs.HasKey((i + 1).ToString() + "Highscore") && PlayerPrefs.GetInt((i + 1).ToString() + "Highscore") > 0)
                LevelScor[i].text = @"Best whawe :
" + PlayerPrefs.GetInt((i + 1).ToString() + "Highscore").ToString();
            else LevelScor[i].text = "Not playd";
    }
}
