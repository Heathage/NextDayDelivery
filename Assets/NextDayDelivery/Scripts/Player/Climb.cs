using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public GameObject player;

    public GameObject bottom;
    public GameObject top;
    public Transform currentPosition;

    public bool atBottom = false;
    public bool atTop = false;

    [SerializeField]
    private float climbSpeed = 0.25f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(atBottom)
            {
                currentPosition = player.transform;
                player.transform.position = top.transform.position;
                atBottom = false;
            }

            if(atTop)
            {
                currentPosition = player.transform;
                player.transform.position = bottom.transform.position;
                atTop = false;
            }
            
            else
            {

            }
        }
    }
}
