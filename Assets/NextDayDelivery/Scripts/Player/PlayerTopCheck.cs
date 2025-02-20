using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopCheck : MonoBehaviour
{
    public GameObject stepLadder;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            stepLadder.GetComponent<Climb>().atTop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stepLadder.GetComponent<Climb>().atTop = false;
        }
    }
}
