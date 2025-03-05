using UnityEngine;

public class BoombManager : MonoBehaviour
{
    public float damage;
    public GameObject efect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Rat")
        {
            Instantiate(efect, transform.position, Quaternion.identity);

            collision.gameObject.GetComponent<RatBase>().Damage(damage);
        }
    }
}
