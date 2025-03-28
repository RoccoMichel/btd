using UnityEngine;
using UnityEngine.Splines;

public class RatTank : RatBase
{
    public int spawnAmmount;
    public GameObject[] rats;

    public override void OnStart(float startPos)
    {
        ratMesh = GetComponentInChildren<MeshRenderer>();
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
    public override void Kill()
    {
        session.Profit(value);

        for (int i = 0; i < spawnAmmount; i++)
        {
            Vector3 s = new Vector3(0, 0, 0);
            GameObject Rat = Instantiate(rats[Random.Range(0, rats.Length)], s, Quaternion.identity);
            Rat.GetComponent<RatBase>().session = session;
            Rat.GetComponent<RatBase>().OnStart(spline.normalizedTime + Random.Range(-0.01f, 0.01f));
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

            // Get world position using normalized time
            Vector3 position = SplineUtility.EvaluatePosition(splineData, t);

            //Debug.Log("Current Position on Spline: " + position);
        }
    }
}
