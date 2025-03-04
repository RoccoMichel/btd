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

    [Space(15)]
    public TMP_Text musicText;
    public TMP_Text soundText;
    public TMP_Text uiText;

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
        musicText.text = "Music: " + PlayerPrefs.GetFloat("Music", 100);
        soundText.text = "Sound Effects: " + PlayerPrefs.GetFloat("Sound", 100);
        uiText.text = "UI Sound: " + PlayerPrefs.GetFloat("UI", 100);

        musicInput.text = PlayerPrefs.GetFloat("Music", 100).ToString();
        soundInput.text = PlayerPrefs.GetFloat("Sound", 100).ToString();
        uiInput.text = PlayerPrefs.GetFloat("UI", 100).ToString();

        float musicVolume = Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("Music", 100) / 100);
        music.SetFloat("Music", musicVolume);

        float soundVolume = Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("Sound", 100) / 100);
        sound.SetFloat("Sound", soundVolume);

        float uiVolume = Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("UI", 100) / 100);
        ui.SetFloat("UISound", uiVolume);

        musicSlider.value = PlayerPrefs.GetFloat("Music", 100);
        soundSlider.value = PlayerPrefs.GetFloat("Sound", 100);
        uiSlider.value = PlayerPrefs.GetFloat("UI", 100);
    }

    private void Start()
    {
        UpdateVolume();
    }
}
