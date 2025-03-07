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

    //[Space(15)]
    //public TMP_Text musicText;
    //public TMP_Text soundText;
    //public TMP_Text uiText;

    public void SetMusicVolumeSlider(float value)
    {
        PlayerPrefs.SetFloat("Music", value);

        UpdateVolume();
    }

    public void SetSoundVolumeSlider(float value)
    {
        PlayerPrefs.SetFloat("Sound", value);

        UpdateVolume();
    }

    public void SetUIVolumeSlider(float value)
    {
        PlayerPrefs.SetFloat("UI", value);

        UpdateVolume();
    }

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

    public void UpdateVolume()
    {
        //musicText.text = "Music: " + PlayerPrefs.GetFloat("Music", 100);
        //soundText.text = "Sound Effects: " + PlayerPrefs.GetFloat("Sound", 100);
        //uiText.text = "UI Sound: " + PlayerPrefs.GetFloat("UI", 100);

        musicInput.text = PlayerPrefs.GetFloat("Music", 100).ToString();
        soundInput.text = PlayerPrefs.GetFloat("Sound", 100).ToString();
        uiInput.text = PlayerPrefs.GetFloat("UI", 100).ToString();

        float musicVolume = Mathf.Lerp(-70, 10, PlayerPrefs.GetFloat("Music", 100) / 100);
        music.SetFloat("Music", musicVolume);

        float soundVolume = Mathf.Lerp(-70, 10, PlayerPrefs.GetFloat("Sound", 100) / 100);
        sound.SetFloat("Sound", soundVolume);

        float uiVolume = Mathf.Lerp(-70, 10, PlayerPrefs.GetFloat("UI", 100) / 100);
        ui.SetFloat("UISound", uiVolume);

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
}
