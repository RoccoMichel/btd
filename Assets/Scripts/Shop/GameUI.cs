using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [HideInInspector] public Session session;

    [Header("UI References")]
    [SerializeField] internal TMP_Text balanceDisplay;
    [SerializeField] internal TMP_Text wavesDisplay;
    [SerializeField] internal TMP_Text healthDisplay;
    [SerializeField] internal Slider healthBar;

    private void Start()
    {
        if (session == null)
        {
            try { session = FindAnyObjectByType<Session>().GetComponent<Session>(); }
            catch { Debug.LogError("No session script was found in active scene"); }
        }

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
        healthBar.maxValue = session.maxHealth;
        healthBar.value = session.health;
    }

    public void ToggleOptions()
    {
        // not done lol cause we got no settings dawg
    }

    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
