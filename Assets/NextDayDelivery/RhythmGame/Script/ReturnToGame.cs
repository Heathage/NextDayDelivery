using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToGame : MonoBehaviour
{
    public string terminal;

    public void Leave()
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

        Camera.main.GetComponent<MouseLook>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Input.GetMouseButtonDown(1);

        terminal = this.GetComponentInParent<MenuHandler>().attatchedTerminal;
        GameObject.Find(terminal).GetComponent<StartGame>().counter = 0;

        SingletonClass.Instance.Drones.SetActive(true);
       
    }
}
