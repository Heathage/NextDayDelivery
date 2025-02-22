﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomCheck : MonoBehaviour
{
    public GameObject stepLadder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stepLadder.GetComponent<Climb>().atBottom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stepLadder.GetComponent<Climb>().atBottom = false;
        }
    }
}
