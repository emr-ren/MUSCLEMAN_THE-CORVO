using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] theEffects;
    [SerializeField] private AudioSource[] theMusic;


    public bool playTheMusic;
    private int theMusicIndex;

    private void Awake()
    {   //Sadece bir tane manager olması icin yok ettim skillde engellemesin
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }

    private void Update()
    {
        if (!playTheMusic)
            StopAllTheMusic();
        else
        {
            if (!theMusic[theMusicIndex].isPlaying)         
                PlayTheMusic(theMusicIndex);

        }
             
    }

    public void PlayTheEffect(int _theEffects)
    {
        if (_theEffects < theEffects.Length)
        {
            theEffects[_theEffects].Play();
        }
    }

    public void StopTheEffect(int _index) => theEffects[_index].Stop();

    public void PlayerRandomTheMusic()
    {
        theMusicIndex = Random.Range(0,theMusic.Length);
        PlayTheMusic(theMusicIndex);
    }
    
    public void PlayTheMusic(int _theMusic)
    {
        theMusicIndex = _theMusic;

        StopAllTheMusic();
        theMusic[theMusicIndex].Play();

    }

    public void StopAllTheMusic()
    {
        for (int i = 0; i < theMusic.Length; i++)
        {
            theMusic[i].Stop();
        }
    }
}
