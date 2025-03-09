// All Code By Charlie

using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer music, sound, ui;

    [Space(15)]
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider uiSlider;

    [Space(15)]    
    public TMP_InputField musicInput;
    public TMP_InputField soundInput;
    public TMP_InputField uiInput;

    float holdTime = 0;
    public GameObject resetUI;
    public TMP_Text resetText;
    float lastSec = -1;

    // Set The Volume Of Music With A Slider
    public void SetMusicVolumeSlider(float value)
    {
        PlayerPrefs.SetFloat("Music", value);

        UpdateVolume();
    }

    // Set The Volume Of Sound With A Slider
    public void SetSoundVolumeSlider(float value)
    {
        PlayerPrefs.SetFloat("Sound", value);

        UpdateVolume();
    }

    // Set The Volume Of UI With A Slider
    public void SetUIVolumeSlider(float value)
    {
        PlayerPrefs.SetFloat("UI", value);

        UpdateVolume();
    }

    // Set The Volume Of Music By Typing It In A Text Feild
    public void SetMusicVolumeInput(string input)
    {
        int value = int.Parse(input);

        if (value > 100)
            value = 100;
        if (value < 0)
            value = 0;

        PlayerPrefs.SetFloat("Music", value);

        UpdateVolume();
    }

    // Set The Volume Of Sound By Typing It In A Text Feild
    public void SetSoundVolumeInput(string input)
    {
        int value = int.Parse(input);

        if (value > 100)
            value = 100;
        if (value < 0)
            value = 0;

        PlayerPrefs.SetFloat("Sound", value);

        UpdateVolume();
    }

    // Set The Volume Of UI By Typing It In A Text Feild
    public void SetUIVolumeInput(string input)
    {
        int value = int.Parse(input);

        if (value > 100)
            value = 100;
        if (value < 0)
            value = 0;

        PlayerPrefs.SetFloat("UI", value);

        UpdateVolume();
    }

    // Updates The Volume On The Volume Mixers
    public void UpdateVolume()
    {

        musicInput.text = PlayerPrefs.GetFloat("Music", 100).ToString();
        soundInput.text = PlayerPrefs.GetFloat("Sound", 100).ToString();
        uiInput.text = PlayerPrefs.GetFloat("UI", 100).ToString();

        // yo code was ass, i fixed with a bunch of shitty math. I hated this, why would you make me do this (you didn't)
        music.SetFloat("Music", Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("Music", 100), 0.0001f, 100) / 100) * 20);

        sound.SetFloat("Sound", Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("Sound", 100), 0.0001f, 100) / 100) * 20);

        ui.SetFloat("UISound", Mathf.Log10(Mathf.Clamp(PlayerPrefs.GetFloat("UI", 100), 0.0001f, 100) / 100) * 20);

        musicSlider.value = PlayerPrefs.GetFloat("Music", 100);
        soundSlider.value = PlayerPrefs.GetFloat("Sound", 100);
        uiSlider.value = PlayerPrefs.GetFloat("UI", 100);
    }

    private void Start()
    {
        UpdateVolume();
    }

    public void Return()
    {
        gameObject.SetActive(false);
    }
    // <summary>
    // Resets All PlayerPfres
    private void Update()
    {
        if(resetUI != null)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.R))
            {
                resetUI.SetActive(true);

                holdTime += Time.deltaTime;

                int secLeft = Mathf.CeilToInt(3 - holdTime);

                if (secLeft != lastSec && secLeft > 0)
                {
                    resetText.text = $"Reseting in: {secLeft}";
                    lastSec = secLeft;
                }

                if (holdTime >= 3)
                {
                    resetText.text = "Reseted All Progres";
                    ResetPlayerPrefs();
                }
            }
            else
            {
                resetUI.SetActive(false);

                holdTime = 0;
                lastSec = -1;
            }
        }
    }

    void ResetPlayerPrefs()
    {
        float musicVol = PlayerPrefs.GetFloat("Music");
        float soundVol = PlayerPrefs.GetFloat("Sound");
        float UIVol = PlayerPrefs.GetFloat("UI");

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("Music", musicVol);
        PlayerPrefs.SetFloat("Sound", soundVol);
        PlayerPrefs.SetFloat("UI", UIVol);
    }
    // <summary>
}
