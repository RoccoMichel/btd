using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject levelButton;
    public RectTransform parent;

    List<GameObject> levelButtons = new List<GameObject>();

    int sceneID = 0;

    public void SetSceneID()
    {
        sceneID = int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name);
    }

    public void LoadLevel()
    {
        LoadLogic.LoadSceneByNumber(sceneID);
    }

    [Button("Create New Level")]
    public void CreateNewLevel()
    {
        GameObject newLevel = Instantiate(levelButton);
        newLevel.name = (levelButtons.Count + 1).ToString();
        newLevel.GetComponentInChildren<TMP_Text>().text = "Level: " + (levelButtons.Count + 1).ToString();

        newLevel.GetComponent<Button>().onClick.AddListener(SetSceneID);
        newLevel.GetComponent<Button>().onClick.AddListener(LoadLevel);

        newLevel.GetComponent<RectTransform>().SetParent(parent, false);

        levelButtons.Add(newLevel);

        print("Made New Level");
    }

    [Button("Remove Level")]
    public void RemoveLevel()
    {
        DestroyImmediate(levelButtons[levelButtons.Count - 1]);
        levelButtons.Remove(levelButtons[levelButtons.Count - 1]);

        print("Removed A Level");
    }

    [Button("Reset List")]
    public void ResetList()
    {
        levelButtons.Clear();
    }
}
