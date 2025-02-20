using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRegular : MonoBehaviour
{
    Animator anim;
    //bool Opening;

    public bool Locked;
    public bool Opened = false;

    public GameObject uielement;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!Opened)
        {
            if (Locked == false)
            {
                anim.SetBool("Open?", true);
                Opened = true;
                uielement.SetActive(false);
            }
            else
            {
                anim.SetTrigger("Locked");
                Debug.Log("locked!!!");
            }
        }

        else if(Opened)
        {
            anim.SetBool("Open?", false);
            Opened = false;
        }
    }
        
    //}

    void OnTriggerExit()
    {
        //anim.SetBool("Open?", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
