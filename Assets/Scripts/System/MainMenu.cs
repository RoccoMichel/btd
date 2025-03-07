using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using static Cinemachine.DocumentationSortingAttribute;

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

    [Button("Create New Level")]
    public void CreateNewLevel()
    {
        int level = levelButtons.Count + 1;

        GameObject newLevel = Instantiate(levelButton);
        newLevel.name = level.ToString();
        newLevel.GetComponentInChildren<TMP_Text>().text = "Level: " + (levelButtons.Count + 1).ToString();

        newLevel.GetComponent<RectTransform>().SetParent(parent, false);

        levelButtons.Add(newLevel);

        astb.exclude.Add(newLevel.GetComponent<Button>());

        print("Made New Level");
    }

    [Button("Remove Level")]
    public void RemoveLevel()
    {
        astb.exclude.Remove(levelButtons[levelButtons.Count - 1].GetComponent<Button>());

        DestroyImmediate(levelButtons[levelButtons.Count - 1]);

        levelButtons.Remove(levelButtons[levelButtons.Count - 1]);

        print("Removed A Level");
    }

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

        for(int i = 0; i < levelButtons.Count; i++)
        {
            GameObject highScoreObject = new GameObject();

            highScoreObject.name = "Score";

            RectTransform objectTransform = highScoreObject.AddComponent<RectTransform>();
            TMP_Text objectText = highScoreObject.AddComponent<TextMeshProUGUI>();

            objectTransform.SetParent(levelButtons[i].GetComponent<RectTransform>(), false);
            objectTransform.sizeDelta = new Vector2(200, 100);
            objectTransform.localPosition = new Vector3(0, -100, 0);

            objectText.alignment = TextAlignmentOptions.Center;
            objectText.fontSize = 45;

            string highScoreText = "0";
            if (PlayerPrefs.HasKey((i + 1).ToString()) && PlayerPrefs.GetFloat((i + 1).ToString()) > 0)
                highScoreText = PlayerPrefs.GetFloat((i + 1).ToString()).ToString();
            else
                highScoreText = "Not Played";

            objectText.text = highScoreText;

            levelButtons[i].GetComponent<Button>().onClick.AddListener(LoadLevel);
        }
    }

    IEnumerator SwitchScenes()
    {
        loadingObject.SetActive(true);

        astb.PlaySelectedSound();
        
        yield return new WaitForSeconds(aniL.GetCurrentAnimatorStateInfo(0).length);

        LoadLogic.LoadSceneByNumber(int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name));
    }
}