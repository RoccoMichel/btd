using System.Collections.Generic;
using UnityEngine;

public class PlaseCat : MonoBehaviour
{
    public GameObject cat;
    public LayerMask grond;
    public Material canBild;
    public List<Material> oldMaterols;

   
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.direction *= 1000;


        if (Physics.Raycast(ray, out hit)) cat.transform.position = hit.point;

        MeshRenderer[] meshes = cat.GetComponentsInChildren<MeshRenderer>();
        bool canPlase = cat.GetComponent<CatBase>().isColiding;
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].material = canBild;
            canBild.color = canPlase ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
        }


    }
}
