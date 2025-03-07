// All Code By Charlie

using UnityEditor;
using UnityEngine;

public class Tools : MonoBehaviour
{

}

// Change The Value Of A Given PlayerPrefs
public class SetNewPlayerPrefs : EditorWindow
{
    string playerPrefs;
    string newPlayerPrefs;

    // Adds It To The Tools Menu
    [MenuItem("Tools/Set New Int On PlayerPrefs")]
    public static void ShowWidow()
    {
        // Creats A Custom Window
        GetWindow(typeof(SetNewPlayerPrefs));
    }

    private void OnGUI()
    {
        // Adds A Label
        GUILayout.Label("Set New Int On PlayerPrefs", EditorStyles.boldLabel);

        // Adds A Editable Text Field That The You Input The PlayerPrefs You Want To Change
        playerPrefs = EditorGUILayout.TextField("PlayerPrefs To Cnange", playerPrefs);
        // Adds A Editable Text Field That The New Value For The PlayerPrefs Is Going To Be
        newPlayerPrefs = EditorGUILayout.TextField("Set New PlayerPrefs Value", newPlayerPrefs);

        // Adds A Button That When Pressed Calls The ChangePlayerPres Function
        if (GUILayout.Button("Change PlayerPres"))
            ChangePlayerPres();
    }

    public void ChangePlayerPres()
    {
        // Gets The Typ Of PlayerPrefs
        string typeOfPlayerPfrefs = GetPlayerPfresAsString(playerPrefs);

        // Sets A New Int If Its An Int
        if (typeOfPlayerPfrefs == "int")
            PlayerPrefs.SetInt(playerPrefs, int.Parse(newPlayerPrefs));
        // Sets A New Floaf If Its A Float
        else if (typeOfPlayerPfrefs == "float")
            PlayerPrefs.SetFloat(playerPrefs, float.Parse(newPlayerPrefs));
        // Sets A New Sting If Its A String
        else if (typeOfPlayerPfrefs == "string")
            PlayerPrefs.SetString(playerPrefs, newPlayerPrefs);
        else
            // If Something Wnet Wrong, Add A Lables That Says "Error"
            GUILayout.Label("Error", EditorStyles.boldLabel);
    }

    public string GetPlayerPfresAsString(string key)
    {
        string finalString = "";

        // Cheks If It The PlayerPrefs Exsists
        if (!PlayerPrefs.HasKey(key))
            return key + " Does Not Have A value!";

        int intValue = PlayerPrefs.GetInt(key);
        float floatValue = PlayerPrefs.GetFloat(key);

        // Cheks If Its An Int
        if (intValue != int.MinValue)
        {
            finalString = "int";
        }
        // If Not An Int Chek If Its A Float
        else if (floatValue != float.MinValue)
        {
            finalString = "float";
        }
        // If Not A Float Or String
        else
        {
            finalString = "string";
        }

        return finalString;
    }
}

// You Can Check A Value Of A PlayerPfres
public class SeePlayerPrefs : EditorWindow
{
    string playerPfrefs;

    string newValue = "";
    bool showText = false;

    // Adds It To The Tools Menu
    [MenuItem("Tools/See PlayerPrefs")]
    public static void ShowWidow()
    {
        // Creats A Custom Window
        GetWindow(typeof(SeePlayerPrefs));
    }

    private void OnGUI()
    {
        // Adds A Editable Text Field That The You Input The PlayerPrefs You Want To Check
        playerPfrefs = EditorGUILayout.TextField("PlayerPfres", playerPfrefs);

        // Adds A Button That When Pressed Calls The Check Function
        if (GUILayout.Button("Check"))
            Check();

        // If showText Is True Add A Label With The PlayerPfres Value
        if (showText)
        {
            GUILayout.Label(newValue, EditorStyles.boldLabel);
        }
    }

    public void Check()
    {
        // Gets The Value Of The PlayerPrefs
        newValue = GetPlayerPfresAsString(playerPfrefs);

        showText = true;
    }

    public string GetPlayerPfresAsString(string key)
    {
        string finalString = "";

        // Cheks If The PlayerPrefs Exsits
        if (!PlayerPrefs.HasKey(key))
            return key + " Does Not Have A value!";

        int intValue = PlayerPrefs.GetInt(key);
        float floatValue = PlayerPrefs.GetFloat(key);
        string stringValue = PlayerPrefs.GetString(key);

        // Cheks If Its Aa Int
        if (intValue != int.MinValue)
            finalString = intValue.ToString();
        // If Not An Int Check If Its A Float
        else if (floatValue != float.MinValue)
            finalString = floatValue.ToString();
        // If Its Not An Int Or A Float It Must Be A String
        else
            finalString = stringValue;

        return finalString;
    }
}