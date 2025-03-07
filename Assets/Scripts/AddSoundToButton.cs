// All Code By Charlie

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
        // Setts All Game Objects To Active So The Script Can Find All Buttons
        for (int i = 0; i < unLoadOnStart.Count; i++)
            unLoadOnStart[i].SetActive(true);

        // Sets The Audi Source To The One On The Game Object
        AS = GetComponent<AudioSource>();

        // Fids All Buttons In The Secene
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        // Makes It A List Cuz I Like Them More
        List<Button> buttonsList = buttons.ToList();
        
        // Cheks If Any Button Is Part Of The Exclude List
        // And If Not, Adds The Listener PlaySound To It
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

        // Sets All The Game Objects In The List To Inactive
        for (int i = 0; i < unLoadOnStart.Count; i++)
            unLoadOnStart[i].SetActive(false);
    }

    // Plays A Random Sound From The List soundEfects
    public void PlaySound()
    {
        AS.Stop();
        AS.clip = soundEfects[Random.Range(0, soundEfects.Count - 1)];
        AS.Play();
    }

    // Plays The buttonPlaySound Insted Of A Random One
    public void PlaySelectedSound()
    {
        AS.Stop();
        AS.clip = buttonPlaySound;
        AS.Play();
    }
}