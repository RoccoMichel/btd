using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class RandomRatSponer : MonoBehaviour
{
    public List<GameObject> rats;
    public int[] lastTime = new int[4];
    public float difecolty;
    public float timer = 0;
    public int StartRandomSponing = 11;
    float totolTimer = 0;
    void SpaneRat(int ratTyp)
    {
        GameObject rat = Instantiate(rats[ratTyp], transform.position, transform.rotation);
        rat.GetComponent<RatBase>().session = GetComponent<Session>();
        rat.GetComponent<RatBase>().OnStart(0.01f);

       if (difecolty > 2f) SetBigRat(rat, Mathf.Log(1 + Random.Range(0.25f, 1f) * difecolty, 2));
    }

    void SetBigRat(GameObject rat, float scaleProsent)
    {
        rat.GetComponent<RatBase>().health *= scaleProsent;
        rat.GetComponent<RatBase>().speed *= scaleProsent;
        //rat.GetComponent<RatBase>().value *= scaleProsent;
        rat.GetComponent<RatBase>().damage *= scaleProsent;
        //rat.transform.localScale *= scaleProsent;
    }

    void Update()
    {

        if (GetComponent<WaveManager>().currentWaveIndex > StartRandomSponing)
        {

            difecolty += Time.deltaTime * 0.01f;

            timer += Time.deltaTime * 10;
            totolTimer += Time.deltaTime * 10;
            if (Mathf.Round(timer) != 0)
            {
                int time = Mathf.RoundToInt(timer);

                if (time % 0.5f == 0 && lastTime[0] != time)
                    SpaneRat(0); lastTime[0] = time;

                if (time % 1 == 0 && lastTime[1] != time)
                    SpaneRat(1); lastTime[1] = time;

                if (time % 1.5f == 0 && lastTime[2] != time)
                    SpaneRat(2); lastTime[2] = time;

                if (time % 2 == 0 && lastTime[3] != time)
                    SpaneRat(3); lastTime[3] = time;

                if (time % 2.5f == 0 && lastTime[4] != time)
                    SpaneRat(4); lastTime[4] = time;

                //if (time % 4f == 0 && lastTime[5] != time)
                //    SpaneRat(5); lastTime[5] = time;
            }

            if (Mathf.Round(totolTimer) % 5 == 0)
                GetComponent<WaveManager>().currentWaveIndex++;

            if (timer > difecolty)
            {
                timer = 0;
                for (int i = 0; i < lastTime.Length; i++)
                    lastTime[i] = -1;
            }


        }
    }
}
