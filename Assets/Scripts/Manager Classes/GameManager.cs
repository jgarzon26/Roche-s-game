using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;

    public static GameManager Instance { get =>  m_Instance; }

    public bool HasEnteredCombat { get; set; }

    private void Awake()
    {
        m_Instance = this;
        HasEnteredCombat = false;
    }

    private void Start()
    {
        
    }
}
