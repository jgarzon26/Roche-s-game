using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backbutton : MonoBehaviour
{
    
   public GameObject CreditPanel;

   public void OpenPanel()
   {
    if(CreditPanel != null)
    {
        CreditPanel.SetActive(true);
    }
   }

   public void ClosePanel()
   {
    if(CreditPanel != null)
    {
        CreditPanel.SetActive(false);
    }
   }

   
}


