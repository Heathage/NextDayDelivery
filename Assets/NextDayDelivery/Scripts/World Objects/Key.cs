using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject key;
    public DoorRegular door;
    public GameObject uielement;

    public void Interact()
    {
        uielement.SetActive(true);
        Debug.Log("UNLOCKED");
        door.Locked = false;
        gameObject.SetActive(false);
    }
}
