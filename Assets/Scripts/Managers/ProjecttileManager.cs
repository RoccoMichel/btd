// Code By Charlie And Algot

using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;

public class ProjecttileManager : MonoBehaviour
{
    public LayerMask rats;
    [Expandable]
    public ScriptableProjecttileManager SPM;
    
    float lifeTime;

    float damage;
    public float speed;
    public bool prsig;
    public AnimationCurve SacleOwerTime;
    bool doDamageWhenDied;
    float damageRange;

    bool doEfect;
    bool doEfectWhenDied;
    bool doEfectWhenHitEnemy;
    GameObject efect;
    Vector3 startSacale = Vector3.one;

    bool doPoisone;
    float poisoneDamage, posenDureshen;

    List<GameObject> poiseonedRats = new List<GameObject>();

    float timeAlive = 0;
    public MeshRenderer mesh;

    Rigidbody rb;

    private void Update()
    {
        if (mesh != null)
        {
            Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
            Vector3 camDir = Camera.main.transform.forward;
            if (Vector3.Dot(camDir, dir) < 0.5f) mesh.gameObject.SetActive(false);
            else mesh.gameObject.SetActive(true);
        }

        transform.localScale = startSacale * SacleOwerTime.Evaluate(timeAlive/lifeTime);

        // <Code By Charlie>
        // Moves The Projectile Forword
        transform.position += transform.forward * Time.deltaTime * speed;
        runHitDetecshen();
        // Counts The Time The Projectile Is Alive
        timeAlive += Time.deltaTime;

        // Destroys It When The Time Alive Is Bigger The lifeTime
        if(timeAlive >= lifeTime)
        {
            // If doEfectWhenDied Is True Then It Adds An Effect
            if (doEfectWhenDied) Instantiate(efect, transform.position, Quaternion.identity);

            DestroyObject(doDamageWhenDied);
        }
        // <Code By Charlie>
    }
    void runHitDetecshen()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.forward, out hit, Time.deltaTime * speed, rats))
        {
            onHit(hit.collider.gameObject);
        }
    }

    void onHit(GameObject hit)
    {
        // If The Projectile Is Poisened
        if (doPoisone)
        {
            // Do The Poisen
            RatBase rat = hit.GetComponent<RatBase>();
            rat.posendTime = posenDureshen;
            rat.poisoneDamage = poisoneDamage;

            rat.StartCoroutine(rat.Poisone());
        }

        // Dose Damage To The Rat
        hit.GetComponent<RatBase>().Damage(damage);
        // <Code By Charlie>

        if (!prsig) DestroyObject(doEfectWhenHitEnemy);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // <Code By Charlie>
        // Cheks If The Projectile Collides With A Rat
        if(collision.transform.tag == "Rat")
        {
            onHit(collision.gameObject);
        }
    }

    void DestroyObject(bool timeRanOut = false)
    {
        if (doEfectWhenHitEnemy)
            Instantiate(efect, transform.position, Quaternion.identity);

        if (timeRanOut)
        {
            if (doEfectWhenDied)
                Instantiate(efect, transform.position, Quaternion.identity);
        }
        else
        {
           
        }
            
        Destroy(gameObject);
    }

    private void Awake()
    {
        SetSettings();
        startSacale = transform.localScale;
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

        doPoisone = SPM.doPoisone;

        poisoneDamage = SPM.poisoneDamage;
        posenDureshen = SPM.poisoneTimes;

        rb = GetComponent<Rigidbody>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            transform.position - transform.forward * Time.deltaTime * speed); 
    }
}
