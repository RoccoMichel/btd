using System.Collections.Generic;
using UnityEngine;

public class PlaseCat : MonoBehaviour
{
    public GameObject cat;
    public LayerMask ground;
    public Material canBild;
    public List<Material> oldMehsMaterials;
    public List<Material> oldSkinMehsMaterials;
    public Transform RangeVisualization;

    public void VisualizeRange()
    {
        oldMehsMaterials.Clear();
        oldSkinMehsMaterials.Clear();
        
        MeshRenderer[] meshes = cat.GetComponentsInChildren<MeshRenderer>();
        SkinnedMeshRenderer[] skins = cat.GetComponentsInChildren<SkinnedMeshRenderer>();

        for (int i = 0; i < meshes.Length; i++) { oldMehsMaterials.Add(meshes[i].material); }
        for (int i = 0; i < skins.Length; i++) { oldSkinMehsMaterials.Add(skins[i].material); }
    }
    void Update()
    {
        if (cat != null && oldMehsMaterials.Count > 0)
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
            SkinnedMeshRenderer[] skins = cat.GetComponentsInChildren<SkinnedMeshRenderer>();
            bool canPlase = cat.GetComponent<CatBase>().isColliding;
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].material = canBild;
                canBild.color = canPlase ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
                canBild.SetColor("_EmissionColor", canPlase ? new Color(1, 0, 0, 0.25f) : new Color(0, 0, 1, 0.25f));
            }

            for (int i = 0; i < skins.Length; i++)
            {
                skins[i].material = canBild;
                canBild.color = canPlase ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
                canBild.SetColor("_EmissionColor", canPlase ? new Color(1, 0, 0, 0.25f) : new Color(0, 0, 1, 0.25f));
            }

            if (Input.GetMouseButtonDown(0) && !canPlase)
            {
                cat.GetComponent<CatBase>().canAtacke = true;

                for (int i = 0; i < meshes.Length; i++)
                { meshes[i].material = oldMehsMaterials[i]; }

                for (int i = 0; i < skins.Length; i++)
                    skins[i].material = oldSkinMehsMaterials[i];

                cat = null;
                oldMehsMaterials.Clear();
            }
            else if (cat != null)
            {
                cat.GetComponent<CatBase>().canAtacke = false;
            }
        }
        else RangeVisualization.position = Vector3.down * 1000;
    }
}