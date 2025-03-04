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
        if (!AS.isPlaying)
            PlayNewSong();
    }

    void PlayNewSong()
    {
        AS.Stop();

        AudioClip songToPlay = music[Random.Range(0, music.Count)];
        
        AS.clip = songToPlay;
        AS.Play();
    }

    private void OnValidate()
    {
        UnityEditorInternal.ComponentUtility.MoveComponentUp(this);

        AS = GetComponent<AudioSource>();
        AS.outputAudioMixerGroup = mixerGroup;
    }
}
