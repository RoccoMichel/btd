using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class MainMenu : MonoBehaviour
{
    public GameObject levelButton;
    public RectTransform parent;

    List<GameObject> levelButtons = new();

    int sceneID = 0;

    public void SetSceneID()
    {
        sceneID = int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name);
    }

    public void LoadLevel()
    {
        LoadLogic.LoadSceneByNumber(sceneID);
    }

    public void PlaySound()
    {
        AddSoundToButton.PlaySelectedSound();
    }

    [Button("Create New Level")]
    public void CreateNewLevel()
    {
        GameObject newLevel = Instantiate(levelButton);
        newLevel.name = (levelButtons.Count + 1).ToString();
        newLevel.GetComponentInChildren<TMP_Text>().text = "Level: " + (levelButtons.Count + 1).ToString();

        MonoBehaviour sctipt = this;

        UnityEditor.Events.UnityEventTools.AddPersistentListener(newLevel.GetComponent<Button>().onClick, () =>
        {
            MethodInfo methodInfo = sctipt.GetType().GetMethod("SetSceneID", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (methodInfo != null)
                methodInfo.Invoke(sctipt, null);
            else
                Debug.LogError("Something Is Wrong");
        });

        UnityEditor.Events.UnityEventTools.AddPersistentListener(newLevel.GetComponent<Button>().onClick, () =>
        {
            MethodInfo methodInfo = sctipt.GetType().GetMethod("LoadLevel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (methodInfo != null)
                methodInfo.Invoke(sctipt, null);
            else
                Debug.LogError("Something Is Wrong");
        });

        UnityEditor.Events.UnityEventTools.AddPersistentListener(newLevel.GetComponent<Button>().onClick, () =>
        {
            MethodInfo methodInfo = sctipt.GetType().GetMethod("PlaySound", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (methodInfo != null)
                methodInfo.Invoke(sctipt, null);
            else
                Debug.LogError("Something Is Wrong");
        });

        //newLevel.GetComponent<Button>().onClick.AddListener(SetSceneID);
        //newLevel.GetComponent<Button>().onClick.AddListener(LoadLevel);
        //newLevel.GetComponent<Button>().onClick.AddListener(PlaySound);

        AddSoundToButton.staticExclude.Add(newLevel.GetComponent<Button>());

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(newLevel);
#endif

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