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
    public WaveManager waveManager;
    float totolTimer = 0;
    void SpaneRat(int ratTyp)
    {
        //GameObject rat = Instantiate(rats[ratTyp], transform.position, transform.rotation);
        //rat.GetComponent<RatBase>().session = GetComponent<Session>();
        //rat.GetComponent<RatBase>().OnStart(0.01f);

        waveManager.SponeRat(rats[ratTyp], Mathf.Log(1 + Random.Range(0.25f, 1f) * difecolty*difecolty, 2));
    }

    void Update()
    {

        if (waveManager.currentWaveIndex > StartRandomSponing)
        {
            difecolty += Time.deltaTime * difecolty*0.01f;

            timer += Time.deltaTime * 2;
            totolTimer += Time.deltaTime * 2;
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

            if (totolTimer > 5)
            { waveManager.session.NextWave(); totolTimer = 0; }

            if (timer > 2.5f)
            {
                timer = 0;
                for (int i = 0; i < lastTime.Length; i++)
                    lastTime[i] = -1;
            }


        }
    }
}
