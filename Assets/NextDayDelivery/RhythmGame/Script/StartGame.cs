using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class StartGame : MonoBehaviour
{
    public int counter = 0;
    public Canvas menu;
    public GameObject enemygameobject;
    void Interact()
    {
        if(counter == 0)
        {
            menu.GetComponent<MenuHandler>().attatchedTerminal = this.name.ToString();
            MonoBehaviour[] playerScripts = GameObject.Find("Player").GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in playerScripts)
            {
                script.enabled = false;
            }
            MonoBehaviour[] canvasScripts = GameObject.Find("Canvas").GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in canvasScripts)
            {
                script.enabled = false;
            }


            SingletonClass.Instance.Drones.SetActive(false);

            Camera.main.GetComponent<MouseLook>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Instantiate(menu);
            counter += 1;
        }
    }


}