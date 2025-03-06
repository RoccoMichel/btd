using UnityEngine;
using UnityEngine.Splines;

public class RatTank : RatBase
{
    public int spawnAmmount;
    public GameObject rat;
    float t;

    public override void Kill()
    {
        session.Profit(value);

        for (int i = 0; i < spawnAmmount; i++)
        {
            Vector3 s = new Vector3(0, 0, 0);
            GameObject Rat = Instantiate(rat, s, Quaternion.identity);
            Rat.GetComponent<RatBase>().session = session;
<<<<<<< Updated upstream
            Rat.GetComponent<RatBase>().OnStart(spline.NormalizedTime + Random.Range(-0.05f, 0.05f));
=======
            rat.GetComponent<RatBase>().OnStart(t + Random.Range(-0.05f, 0.05f));
            Debug.Log(t);
>>>>>>> Stashed changes
        }

        Destroy(gameObject);
    }
    void Update()
    {
        OnUpdate();
        if (spline != null && spline.Container != null)
        {
            // Get the spline reference
            Spline splineData = spline.Container.Spline;

            // Get current normalized position on spline
            t = spline.NormalizedTime;

            // Get world position using normalized time
            Vector3 position = SplineUtility.EvaluatePosition(splineData, t);

            //Debug.Log("Current Position on Spline: " + position);
        }
    }
}
