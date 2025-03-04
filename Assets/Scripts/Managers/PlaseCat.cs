using System.Collections.Generic;
using UnityEngine;

public class PlaseCat : MonoBehaviour
{
    public GameObject cat;
    public LayerMask ground;
    public Material canBild;
    public List<Material> oldMaterials;
    public Transform RangeVisualization;

    public void VisualizeRange()
    {
        oldMaterials.Clear();
        
        MeshRenderer[] meshes = cat.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            oldMaterials.Add(meshes[i].material);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) VisualizeRange();
        if (cat != null && oldMaterials.Count > 0)
        {

            RangeVisualization.position = cat.transform.position + Vector3.up * 0.01f;
            float range = cat.GetComponent<CatBase>().range;

            RangeVisualization.localScale = new Vector3(range * 2, range * 2, 0);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray.direction *= 1000;
            RaycastHit[] hit = Physics.RaycastAll(ray.origin, ray.direction);


            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.CompareTag("CanPlase"))
                    cat.transform.position = hit[i].point;
            }

            MeshRenderer[] meshes = cat.GetComponentsInChildren<MeshRenderer>();
            bool canPlase = cat.GetComponent<CatBase>().isColliding;
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].material = canBild;
                canBild.color = canPlase ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
                canBild.SetColor("_EmissionColor", canPlase ? new Color(1, 0, 0, 0.25f) : new Color(0, 0, 1, 0.25f));
            }

            if (Input.GetMouseButtonDown(0) && !canPlase)
            {
                for (int i = 0; i < meshes.Length; i++)
                    meshes[i].material = oldMaterials[i];

                cat = null;
                oldMaterials.Clear();
            }
        }
        else RangeVisualization.position = Vector3.down * 1000;
    }
}