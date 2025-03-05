using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AddSoundToButton : MonoBehaviour
{
    public List<AudioClip> soundEfects;
    public List<Button> exclude;

    public List<GameObject> unLoadOnStart;

    public AudioClip buttonPlaySound;

    [HideInInspector]
    public AudioSource AS;

    private void Start()
    {
        for (int i = 0; i < unLoadOnStart.Count; i++)
            unLoadOnStart[i].SetActive(true);

        AS = GetComponent<AudioSource>();

        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        List<Button> buttonsList = buttons.ToList();
        
        for(int i = 0; i < buttonsList.Count; i++)
        {
            for(int y = 0; y < exclude.Count; y++)
            {
                if (buttonsList[i] != exclude[y])
                {
                    buttonsList[i].onClick.AddListener(PlaySound);
                }
            }
        }

        for (int i = 0; i < unLoadOnStart.Count; i++)
            unLoadOnStart[i].SetActive(false);
    }

    public void PlaySound()
    {
        AS.Stop();
        AS.clip = soundEfects[Random.Range(0, soundEfects.Count - 1)];
        AS.Play();
    }

    public void PlaySelectedSound()
    {
        AS.Stop();
        AS.clip = buttonPlaySound;
        AS.Play();
    }
}