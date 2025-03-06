using Unity.Collections.LowLevel.Unsafe;
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

public class SetNewPlayerPrefs : EditorWindow
{
    string playerPrefs;
    string newPlayerPrefs;

    [MenuItem("Tools/Set New Int On PlayerPrefs")]
    public static void ShowWidow()
    {
        EditorWindow.GetWindow(typeof(SetNewPlayerPrefs));
    }

    private void OnGUI()
    {
        GUILayout.Label("Set New Int On PlayerPrefs", EditorStyles.boldLabel);

        playerPrefs = EditorGUILayout.TextField("PlayerPrefs To Cnange", playerPrefs);
        newPlayerPrefs = EditorGUILayout.TextField("Set New PlayerPrefs Value", newPlayerPrefs);

        if (GUILayout.Button("Change PlayerPres"))
            ChangePlayerPres();
    }

    public void ChangePlayerPres()
    {
        string typeOfPlayerPfrefs = GetPlayerPfresAsString(playerPrefs);

        if (typeOfPlayerPfrefs == "int")
            PlayerPrefs.SetInt(playerPrefs, int.Parse(newPlayerPrefs));
        else if (typeOfPlayerPfrefs == "float")
            PlayerPrefs.SetFloat(playerPrefs, float.Parse(newPlayerPrefs));
        else if (typeOfPlayerPfrefs == "string")
            PlayerPrefs.SetString(playerPrefs, newPlayerPrefs);
        else
            GUILayout.Label("Error", EditorStyles.boldLabel);
    }

    public string GetPlayerPfresAsString(string key)
    {
        string finalString = "";

        if (!PlayerPrefs.HasKey(key))
            return key + " Does Not Have A value!";

        int intValue = PlayerPrefs.GetInt(key);
        float floatValue = PlayerPrefs.GetFloat(key);

        if (intValue != int.MinValue)
        {
            finalString = "int";
        }
        else if (floatValue != float.MinValue)
        {
            finalString = "float";
        }
        else
        {
            finalString = "string";
        }

        return finalString;
    }
}

public class SeePlayerPrefs : EditorWindow
{
    string playerPfrefs;

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