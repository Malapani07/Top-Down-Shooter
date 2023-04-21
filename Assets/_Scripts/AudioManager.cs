
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {

            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("HorrorSound");
    }
    public void Play(string name)
    {
        Sound S = Array.Find(sounds, sound => sound.name == name);
        if (S == null) { return; };
        S.Source.Play();
    }

}
