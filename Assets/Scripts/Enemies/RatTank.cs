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
            GameObject Rat = Instantiate(rat, s , Quaternion.identity);
            Rat.GetComponent<RatBase>().session = session;
            rat.GetComponent<RatBase>().OnStart(t);
        }

        Destroy(gameObject);
    }
    void Update()
    {
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
