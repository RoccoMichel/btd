using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RatBase : MonoBehaviour
{
    public SplineAnimate spline;
    public Session session;
    public float speed, health, damage, value;

    public bool isPoisoned;
    public float posendTime, poisoneDamage;

    public Material poisonedMat;
    Material normalMat;
    public MeshRenderer ratMesh;

    Vector3 ofsert = Vector3.zero;

    public List<AudioClip> deathSound;
    AudioSource AS;

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

        session.Profit(value);
        Destroy(gameObject);
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
    }

    /// <summary>
    /// Base method for logic that will run when instance is created
    /// </summary>
    public virtual void OnStart(float startPos)
    {
        ofsert = new Vector3
        {
            x = Random.Range(-.2f, .2f),
            y = GetComponentInChildren<MeshRenderer>().transform.localPosition.y + .25f,
            z = 0

        };
        GetComponentInChildren<MeshRenderer>().transform.localPosition = ofsert;


        transform.SetParent(FindAnyObjectByType<WaveManager>().transform);
        var container = FindAnyObjectByType<SplineContainer>();
        if (container == null)
        {
            Debug.LogError("SplineContainer not found in the scene!");
            return;
        }

        spline.Container = container;
        spline.MaxSpeed = speed;

        // Set starting position (0 = start, 1 = end)

        spline.StartOffset = startPos;

        spline.Play();
        float startPosition = startPos; // Change this value to set where the object starts (e.g., 0.5f for halfway)
        spline.NormalizedTime = startPosition;
        spline.StartOffset = startPosition;

        spline.Play();
        spline.StartOffset = startPosition;
    }

    /// <summary>
    /// Base method for logic that will run every frame
    /// </summary>
    public virtual void OnUpdate()
    {
        if (health <= 0) Kill();
        if (spline.NormalizedTime > 0.9f) Score();
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
            Damage(poisoneDamage * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        DeAcivatePosenEfect();
    }
}