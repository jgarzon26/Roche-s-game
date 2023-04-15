using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get =>  instance; }

    [SerializeField]
    private Slider m_HealthSlider;
    [SerializeField]
    private Slider m_ManaSlider;

    private void Awake()
    {
        instance = this;
    }

    public void SetMaxHealth(int health)
    {
        m_HealthSlider.maxValue = health;
    }

    public void SetCurrentHealth(int health)
    {
        m_HealthSlider.value = health;
    }

    public void SetMaxMana(int mana)
    {
        m_ManaSlider.maxValue = mana;
    }

    public void SetCurrentMana(int mana)
    {
        m_ManaSlider.value = mana;
    }
}
