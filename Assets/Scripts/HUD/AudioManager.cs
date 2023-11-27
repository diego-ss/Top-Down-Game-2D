using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Don't destroy this object when loading a new scene
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }   

    public void SwitchAndPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
