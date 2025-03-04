using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AddSoundToButton : MonoBehaviour
{
    public List<AudioClip> soundEfects;
    public List<Button> exclude;
    public static List<Button> staticExclude = new();

    public static AudioSource AS;

    public static AudioClip defultPlayClip;

    private void Start()
    {
        AS = GetComponent<AudioSource>();

#if UNITY_EDITOR
        defultPlayClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/SoundEfects/UI/LevelSelected.wav");
#endif

        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        List<Button> buttonsList = buttons.ToList();

        for (int i = 0; i < staticExclude.Count; i++)
            exclude.Add(staticExclude[i]);
        
        for(int i = 0; i < buttonsList.Count; i++)
        {
            for(int y = 0; y < exclude.Count; y++)
            {
                if (buttonsList[i] == exclude[y])
                {
                    buttonsList.Remove(buttonsList[i]);
                    i++;
                }
            }

            buttonsList[i].onClick.AddListener(PlaySound);
        }
    }

    public void PlaySound()
    {
        AS.Stop();
        AS.clip = soundEfects[Random.Range(0, soundEfects.Count + 1)];
        AS.Play();
    }

    public static void PlaySelectedSound(AudioClip sound = null)
    {
        if (sound == null)
            sound = defultPlayClip;

        AS.Stop();
        AS.clip = sound;
        AS.Play();
    }
}