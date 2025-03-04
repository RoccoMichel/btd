using UnityEngine;
using NaughtyAttributes;

public class ProjecttileManager : MonoBehaviour
{
    [Expandable]
    public ScriptableProjecttileManager SPM;

    float lifeTime;

    float damage;
    float speed;
    public bool prsig;
    bool doDamageWhenDied;
    float damageRange;

    bool doEfect;
    bool doEfectWhenDied;
    bool doEfectWhenHitEnemy;
    GameObject efect;

    bool bounce;

    float timeAlive = 0;

    private void Update()
    {
        if (bounce)
        {
            // Algot You Fix This
            // IDK How To
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        timeAlive += Time.deltaTime;

        if(timeAlive >= lifeTime)
        {
            if (doEfectWhenDied)
                Instantiate(efect, transform.position, Quaternion.identity);

            DestroyObject(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Rat")
        {
            // Damage the rat here
            collision.gameObject.GetComponent<RatBase>().Damage(damage);
            
            if (!prsig) Destroy(gameObject);
            //DestroyObject(false);
        }
    }

    void DestroyObject(bool timeRanOut = false)
    {
        if (doDamageWhenDied)
        {
            // Gonna Fix This Later!
        }

        if (timeRanOut)
        {
            if (doEfectWhenDied)
                Instantiate(efect, transform.position, Quaternion.identity);
        }
        else
        {
            if (doEfectWhenHitEnemy)
                Instantiate(efect, transform.position, Quaternion.identity);
        }
            
        Destroy(gameObject);
    }

    private void Awake()
    {
        SetSettings();
    }

    void SetSettings()
    {
        lifeTime = SPM.lifeTime;

        damage = SPM.damage;
        speed = SPM.speed;

        doDamageWhenDied = SPM.doDamageWhenDied;
        damageRange = SPM.damageRange;

        doEfect = SPM.doEfect;
        doEfectWhenDied = SPM.doEfectWhenDied;
        doEfectWhenHitEnemy = SPM.doEfectWhenHitEnemy;
        efect = SPM.efect;

        bounce = SPM.bounce;
    }
}
