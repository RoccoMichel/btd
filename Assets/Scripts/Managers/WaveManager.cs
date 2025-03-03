using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

public class WaveManager : MonoBehaviour
{
    [Header("Elements")]
    /*  
      [SerializeField] private Player player;
      [SerializeField] private WaveManagerUI waveManagerUI;
    */
    [SerializeField] private Session sesion;

    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    private int currentWaveIndex;
    private float waveTransitionTimer;
   [SerializeField] private float waveTransitionTime;


    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();


    private void Start()
    {
        StartNextWave();
    }
    private void StartWave(int waveIndex)
    {
       // waveManagerUI.UpdateWaveText("Wave " + (currentWaveIndex + 1));
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
        StartWave(currentWaveIndex);
    }
    private void Update()
    {
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

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("Waves completed");

            /* waveManagerUI.UpdateTimerText("");
             waveManagerUI.UpdateWaveText("Waves Completed");
             GameManager.Instance.SetGameState(GameState.STAGECOMPLETE);*/
        }
        else
        {
            waveTransitionTimer += Time.deltaTime;
            if(waveTransitionTimer>= waveTransitionTime)
            {
                waveTransitionTimer = 0f;
                StartNextWave();


            }
        }
       // else GameManager.Instance.WaveCompletedCallback();
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
                GameObject rat = Instantiate(segment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                rat.GetComponent<RatBase>().session = sesion;
                rat.GetComponent<RatBase>().OnStart();


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
        Vector2 targetPos = new Vector2(0, 0);

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

