// All Code By Charlie

using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class PlaceBase : MonoBehaviour
{
    
}

public class SpawnBase : EditorWindow
{
    // Gets The Path To The Base Prefab
    string path = "Assets/Prefabs/Base.prefab";
    float percentPos = 0.98f;
    SplineContainer container;

    // Adds It To The Tools Menu
    [MenuItem("Tools/Spawn Base")]
    public static void ShowWidow()
    {
        // Creats A Custom Window
        GetWindow(typeof(SpawnBase));
    }

    private void OnGUI()
    {
        // Adds A Label
        GUILayout.Label("Spawn Base", EditorStyles.boldLabel);

        // Adds A Editable Float Field To Set The Persent From End On A Spline
        percentPos = EditorGUILayout.FloatField("Percentes Away From The End", percentPos);

        // Adds A Button That When Pressed Calls The Spawn Function
        if (GUILayout.Button("Spawn"))
            Spawn();
    }

    public void Spawn()
    {
        // Finds The Spline Container In The Scene
        container = FindObjectOfType<SplineContainer>();

        // Creats The Base Prefab As A Prefab
        GameObject Base = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(path)).GameObject();

        // Gets The Last Index Of The Knots In The Spline
        int splineIndex = container.Spline.Count - 1;
        Vector3 pos = Vector3.zero;

        // Gets The Position Of The Last Knot
        foreach (var spline in container.Splines)
            pos = spline.EvaluatePosition(percentPos);

        // Offsets The Vector It So Its Not In The Wrong Location
        pos.x += container.gameObject.transform.position.x;
        pos.y = 1.9f;
        pos.z += container.gameObject.transform.position.z;

        // Sets The Position And Rotation
        Base.transform.position = pos;
        Base.transform.rotation = container.Spline[splineIndex].Rotation;

        // Sets The Position In The Hierarchy To Be Under The Spline
        int hierarchyIndex = container.gameObject.transform.GetSiblingIndex() + 1;
        Base.transform.SetSiblingIndex(hierarchyIndex);
    }
}
