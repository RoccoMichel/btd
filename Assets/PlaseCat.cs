using UnityEngine;

public class PlaseCat : MonoBehaviour
{
    public GameObject cat;
    public LayerMask grond;
   
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.direction *= 1000;


        if (Physics.Raycast(ray, out hit)) cat.transform.position = hit.point;

        cat.GetComponentInChildren<>
    }
}
