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

    [Button("Create New Level")]
    public void CreateNewLevel()
    {
        GameObject newLevel = Instantiate(levelButton);
        newLevel.name = (levelButtons.Count + 1).ToString();
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
            levelButtons[i].GetComponent<Button>().onClick.AddListener(LoadLevel);
    }

    IEnumerator SwitchScenes()
    {
        loadingObject.SetActive(true);

        astb.PlaySelectedSound();
        
        yield return new WaitForSeconds(aniL.GetCurrentAnimatorStateInfo(0).length);

        LoadLogic.LoadSceneByNumber(int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name));
    }
}