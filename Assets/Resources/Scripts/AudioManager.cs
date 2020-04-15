using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;

    public AudioClip click;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // If sucess then keep it
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = GameData.d.musicOn ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMusic(int numAudio)
    {
        music1.Stop();
        music2.Stop();
        music3.Stop();

        switch (numAudio)
        {
            case 0:
                music1.Play();
                break;
            case 1:
                music2.Play();
                break;
            default:
                music3.Play();
                break;
        }
    }

    public void MuteUnmute()
    {
        if (GameData.d.musicOn)
        {
            GameData.d.musicOn = false;
            AudioListener.volume = 0;
        }
        else
        {
            GameData.d.musicOn = true;
            AudioListener.volume = 1;
        }
    }

    public void playClick()
    {
       var audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(click, 0.7F);
    }
}
