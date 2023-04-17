using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelopen : MonoBehaviour
{
   public GameObject CreditPanel;

   public void OpenPanel()
   {
    if(CreditPanel != null)
    {
        CreditPanel.SetActive(true);
    }
   }
}
