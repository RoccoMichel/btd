// All Code By Charlie

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public List<AudioClip> music;

    AudioSource AS;

    public AudioMixerGroup mixerGroup;

    private void Update()
    {
        // If Song Is Done Then Play A New Song
        if (!AS.isPlaying)
            PlayNewSong();
    }

    void PlayNewSong()
    {
        // Stops The Song
        AS.Stop();

        // Picks A Song To Play
        AudioClip songToPlay = music[Random.Range(0, music.Count)];
        
        // Plays That Song
        AS.clip = songToPlay;
        AS.Play();
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        // Moves The Script To The Top Cuz It Looks Better :)
        UnityEditorInternal.ComponentUtility.MoveComponentUp(this);
#endif
        // Sets Some Setting To The Audio Source
        AS = GetComponent<AudioSource>();
        AS.outputAudioMixerGroup = mixerGroup;
    }
}
