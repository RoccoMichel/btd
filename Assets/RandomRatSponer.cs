using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class RandomRatSponer : MonoBehaviour
{
    public List<GameObject> rats;
    public int[] lastTime = new int[2];
    public float difecolty;
    public float timer = 0;

    void SpaneRat(int ratTyp)
    {
        GameObject rat = Instantiate(rats[ratTyp], transform.position, transform.rotation);
        rat.GetComponent<RatBase>().session = GetComponent<Session>();
        rat.GetComponent<RatBase>().OnStart(0.01f);

        SetBigRat(rat, Mathf.Log(1 + Random.Range(0.25f, 1f) * difecolty, 2));
    }

    void SetBigRat(GameObject rat, float scaleProsent)
    {
        rat.GetComponent<RatBase>().health *= scaleProsent;
        rat.GetComponent<RatBase>().speed *= scaleProsent;
        rat.GetComponent<RatBase>().value *= scaleProsent;
        rat.GetComponent<RatBase>().damage *= scaleProsent;
        rat.transform.localScale *= scaleProsent;
    }

    void Update()
    {
        difecolty += Time.deltaTime * 0.01f;

        timer += Time.deltaTime * difecolty * 0.25f;

        if (Mathf.Round(timer) != 0)
        {
            int time = Mathf.RoundToInt(timer);

            if (time % 2 == 0 && lastTime[0] != time) 
            {
                SpaneRat(0); lastTime[0] = time;
            }

            if (time % 4 == 0 && lastTime[1] != time)
            {
                SpaneRat(1); lastTime[1] = time;
            }
        }

        if (timer > difecolty)
        { 
            timer = 0;
            for (int i = 0; i < lastTime.Length; i++)
                lastTime[i] = -1;
        }
    }
}
