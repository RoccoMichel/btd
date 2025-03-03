using UnityEngine;
using UnityEngine.Splines;

public class RatBase : MonoBehaviour
{
    public SplineAnimate spline;

    public float spede, health;

    void Start() { spline.MaxSpeed = spede; }
    public virtual void IsDedFrFr()
    {
        Destroy(gameObject);
    }

    public void RunDefaltCode()
    {
        if (health <= 0) IsDedFrFr();
    }



}
