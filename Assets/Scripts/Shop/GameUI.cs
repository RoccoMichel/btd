using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }
    [HideInInspector] public Session session;

    [Header("UI References")]
    [SerializeField] internal GameObject settings;
    [SerializeField] internal GameObject pauseMenu;
    [SerializeField] internal GameObject shop;
    [SerializeField] internal GameObject loseScreen;
    [SerializeField] internal GameObject winScreen;
    [SerializeField] internal TMP_Text balanceDisplay;
    [SerializeField] internal TMP_Text wavesDisplay;
    [SerializeField] internal TMP_Text healthDisplay;
    [SerializeField] internal TMP_Text FastForwardText;
    [SerializeField] internal RectTransform healthBarMask, healthBarOutlineMask;
    Vector3 healthBarStartPos;

    float holdTime;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (session == null)
        {
            try { session = FindAnyObjectByType<Session>().GetComponent<Session>(); }
            catch { Debug.LogError("No session script was found in active scene"); }
        }
        if (settings != null) settings.SetActive(false);

        healthBarStartPos = healthBarOutlineMask.position;

        ToggleSettings(false);
        DisplayRefresh();
    }
    void FixedUpdate()
    {
        DisplayRefresh();
        balanceDisplay.color = Color.Lerp(balanceDisplay.color, Color.white, .1f);
    }

    void DisplayRefresh()
    {
        balanceDisplay.text = $"${session.balance}";
        healthDisplay.text = $"{session.health} HP";
        wavesDisplay.text = $"{session.wave}/{session.maxWaves}";

        healthBarMask.localPosition = new Vector3(Mathf.Lerp(-785, 30, session.health / session.maxHealth), 90, 0);

        healthBarOutlineMask.position = healthBarStartPos;
    }
    public void Stop()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    public void ToggleSpeed()
    {
        
        Time.timeScale = Time.timeScale == 1 ? 3 : 1;
        FastForwardText.gameObject.SetActive(Time.timeScale != 1);
    }

    public void ToggleShop()
    {
        if (shop == null) return;

        shop.SetActive(!shop.activeSelf);
    }

    public void OnGameLose()
    {
        loseScreen.SetActive(true);
    }
    public void OnGameWin()
    {
        winScreen.SetActive(true);
        pauseMenu.SetActive(false);
        settings.SetActive(false);
    }
    public void contiuAfterWin()
    {
        winScreen.SetActive(!true);
        pauseMenu.SetActive(!false);
        settings.SetActive(!false);
    }
    public void ToggleSettings()
    {
        if (settings == null) return;

        settings.SetActive(!settings.activeSelf);
        pauseMenu.SetActive(false);
    }

    public void ToggleSettings(bool state)
    {
        if (settings == null) return;

        settings.SetActive(state);
        pauseMenu.SetActive(false);
    }

    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        settings.SetActive(false);
    }
}