using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
   private int parts = 0;
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("parts"))
        {
            Destroy(other.gameObject);
            parts++;
            Debug.Log("Parts Collected: " + parts);
        }
    }
}
