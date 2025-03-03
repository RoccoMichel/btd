using UnityEngine;
using NaughtyAttributes;

public class ProjecttileManager : MonoBehaviour
{
    [Expandable]
    public ScriptableProjecttileManager SPM;

    float lifeTime;

    int damage;
    float speed;

    bool doDamageWhenDied;
    float damageRange;

    bool doEfect;
    bool doEfectWhenDied;
    bool doEfectWhenHitEnemy;
    GameObject efect;

    bool bounce;

    [HideInInspector]
    public Vector3 targetPos;
    float sinTime = 0;

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
            if (transform.position != targetPos)
            {
                sinTime += Time.deltaTime * speed;
                sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2) + 0.5f;
                transform.position = Vector3.Lerp(transform.position, targetPos, t);
            }
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

            DestroyObject(false);
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
