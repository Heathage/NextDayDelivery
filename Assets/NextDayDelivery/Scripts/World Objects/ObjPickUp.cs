using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPickUp : MonoBehaviour
{
    Vector3 objectPos;

    [Header("Camera freeze reference")]
    public MouseLook freezeCam;

    [Header("Player Object Interaction")]
    [SerializeField]
    private bool holding = false;
    [SerializeField]
    private bool inspecting = false;
    public float fadeSpeed = 0.035f;
    public float transparentAlpha = 0.5f;
    public float solidAlpha = 1f;

    [Header("Object References")]
    [SerializeField]
    public GameObject item;
    [SerializeField]
    public GameObject tempParent;

    [Header("The Yeet")]
    [SerializeField]
    private float throwForce;
    public bool wasThrown = false;

    public void Interact()
    {
        Debug.Log("Interacted");

        //Picks up item if player isn't holding anything and disbales the gravity on object.
        if (holding == false)
        {
            holding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
        }

        //Drops the item if it is currently held.
        //Unfreezes the camera if the player was in the middle of inspecting.
        else if (holding == true)
        {
            holding = false;
            freezeCam.canLook = true;
        }
    }

    void Update()
    {
        throwForce = 1000;

        if (holding)
        {
            heldItem();

            if (Input.GetKey(KeyCode.J))
            {
                Debug.Log("IT'S OVER 9000!");
                throwForce = 9001;
            }

            if (Input.GetMouseButtonDown(1))
            {
                yeet();
            }

            if (Input.GetMouseButtonDown(2))
            {
                inspect();
            }

            if (inspecting)
            {
                rotate();
            }

            //this.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
        }

        else
        {
            dropped();
            inspecting = false;
        }
    }
    
    //Disables certain Rigidbody elements so that the object is held in place.
    void heldItem()
    {
        //Makes the object slightly see through for Quality of Life.
        //As long as its not being inspected. 
        if (!inspecting)
        {
            StartCoroutine(FadeColour(this.gameObject, fadeSpeed, transparentAlpha));
        }

        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //Sets the position of the item to the specified empty game object space for player held items.
        item.transform.SetParent(tempParent.transform, false);

        if (!inspecting)
        {
            item.transform.localPosition = new Vector3(0, 0.125f, 0);
        }

        //Ensures the item is of a certain rotation when it is not being inspected.
        //Keeps player view clear.
        if (!inspecting)
        {
            item.transform.localRotation = Quaternion.Euler(0, 90, 15); 
        }
    }

    //Changes the colour of the object over time to make it transparent/solid.
    private IEnumerator FadeColour(GameObject worldObject, float speed, float alphaTarget)
    {
        Color currentColour = worldObject.GetComponent<Renderer>().material.color;

        float t = 0;
        while (t < 1)
        {
            t += speed;

            float alpha = currentColour.a;
            currentColour.a = Mathf.Lerp(alpha, alphaTarget, t);

            worldObject.GetComponent<Renderer>().material.color = currentColour;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    //Throws player held item.
    void yeet()
    {
        //Raises the item just before throwing for better throw distance and aim.
        item.transform.localPosition = new Vector3(0, 0.75f, 0);
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
        freezeCam.canLook = true;
        holding = false;
        wasThrown = true;
    }

    //Drops the item in front of the player.
    //Returns rigidbody elements back to normal state.
    void dropped()
    {
        objectPos = item.transform.position;
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item.transform.position = objectPos;

        //Returns object to full colour.
        StartCoroutine(FadeColour(this.gameObject, fadeSpeed, solidAlpha));
    }

    //Allows the player to rotate the held object.
    void inspect()
    {
        //Freezes the camera so the player can use the mouse to rotate the object. 
        if (!inspecting)
        {
            inspecting = true;
            freezeCam.canLook = false;

            item.transform.localPosition = new Vector3(0, 0.5f, 0.5f);

            //Reverts to full colour for inspection.
            StartCoroutine(FadeColour(this.gameObject, fadeSpeed, solidAlpha));
        }

        //Unfreezes the camera so the player can look around again.
        else
        {
            inspecting = false;
            freezeCam.canLook = true;
        }
    }

    //Allows the player to use the mouse to rotate the object. 
    private void rotate()
    {
        item.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0), Space.Self);
    }
}
