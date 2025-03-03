using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class RatBase : MonoBehaviour
{
    public SplineAnimate spline;
    public Session session;
    public float spede, health, damitsh, killMuny;
    

    void Start() { spline.MaxSpeed = spede; }
    public virtual void IsDedFrFr()
    {
        session.currency += killMuny;
        Destroy(gameObject);
    }

    public virtual void AtEnd()
    {
        session.health -= damitsh;
        Destroy(gameObject);
    }

    public void RunDefaltCode()
    {
        if (health <= 0) IsDedFrFr();
        if (spline.normalizedTime > 0.99f) AtEnd();
    }


   
}
