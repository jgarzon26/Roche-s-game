using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;

    public static AudioManager Instance { get => m_Instance; }

    private AudioSource[] m_BGMusics;


    private void Awake()
    {
        m_Instance = this;
        m_BGMusics = GetComponentsInChildren<AudioSource>();
    }

    public void PlayExplore()
    {
        m_BGMusics[1].Stop();
        m_BGMusics[0].Play();
    }

    public void PlayCombat()
    {
        m_BGMusics[0].Stop();
        m_BGMusics[1].Play();
    }
}
