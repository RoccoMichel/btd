using System.Collections.Generic;
using UnityEngine;

public class PlaseCat : MonoBehaviour
{
    public GameObject cat;
    public LayerMask grond;
    public Material canBild;
    public List<Material> oldMaterols;
    public Transform rahseVishols;

    public void setOldMaterols()
    {
        oldMaterols.Clear();
        
        MeshRenderer[] meshes = cat.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            oldMaterols.Add(meshes[i].material);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) setOldMaterols();
        if (cat != null && oldMaterols.Count > 0)
        {

            rahseVishols.position = cat.transform.position + Vector3.up * 0.01f;
            float ransh = cat.GetComponent<CatBase>().ransh;

            rahseVishols.localScale = new Vector3(ransh * 2, ransh * 2, 0);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray.direction *= 1000;
            RaycastHit[] hit = Physics.RaycastAll(ray.origin, ray.direction);


            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.CompareTag("CanPlase"))
                    cat.transform.position = hit[i].point;
            }

            MeshRenderer[] meshes = cat.GetComponentsInChildren<MeshRenderer>();
            bool canPlase = cat.GetComponent<CatBase>().isColiding;
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].material = canBild;
                canBild.color = canPlase ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
                canBild.SetColor("_EmissionColor", canPlase ? new Color(1, 0, 0, 0.25f) : new Color(0, 0, 1, 0.25f));
            }

            if (Input.GetMouseButtonDown(0) && !canPlase)
            {
                for (int i = 0; i < meshes.Length; i++)
                    meshes[i].material = oldMaterols[i];

                cat = null;
                oldMaterols.Clear();
            }
        }
        else rahseVishols.position = Vector3.down * 1000;


    }
}
