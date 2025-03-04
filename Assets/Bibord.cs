using UnityEngine;

public class Bibord : MonoBehaviour
{
    void Update() 
    {
        //if not bibord
        //transform.LookAt(Camera.main.transform);

        transform.rotation = Camera.main.transform.rotation;
    }
}
