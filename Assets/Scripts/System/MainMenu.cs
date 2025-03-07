// All Code By Charlie

using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject levelButton;
    public RectTransform parent;

    [HideInInspector]
    public List<GameObject> levelButtons = new List<GameObject>();

    public AddSoundToButton astb;

    public GameObject loadingObject;
    Animator aniL;

    public void LoadLevel()
    {
        StartCoroutine(SwitchScenes());        
    }

    // Makes A Button In The Inspector That Spawns A Level Button
    [Button("Create New Level")]
    public void CreateNewLevel()
    {
        int level = levelButtons.Count + 1;

        // Spawns The Button And Sets All The Information It needs
        GameObject newLevel = Instantiate(levelButton);
        newLevel.name = level.ToString();
        newLevel.GetComponentInChildren<TMP_Text>().text = "Level: " + (levelButtons.Count + 1).ToString();

        newLevel.GetComponent<RectTransform>().SetParent(parent, false);

        // Adds The Button To A List To Use In The Start Void
        levelButtons.Add(newLevel);

        // Adds The Button To The Exclude List In The AddSoundToButton Sctipt
        astb.exclude.Add(newLevel.GetComponent<Button>());

        print("Made New Level");
    }

    // Adds A Button In The Inspector To Removes The Latest Level
    [Button("Remove Level")]
    public void RemoveLevel()
    {
        astb.exclude.Remove(levelButtons[levelButtons.Count - 1].GetComponent<Button>());

        DestroyImmediate(levelButtons[levelButtons.Count - 1]);

        levelButtons.Remove(levelButtons[levelButtons.Count - 1]);

        print("Removed A Level");
    }

    // Adds A Button In The Inspector To Reset Both Lists
    // Its For Debuging ONLY!!!
    [Button("Reset List")]
    public void ResetList()
    {
        levelButtons.Clear();
        astb.exclude.Clear();
    }

    public void Start()
    {
        loadingObject.SetActive(true);
        aniL = loadingObject.GetComponent<Animator>();
        loadingObject.SetActive(false);

        // <summary>
        // Adds Text Under The Level Button To Show Your Highscore On That Level
        for (int i = 0; i < levelButtons.Count; i++)
        {
            // Creats The Game Object
            GameObject highScoreObject = new GameObject();

            // Sets Its Name
            highScoreObject.name = "Score";

            // Adds A Rect Transform Component To It
            RectTransform objectTransform = highScoreObject.AddComponent<RectTransform>();

            // Adds A TextMeshProUGUI Component To It
            TMP_Text objectText = highScoreObject.AddComponent<TextMeshProUGUI>();

            // Sets Its As A Child And Sets The Position And Size
            objectTransform.SetParent(levelButtons[i].GetComponent<RectTransform>(), false);
            objectTransform.sizeDelta = new Vector2(200, 100);
            objectTransform.localPosition = new Vector3(0, -100, 0);

            // Sets The Text Settings
            objectText.alignment = TextAlignmentOptions.Center;
            objectText.fontSize = 45;

            // Makes The Text The Highscore If Played
            string highScoreText = "0";
            if (PlayerPrefs.HasKey((i + 1).ToString() + "Highscore") && PlayerPrefs.GetInt((i + 1).ToString() + "Highscore") > 0)
                highScoreText = PlayerPrefs.GetInt((i + 1).ToString() + "Highscore").ToString();
            else
                highScoreText = "Not Played";

            objectText.text = highScoreText;

            // Adds A Listener To Every Button To Load A Level
            levelButtons[i].GetComponent<Button>().onClick.AddListener(LoadLevel);
        }
        // <summary>
    }

    IEnumerator SwitchScenes()
    {
        // Sets The loadingObject To Active
        loadingObject.SetActive(true);

        // Plays A Sound Effect
        astb.PlaySelectedSound();
        
        // Waits Until The Loading Animation Is Done
        yield return new WaitForSeconds(aniL.GetCurrentAnimatorStateInfo(0).length);

        // Loads The Level
        LoadLogic.LoadSceneByNumber(int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name));
    }
}