using System.Collections;
using UnityEngine;

public class Leaning : MonoBehaviour
{
    public MouseLook freezeCam;

    [Header("Leaning Values")]
    public float leanDistance = 1f;
    public float leanAngle = 30f;
    public float leanSpeed = 0.1f;

    public Animator anim;

    float leanRightDistance;
    float leanLeftAngle;
    float leanLeftDistance;
    float leanRightAngle;

    [Header("Leaning Height")]
    [SerializeField]
    private float camHeight = 1.4f;
    [SerializeField]
    private float crouchCam = 0.5f;
    public float currentHeight;

    [SerializeField]
    private Crouching crouching;

    private Coroutine moveCoroutine;

    [Header("Check for Wall")]
    [SerializeField]
    private bool wallLeft;
    [SerializeField]
    private bool wallRight;

    void Start() 
    {
        leanRightDistance = leanDistance;
        leanLeftAngle = leanAngle;
        leanLeftDistance = leanDistance * -1;
        leanRightAngle = leanAngle * -1;
        currentHeight = camHeight;
        wallLeft = false;
        wallRight = false;
    }

    void Update()
    {
        if (Input.GetKey("e") && (!wallRight))
        {
            anim.SetInteger("Lean", 1);
            freezeCam.canLook = false;
        }

        else if (Input.GetKeyUp("e") || (wallRight))
        {
            anim.SetInteger("Lean", 0);
            freezeCam.canLook = true;
        }

        else if (Input.GetKey("q") && (!wallLeft))
        {
            anim.SetInteger("Lean", -1);
            freezeCam.canLook = false;
        }

        else if (Input.GetKeyUp("q") || (wallLeft))
        {
            anim.SetInteger("Lean", 0);
            freezeCam.canLook = true;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, crouchCam, 0), leanSpeed);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveCoroutine = null;

        }

        if (!crouching.crouchBlocked && moveCoroutine == null)
        { 
            moveCoroutine = StartCoroutine(MoveCamera(new Vector3(0, camHeight, 0), 1.25f));
        }

        wallLeft = IsWallLeft();
        wallRight = IsWallRight();
    }

    private bool IsWallLeft()
    {
        Debug.DrawRay(transform.position, -transform.right * 0.75f, Color.green);
        return Physics.Raycast(transform.position, -transform.right, 0.75f);
    }

    private bool IsWallRight()
    {
        Debug.DrawRay(transform.position, transform.right * 0.75f, Color.blue);
        return Physics.Raycast(transform.position, transform.right, 0.75f);
    }

    private IEnumerator MoveCamera(Vector3 newPosition, float speed)
    {
        Debug.Log("Running");
        float t = 0;
        while (t <= 0.5f)
        {
            t += Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, camHeight, 0), t);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }
}
