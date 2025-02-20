using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FMOD;

public class MenuHandler : MonoBehaviour
{
    FMOD.Studio.EventInstance Audiooo;
    public string attatchedTerminal;
    void Start()
    {
        Audiooo = FMODUnity.RuntimeManager.CreateInstance("event:/Audiooo");
        Audiooo.start();
        Audiooo.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}