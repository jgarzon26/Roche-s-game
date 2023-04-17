using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton_1 : MonoBehaviour
{
  public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
