using UnityEngine;
using UnityEngine.Splines;

public class RatBase : MonoBehaviour
{
    public SplineAnimate spline;
    public Session session;
    public float speed, health, damage, value;    

    void Start()
    {
        OnStart();
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
    }

    /// <summary>
    /// Base method for logic that will run when instance is created
    /// </summary>
    public virtual void OnStart()
    {
        spline.Container = FindAnyObjectByType<SplineContainer>();
        spline.MaxSpeed = speed;
    }

    /// <summary>
    /// Base method for logic that will run every frame
    /// </summary>
    public virtual void OnUpdate()
    {        
        if (spline.NormalizedTime > 0.9f) Score();
    }
}