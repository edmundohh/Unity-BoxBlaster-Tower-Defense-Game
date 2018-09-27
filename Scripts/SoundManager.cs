using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource musicsource;

    [SerializeField]
    private AudioSource sfx;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    // Use this for initialization
    void Start()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio") as AudioClip[];

        foreach (AudioClip clip in clips)
        {
            audioClips.Add(clip.name, clip);
        }
    }

    // Update is called once per frame


    public void PlaySFX(string name){
        sfx.PlayOneShot(audioClips[name]);
    }
}
