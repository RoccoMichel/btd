using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

public class PlaceBase : MonoBehaviour
{
    
}

public class SpawnBase : EditorWindow
{
    string path = "Assets/Prefabs/Base.prefab";
    float percentPos = 0.98f;
    SplineContainer container;

    [MenuItem("Tools/Spawn Base")]
    public static void ShowWidow()
    {
        GetWindow(typeof(SpawnBase));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn Base", EditorStyles.boldLabel);

        percentPos = EditorGUILayout.FloatField("Percentes Away From The End", percentPos);

        if (GUILayout.Button("Spawn"))
            Spawn();
    }

    public void Spawn()
    {
        container = FindObjectOfType<SplineContainer>();

        GameObject Base = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(path)).GameObject();

        int splineIndex = container.Spline.Count - 1;
        Vector3 pos = Vector3.zero;
        foreach (var spline in container.Splines)
            pos = spline.EvaluatePosition(percentPos);

        pos.x += container.gameObject.transform.position.x;
        pos.y = 1.9f;
        pos.z += container.gameObject.transform.position.z;

        Base.transform.position = pos;
        Base.transform.rotation = container.Spline[splineIndex].Rotation;

        int hierarchyIndex = container.gameObject.transform.GetSiblingIndex() + 1;
        Base.transform.SetSiblingIndex(hierarchyIndex);
    }
}
