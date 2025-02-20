using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMOD;
using System;
using UnityEditor;

public class Spawner : MonoBehaviour
{
    FMOD.Studio.EventInstance Audiooo;
    public int level = 99;
    public int counter = 0;
    public int fail = 12;
    public string terminal;
    public Material successMaterial;
    public Material failureMaterial;
    public GameObject note;

    private int levelDelay = 0;
    private string[][] levels = new string[][] { new string[] { "w","p", "space", "w", "p", "space", "d", "p", "d", "space", "p" },
        new string[] { "d","p", "l", "p", "w", "d", "l", "p", "space", "w", "p"},
        new string[] { "p", "p","l", "d","p", "w", "d", "l", "d", "w","p"},
        new string[] { "e", "l","space", "p", "d", "space", "w", "p", "space", "l", "w", "p", "d", "space", "w", "d", "p", "l", "space", "p", "d", "l"},
        new string[] { "u", "space", "space", "u", "space", "u", "space", "q", "f", "j", "m", "v", "j", "p", "m"},
        new string[] {"n","u","l","l"},
        new string[] { "space","z", "f", "z", "v", "v", "m", "j", "l", "v", "r", "space", "space", "j", "l", "space", "q", "r", "space", "u", "p"},
        new string[] {"a", "v","j", "m", "r", "u", "f", "v", "z", "f", "j", "m", "a", "f", "v", "m", "l", "m"},
        new string[] {"w", "d", "w", "space", "l", "p", "l", "space", "w", "p","d", "l"} };
    //A multidimentional string array that's output by the spawner.

    private float[][] levelTimes = new float[][] { new float[] {3f,7,16,13,9,13,4,11,10,10,10}, //Each number in this array represents the amount of time between each note. Each number is in seconds /10 - so 3 in this case is = 0.3 seconds.
        new float[] {20f,7f,12,3,12,7,6,7,19,4,15,3},
        new float[] {20f,3f,14,4,14,7,6,14,8,6,6},
        new float[] {6,9.7f,4,6,6, 9.7f, 4,6,6, 9.7f, 4,6,6, 9.7f, 4,6, 6, 9.7f, 4, 6, 6, 9.7f},
        new float[] {3f,6,6,6,7,8,18,6,8,4,7,5,13,4,3},
        new float[] {1,1,1,1},
        new float[] {1.5f,6,9,5,8,5,1,4,8,5,3,7,5,2.5f,6,4.5f,3,5,4,4,6,5},
        new float[] {6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f, 6.5f},
        new float[] {13,13,13,13,13,13,13,13,13,13,13,13}};

    void Awake()
    {
        StartCoroutine(LevelTiming()); //Allows the script to use WaitForSeconds
        Audiooo = FMODUnity.RuntimeManager.CreateInstance("event:/Audiooo");
        Audiooo.setPaused(true);
        Audiooo.setVolume(0.1f);
       
    }

    void Update()
    {
        if (level != 99)
        {
            if(levelDelay == 0)
            {
                Audiooo.setParameterByName("Section", level + 1);
                Audiooo.start();
                levelDelay = 1;
            }
            Audiooo.setParameterByName("Success Level", fail);
            if (fail <= -4)
            {
                Audiooo.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                Audiooo.release();
                OnDefeat();
                GameObject.Find("HackHandler(Clone)").GetComponent<MinigameHandler>().destroy = true;
            }

            if (counter == levels[level].Length)
            {
                Audiooo.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Audiooo.release();
                UnityEngine.Debug.Log("Congrats!"); //here use varying effects based off of the level value. EG if you beat level 1, then effect no 1 will happen which should be opening the door. Maybe create a nested array of effects like the level and timing arrays?
                OnVictory();
                GameObject.Find("HackHandler(Clone)").GetComponent<MinigameHandler>().destroy = true;

                

            }
            if (counter == 1)
            {
                Audiooo.setPaused(false);
            }
        }
    }

    public void OnVictory()
    {
        ReturnToGame();
        GameObject.Find(terminal).transform.GetChild(0).GetComponent<MeshRenderer>().materials[1] = successMaterial;
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find(terminal).GetComponent<Button>().activate = true;
        Cursor.visible = false;
        Input.GetMouseButtonDown(1);

    }

    public void OnDefeat()
    {
        ReturnToGame();
        GameObject.Find(terminal).transform.GetChild(0).GetComponent<MeshRenderer>().materials[1] = failureMaterial;
        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Input.GetMouseButtonDown(1);
        UnityEngine.Debug.Log("the terminal should be" + terminal);
        GameObject.Find(terminal).GetComponent<StartGame>().counter = 0;
    }

    public void ReturnToGame()
    {
        MonoBehaviour[] playerScripts = GameObject.Find("Player").GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = true;
        }
        MonoBehaviour[] canvasScripts = GameObject.Find("Canvas").GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in canvasScripts)
        {
            script.enabled = true;
        }

        SingletonClass.Instance.Drones.SetActive(true);
    }

    IEnumerator LevelTiming() //Lanuches a time sensitive function
    {
        for (int i = 0; i < levels[level].Length; i++) // for each item in the chosen level
        {
            if (counter >= 0)
            {
                levelTimes[level][i] *= 0.1f;
            }
            note.GetComponent<Keypress>().keyType = (levels[level][i]); //Sets the keytype of the note to the current item in the array
            Instantiate(note, gameObject.transform); //instantiates each note
            yield return new WaitForSeconds(levelTimes[level][i]); //Waits for an amount of time before spawing the next note specified by the time array and divides it by 10, to allow much quicker times to be allowed.
        }
    }
}