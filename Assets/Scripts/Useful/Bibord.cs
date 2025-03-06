using UnityEngine;

public class Bibord : MonoBehaviour
{
    public BillboardTypes billboardType;
    public enum BillboardTypes { lookAt, orthographic, lookedY }
    void Update() 
    {
        switch (billboardType)
        {
            case BillboardTypes.lookAt:
                transform.LookAt(Camera.main.transform);
                break;

            case BillboardTypes.orthographic:
                transform.rotation = Camera.main.transform.rotation;
                break;

            case BillboardTypes.lookedY:
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                break;
        }
    }
}
