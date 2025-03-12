using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
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
    public string typ;
    AudioSource AS;
    public float t;
    float healthSave = 0;

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
        OnStart(0);
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

        WaveManager whaw = GetComponentInParent<WaveManager>();
        if (typ == "normal") whaw.BufNorm.Add(this);
        if (typ == "choky") whaw.BufChoky.Add(this);
        if (typ == "mutant") whaw.BufMutent.Add(this);
        if (typ == "steroid") whaw.BufSterods.Add(this);
        if (typ == "tank") whaw.BufTacnk.Add(this);

        spline.Pause();

        float time = 0;
        if (deathParam != "")
        {
            ani.SetTrigger(deathParam);

            time = ani.GetCurrentAnimatorStateInfo(0).length;
        }

        session.Profit(value);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// When the rat reaches the end of the track
    /// </summary>
    public virtual void Score()
    {

        session.Damage(damage);
        WaveManager whaw = GetComponentInParent<WaveManager>();
        if (typ == "normal") whaw.BufNorm.Add(this);
        if (typ == "choky") whaw.BufChoky.Add(this);
        if (typ == "mutant") whaw.BufMutent.Add(this);
        if (typ == "steroid") whaw.BufSterods.Add(this);
        if (typ == "tank") whaw.BufTacnk.Add(this);
        gameObject.SetActive(false);

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

    public void OnReStart(float startPos)
    {
        health = healthSave;
        var container = FindAnyObjectByType<SplineContainer>();
        gameObject.SetActive(true);
        spline.Container = container;
        spline.MaxSpeed = speed;
        spline.NormalizedTime = startPos;

        spline.Play();
    }
    /// <summary>
    /// Base method for logic that will run when instance is created
    /// </summary>
    public virtual void OnStart(float startPos)
    {
        healthSave = health;
        AS = GetComponent<AudioSource>();

        if (!IsDrugdiler)
        {
            ofsert = new Vector3
            {
                x = Random.Range(-.2f, .2f),
                y = .25f,
                z = 0

            };
            ratMesh.transform.localPosition = ofsert;
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

        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        Vector3 camDir = Camera.main.transform.forward;
        if (Vector3.Dot(camDir, dir) < 0.5f) ratMesh.gameObject.SetActive(false);
        else ratMesh.gameObject.SetActive(true);
    }
    public void SetBigRat(float scaleProsent)
    {
        health *= scaleProsent;
        speed *= scaleProsent;
        damage *= scaleProsent;
    }
    // posen efeckt
    void acivatePosenEfect()
    {
        Material posed = Instantiate(ratMesh.material);
        ratMesh.material = posed;

        posed.color = new Color(0f, 1f, 0f);
    }
    void DeAcivatePosenEfect()
    {
        ratMesh.material.color = Color.white;
    }
    public IEnumerator Poisone()
    {
        acivatePosenEfect();
        for (float time = 0; time < posendTime; time += Time.deltaTime)
        {
            ratMesh.material.color = Color.Lerp(Color.green, Color.white, time/posendTime);
            DamageNoEfeckt(poisoneDamage * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        DeAcivatePosenEfect();
    }
}