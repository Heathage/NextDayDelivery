using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingObjects : MonoBehaviour
{
    public bool canDrop;
    private Rigidbody rb;
    [SerializeField][Tooltip("This is the child light source game object")]
    private Light lightSource;
    [SerializeField][Tooltip("This is the lamp material")]
    private Material lightEmission;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        if(lightSource != null)
        {
            lightSource.enabled = true;
        }
        if (lightEmission != null)
        {
            lightEmission.EnableKeyword("_EMISSION");
            lightEmission.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canDrop)
        {
            rb.isKinematic = false;
            if (lightSource != null)
            {
                lightSource.enabled = false;
            }
            if (lightEmission != null)
            {
                lightEmission.DisableKeyword("_EMISSION");
                lightEmission.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            }
        }
    }
}
