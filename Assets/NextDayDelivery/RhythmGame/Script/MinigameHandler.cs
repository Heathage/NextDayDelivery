using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHandler : MonoBehaviour
{
    public GameObject localMG;
    private Vector3 angles;
    private Camera minigameCam;
    public GameObject minigame;
    public int spawnTotal = 1;
    public bool destroy;
    Vector3 MGLocation;

    void Awake()
    {
        Vector3 newRotate = new Vector3(0, 180, 0);
        angles = transform.rotation.eulerAngles;
        angles = newRotate;
        MGLocation = new Vector3(0, -50, 0);
        spawnTotal += 1;
        if (spawnTotal == 1)
        {
            localMG = (GameObject)Instantiate(minigame, MGLocation, Quaternion.Euler(angles));
            minigameCam = localMG.transform.Find("Camera").GetComponent<Camera>();
            spawnTotal = 2;
        }
    }

    void Update()
    {
            if (minigameCam.enabled != true)
            {
                minigameCam.enabled = true;
            }
            if (destroy == true)
            {
                spawnTotal = 0;
                minigameCam.enabled = false;
                Destroy(minigameCam);
                Destroy(localMG);
                Destroy(gameObject);
            }
    }
}