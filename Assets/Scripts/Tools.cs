using UnityEditor;
using UnityEngine;

public class Tools : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SetNewIntOnPlayerPrefs : EditorWindow
{
    string playerPrefs;
    int newPlayerPrefs;

    [MenuItem("Tools/Set New Int On PlayerPrefs")]
    public static void ShowWidow()
    {
        EditorWindow.GetWindow(typeof(SetNewIntOnPlayerPrefs));
    }

    private void OnGUI()
    {
        GUILayout.Label("Set New Int On PlayerPrefs", EditorStyles.boldLabel);

        playerPrefs = EditorGUILayout.TextField("PlayerPrefs To Cnange", playerPrefs);
        newPlayerPrefs = EditorGUILayout.IntField("Set New PlayerPrefs Value", newPlayerPrefs);

        if (GUILayout.Button("Change PlayerPres"))
            ChangePlayerPres();
    }

    public void ChangePlayerPres()
    {
        PlayerPrefs.SetInt(playerPrefs, newPlayerPrefs);
    }
}


public class SetNewStringtOnPlayerPrefs : EditorWindow
{
    string playerPrefs;
    string newPlayerPrefs;

    [MenuItem("Tools/Set New String On PlayerPrefs")]
    public static void ShowWidow()
    {
        EditorWindow.GetWindow(typeof(SetNewStringtOnPlayerPrefs));
    }

    private void OnGUI()
    {
        GUILayout.Label("Set New String On PlayerPrefs", EditorStyles.boldLabel);

        playerPrefs = EditorGUILayout.TextField("PlayerPrefs To Cnange", playerPrefs);
        newPlayerPrefs = EditorGUILayout.TextField("Set New PlayerPrefs Value", newPlayerPrefs);

        if (GUILayout.Button("Change PlayerPres"))
            ChangePlayerPres();
    }

    public void ChangePlayerPres()
    {
        PlayerPrefs.SetString(playerPrefs, newPlayerPrefs);
    }
}

public class SeePlayerPrefs : EditorWindow
{
    string playerPfrefs;
    string playerPrefsValue = "";

    string newValue = "";
    bool showText = false;

    [MenuItem("Tools/See PlayerPrefs")]
    public static void ShowWidow()
    {
        EditorWindow.GetWindow(typeof(SeePlayerPrefs));
    }

    private void OnGUI()
    {
        playerPfrefs = EditorGUILayout.TextField("PlayerPfres", playerPfrefs);

        if (GUILayout.Button("Check"))
            Check();

        if (showText)
        {
            GUILayout.Label(newValue, EditorStyles.boldLabel);
        }
    }

    public void Check()
    {
        newValue = GetPlayerPfresAsString(playerPfrefs);

        showText = true;
    }

    public string GetPlayerPfresAsString(string key)
    {
        string finalString = "";

        if (!PlayerPrefs.HasKey(key))
            return key + " Does Not Have A value!";

        int intValue = PlayerPrefs.GetInt(key);
        float floatValue = PlayerPrefs.GetFloat(key);
        string stringValue = PlayerPrefs.GetString(key);

        if (intValue != int.MinValue)
            finalString = intValue.ToString();
        else if (floatValue != float.MinValue)
            finalString = floatValue.ToString();
        else
            finalString = stringValue;

        return finalString;
    }
}