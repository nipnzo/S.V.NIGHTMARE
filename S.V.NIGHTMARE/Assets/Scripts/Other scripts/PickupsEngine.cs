using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupsEngine : MonoBehaviour
{
    public static int  Score;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Score++;
            PlayerDetection.isPlayerInRange = true;
        }
    }
}
