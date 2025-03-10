using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RatBase : MonoBehaviour
{
    public SplineAnimate spline;
    public Session session;
    public float speed, health, damage, value;
    public bool isShoky;
    public bool isPoisoned;
    public float posendTime, poisoneDamage;

    public Material poisonedMat;
    Material normalMat;
    public MeshRenderer ratMesh;

    Vector3 ofsert = Vector3.zero;
    public bool IsDrugdiler;
    public Animator ani;
    [AnimatorParam("ani")]
    public string deathParam;
    public float deathTimeAfterAni = 0.2f;
    public GameObject BufeRat;
    public List<AudioClip> deathSound;
    public ParticleSystem DamiitsEfeckt;
    AudioSource AS;
    public float t;


    void OnCollisionEnter(Collision collision)
    {
        if (IsDrugdiler
            && collision.gameObject.CompareTag("Rat")
            && !collision.gameObject.GetComponentInParent<RatBase>().isShoky)
        {
            Debug.Log("bog rat");
            Vector3 s = new Vector3(0, 0, 0);
            GameObject Rat = Instantiate(BufeRat, s, Quaternion.identity);
            Rat.GetComponent<RatBase>().session = session;
            Rat.GetComponent<RatBase>().OnStart(spline.NormalizedTime);
            Destroy(collision.gameObject);
        }
    }

    void Start()
    {
        //try { normalMat = ratMesh.material; }
        //catch { Debug.LogError("Material couldn't be assigned"); }        

        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// when the rat gets killed by the player
    /// </summary>
    public virtual void Kill()
    {
        //AS.clip = deathSound[Random.Range(0, deathSound.Count - 1)];
        //AS.Play();

        spline.Pause();

        float time = 0;
        if (deathParam != "")
        {
            ani.SetTrigger(deathParam);

            time = ani.GetCurrentAnimatorStateInfo(0).length;
        }

        gameObject.tag = "Untagged";

        session.Profit(value);
        Destroy(gameObject, deathTimeAfterAni);
        health = Mathf.Infinity;
    }

    /// <summary>
    /// When the rat reaches the end of the track
    /// </summary>
    public virtual void Score()
    {

        session.Damage(damage);
        Destroy(gameObject);
    }

    /// <summary>
    /// Damage the rat and it dies of enough damage is dealt
    /// </summary>
    public virtual void Damage(float amount)
    {
        health -= amount;
        //DamiitsEfeckt.Play();
    }
    public virtual void DamageNoEfeckt(float amount)
    {
        health -= amount;
    }

    /// <summary>
    /// Base method for logic that will run when instance is created
    /// </summary>
    public virtual void OnStart(float startPos)
    {
        if (!IsDrugdiler)
        {
            ofsert = new Vector3
            {
                x = Random.Range(-.2f, .2f),
                y = GetComponentInChildren<MeshRenderer>().transform.localPosition.y + .25f,
                z = 0

            };
            GetComponentInChildren<MeshRenderer>().transform.localPosition = ofsert;
        }

        transform.SetParent(FindAnyObjectByType<WaveManager>().transform);
        var container = FindAnyObjectByType<SplineContainer>();
        if (container == null)
        {
            Debug.LogError("SplineContainer not found in the scene!");
            return;
        }

        spline.Container = container;
        spline.MaxSpeed = speed;
        spline.NormalizedTime = startPos;

        spline.Play();
    }

    /// <summary>
    /// Base method for logic that will run every frame
    /// </summary>
    public virtual void OnUpdate()
    {
        if (health <= 0) Kill();
        t = spline.NormalizedTime;
        if (t > 0.98f) Score();
    }

    // posen efeckt
    void acivatePosenEfect()
    {
        MeshRenderer mech = GetComponentInChildren<MeshRenderer>();
        Material posed = Instantiate(mech.material);
        mech.material = posed;

        posed.color = new Color(0f, 1f, 0f);
    }
    void DeAcivatePosenEfect()
    {
        MeshRenderer mech = GetComponentInChildren<MeshRenderer>();
        mech.material.color = Color.white;
    }
    public IEnumerator Poisone()
    {
        acivatePosenEfect();
        MeshRenderer mech = GetComponentInChildren<MeshRenderer>();
        for (float time = 0; time < posendTime; time += Time.deltaTime)
        {
            mech.material.color = Color.Lerp(Color.green, Color.white, time/posendTime);
            DamageNoEfeckt(poisoneDamage * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        DeAcivatePosenEfect();
    }
}