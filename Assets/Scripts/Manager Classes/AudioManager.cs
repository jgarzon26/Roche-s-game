using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;

    public static AudioManager Instance { get => m_Instance; }

    [SerializeField]
    private AudioClip _exploreMusic;
    [SerializeField]
    private AudioClip _combatMusic;

    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_Instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayExplore();
    }

    public void PlayExplore()
    {
        if (m_AudioSource.clip != _exploreMusic) m_AudioSource.Stop();
        if(!m_AudioSource.isPlaying)
        {
            m_AudioSource.clip = _exploreMusic;
            m_AudioSource.Play();
        }
    }

    public void PlayCombat()
    {
        if(m_AudioSource.clip != _combatMusic) m_AudioSource.Stop();
        if (!m_AudioSource.isPlaying)
        {
            m_AudioSource.clip = _combatMusic;
            m_AudioSource.Play();
        }
    }
}
