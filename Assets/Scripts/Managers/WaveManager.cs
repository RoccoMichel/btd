using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEngine.Rendering.HableCurve;

public class WaveManager : MonoBehaviour
{
    [Header("Elements")]
    /*  
      [SerializeField] private Player player;
      [SerializeField] private WaveManagerUI waveManagerUI;
    */
    [SerializeField] private Session session;

    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    public int currentWaveIndex;
    private float waveTransitionTimer;
   [SerializeField] private float waveTransitionTime;


    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();

    public List<RatBase> BufNorm, BufChoky, BufMutent, BufSterods, BufTacnk;


    private void Start()
    {
        StartNextWave();
    }

    void SponeNewRat(GameObject Rat)
    {
        GameObject rat = Instantiate(Rat, GetSpawnPosition(), Quaternion.identity, transform);
        rat.GetComponent<RatBase>().session = session;
        rat.GetComponent<RatBase>().OnStart(0.01f);
    }
    void SponeOldRat(int type)
    {
        if (type == 0) { BufNorm[0].OnStart(0.01f); BufNorm.RemoveAt(0); return; }
        if (type == 1) { BufChoky[0].OnStart(0.01f); BufChoky.RemoveAt(0); return; }
        if (type == 2) { BufMutent[0].OnStart(0.01f); BufMutent.RemoveAt(0); return; }
        if (type == 3) { BufTacnk[0].OnStart(0.01f); BufTacnk.RemoveAt(0); return; }
        if (type == 4) { BufSterods[0].OnStart(0.01f); BufSterods.RemoveAt(0); return; }
    }

    public void SponeRat(GameObject rat)
    {
        if (rat.name == "Rat Normal" && BufNorm.Count != 0) { SponeOldRat(0); return;};
        if (rat.name == "RatNormal" && BufChoky.Count != 0) { SponeOldRat(1); return;};
        if (rat.name == "RatNormal" && BufMutent.Count != 0) { SponeOldRat(2); return;};
        if (rat.name == "RatNormal" && BufTacnk.Count != 0) { SponeOldRat(3); return;};
    }

    private void StartWave(int waveIndex)
    {
        GameUI.Instance.UpdateWavesDisplay(currentWaveIndex + 1);
        localCounters.Clear();
        foreach (WaveSegment segment in waves[waveIndex].segments)
        {
            localCounters.Add(1);
        }
        timer = 0;
        isTimerOn = true;
        Debug.Log("Starting wave " + currentWaveIndex);
    }
    private void StartNextWave()
    {
        session.NextWave();
        StartWave(currentWaveIndex);
    }
    private void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            if (transform.GetComponentInChildren<RatBase>() == null)
            {
                Debug.Log("Waves completed");
                PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().buildIndex}BeatGame", 1);

                GameUI.Instance.OnGameWin();
            }
        }
        if (!isTimerOn) return;
        if (timer < waveDuration)
        {
            MangeCurrentWave();

            string timerString = ((int)(waveDuration - timer)).ToString();
            //waveManagerUI.UpdateTimerText(timerString);
        }
        else
        {
            StartWaveTransition();
        }
    }

    private void StartWaveTransition()
    {
        isTimerOn = false;

        currentWaveIndex++;
        

        if ( PlayerPrefs.GetInt($"{SceneManager.GetActiveScene().buildIndex}Highscore", 0) < currentWaveIndex+1)
            PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().buildIndex}Highscore", currentWaveIndex + 1);

        if (currentWaveIndex >= waves.Length)
        {



           
        }
        else StartCoroutine(startNext());
       // else GameManager.Instance.WaveCompletedCallback();
    }

    public IEnumerator startNext()
    {
        yield return new WaitForSeconds(waveTransitionTime);
        StartNextWave();
    }
    private void MangeCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];

            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd) continue;

            float timeSinceSegmentStart = timer - tStart;

            if (timeSinceSegmentStart / (1f / segment.spawnFrequency) > localCounters[i])
            {
                SponeRat(segment.prefab);
                GameObject rat = Instantiate(segment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                rat.GetComponent<RatBase>().session = session;
                rat.GetComponent<RatBase>().OnStart(0.01f);


                localCounters[i]++;
            }
        }
        timer += Time.deltaTime;
    }

    private void ClearAllEnemies()
    {
        transform.Clear();
    }
    private Vector2 GetSpawnPosition()
    {
        Vector2 targetPos = new Vector2(1000, 1000);

        return targetPos;
    }

    /*public void GameStateChangedCallback(GameState gameState)
    {
        //Debug.Log("wavemanger knows gamestate is " + gameState);
        switch (gameState)
        {
            case GameState.GAME:
                StartNextWave();
                break;
            case GameState.GAMEOVER:
                isTimerOn = false;
                ClearAllEnemies();
                break;

        }
    }*/
}


[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}
[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}

