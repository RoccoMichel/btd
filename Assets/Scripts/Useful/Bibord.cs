using UnityEngine;

public class Bibord : MonoBehaviour
{
    public Vector3 offsetRotation;
    public BillboardTypes billboardType;
    public enum BillboardTypes { lookAt, orthographic, lookedY }
    void Update() 
    {
        switch (billboardType)
        {
            case BillboardTypes.lookAt:
                transform.LookAt(Camera.main.transform);

                if (offsetRotation == Vector3.zero) break;
                transform.rotation = Quaternion.Euler(
                    transform.eulerAngles.x + offsetRotation.x,
                    transform.eulerAngles.y + offsetRotation.y,
                    transform.eulerAngles.z + offsetRotation.z);

                break;

            case BillboardTypes.orthographic:
                transform.rotation = Camera.main.transform.rotation;

                if (offsetRotation == Vector3.zero) break;
                transform.rotation = Quaternion.Euler(
                    transform.eulerAngles.x + offsetRotation.x,
                    transform.eulerAngles.y + offsetRotation.y,
                    transform.eulerAngles.z + offsetRotation.z);

                break;

            case BillboardTypes.lookedY:
                transform.LookAt(Camera.main.transform);
                
                transform.rotation = Quaternion.Euler(
                    0 + offsetRotation.x,
                    transform.eulerAngles.y + offsetRotation.y,
                    0 + offsetRotation.z);

                break;
        }
    }
}
