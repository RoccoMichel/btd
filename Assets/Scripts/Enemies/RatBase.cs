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
    public float poisoneTimes, poisoneRate, poisoneDamage;

    public Material poisonedMat;
    Material normalMat;
    public MeshRenderer ratMesh;

    Vector3 ofsert = Vector3.zero;

    public List<AudioClip> deathSound;
    AudioSource AS;

    void Start()
    {
        normalMat = ratMesh.material;

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
        AS.clip = deathSound[Random.Range(0, deathSound.Count - 1)];
        AS.Play();

        session.Profit(value);
        Destroy(gameObject);
    }

    /// <summary>
    /// When the rat reaches the end of the track
    /// </summary>
    public virtual void Score()
    {
        session.health -= damage;
        Destroy(gameObject);
    }

    /// <summary>
    /// Damage the rat and it dies of enough damage is dealt
    /// </summary>
    public virtual void Damage(float amount)
    {
        health -= amount;

        if (health <= 0) Kill();

        if (isPoisoned)
        {
            if(poisoneTimes != 0)
            {
                ratMesh.material = poisonedMat;

                StartCoroutine(Poisone());

                poisoneTimes--;
            }
            else
            {
                ratMesh.material = normalMat;
                isPoisoned = false;
            }
        }
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


        var container = FindAnyObjectByType<SplineContainer>();
        if (container == null)
        {
            Debug.LogError("SplineContainer not found in the scene!");
            return;
        }

        spline.Container = container;
        spline.MaxSpeed = speed;

        // Set starting position (0 = start, 1 = end)
        float startPosition = startPos; // Change this value to set where the object starts (e.g., 0.5f for halfway)
        spline.NormalizedTime = startPosition;

        spline.StartOffset = startPosition;

        spline.Play();
    }

    /// <summary>
    /// Base method for logic that will run every frame
    /// </summary>
    public virtual void OnUpdate()
    {        
        if (spline.NormalizedTime > 0.9f) Score();
    }

    IEnumerator Poisone()
    {
        yield return new WaitForSeconds(poisoneRate);
        Damage(poisoneDamage);
    }
}