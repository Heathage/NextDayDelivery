using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float speed = 1;
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2((Time.time * (speed/(399/100))) % 1, 0f); //Gets the texture renderer and moves it to the left continuously, then moves back once this is complete to create the image of a constantly moving grid.
    }

}
