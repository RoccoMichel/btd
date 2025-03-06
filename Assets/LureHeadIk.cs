using UnityEngine;

public class LureHeadIk : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.forward = transform.right;

        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, transform.rotation, Time.deltaTime*10);
    }
}
