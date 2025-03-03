using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class RatBase : MonoBehaviour
{
    public SplineAnimate spline;

    public float spede, health, damitsh;
    public Slider temp;

    void Start() { spline.MaxSpeed = spede; }
    public virtual void IsDedFrFr()
    {
        Destroy(gameObject);
    }

    public virtual void AtEnd()
    {
        temp.value -= damitsh;
        Destroy(gameObject);
    }

    public void RunDefaltCode()
    {
        if (health <= 0) IsDedFrFr();
        if (spline.normalizedTime > 0.99f) AtEnd();
    }


   
}
