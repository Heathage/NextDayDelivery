
using UnityEngine;

public class EmitSound : MonoBehaviour
{
    private Rigidbody rb;
    [Range (0, 50)]
    [SerializeField]
    private float sphereRadius;
    public LayerMask layerMask;
    [SerializeField]
    private AudioClip droopSound;
    private AudioSource audioSource;

    public Vector3 origin;
    private Vector3 velocity;

    private PatrolAI patrolEnemy;
    private StationaryAI stationaryEnemy;

    private FOVDetection fOVDetection;
    private ObjPickUp objPickUp;

    [SerializeField]
    private GameObject audioProjection;
    [SerializeField]
    private GameObject projector;

    private Collider[] hits;

    private bool timerRunning = false;
    [SerializeField]
    private float timeStart;
    private float timeRemaining;
    [SerializeField]
    private LayerMask groundLayers; 
    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = droopSound;
        rb = this.GetComponent<Rigidbody>();
        objPickUp = this.gameObject.GetComponent<ObjPickUp>();


    }

    private void Update()
    {
        velocity = rb.velocity;
        if (timerRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timerRunning = false;
                Destroy(projector);
            }
        }
        GroundCheck();

    }

    private void GroundCheck()
    {
        if (objPickUp.wasThrown)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, .5f))
            {
                audioSource.PlayOneShot(droopSound);
                if (velocity == Vector3.zero)
                {
                    GetEnemiesInRange();
                    objPickUp.wasThrown = false;
                }
            else
            {
                Debug.Log("I HATE MY SELF");
            }
            }
       
        }
    }

    /**private void SpawnProjectors()
    {
        this.transform.rotation = new Quaternion(this.transform.rotation.x, 3, this.transform.rotation.z, 0);
        
        if(projector == null)
        {
            Debug.Log("SpawnProjector");
            projector = Instantiate(audioProjection, this.transform.position, Quaternion.Euler(90f, 0f, 0f));
            projector.GetComponent<Projector>().orthographicSize = sphereRadius;      
        }
        else
        {
            Debug.Log("!");
        }
        timeRemaining = timeStart;
        timerRunning = true;
    }
    **/

    private void GetEnemiesInRange()
    {
        //SpawnProjectors();
        Collider[] hits = Physics.OverlapSphere(origin, sphereRadius, layerMask, QueryTriggerInteraction.UseGlobal);
        
        int i = 0;
        while (i < hits.Length)
        {
            if (hits[i].gameObject.GetComponent<PatrolAI>() != null)
            {

                patrolEnemy = hits[i].gameObject.GetComponent<PatrolAI>();
                fOVDetection = hits[i].gameObject.GetComponent<FOVDetection>();
                if (patrolEnemy.state != PatrolAI.State.ChaseTarget)
                {
                    patrolEnemy.state = PatrolAI.State.Sound;
                    origin = this.transform.position;
                    fOVDetection.playerLastKnownPos = origin;
                    fOVDetection.playerLastKnownPos.y += 1;
                    Debug.Log("Done");
                }

            }
            if (hits[i].gameObject.GetComponent<StationaryAI>() != null)
            {

                stationaryEnemy = hits[i].gameObject.GetComponent<StationaryAI>();
                fOVDetection = hits[i].gameObject.GetComponent<FOVDetection>();
                if (stationaryEnemy.state != StationaryAI.State.ChaseTarget)
                {
                    stationaryEnemy.state = StationaryAI.State.Sound;
                    origin = this.transform.position;
                    fOVDetection.playerLastKnownPos = origin;
                    fOVDetection.playerLastKnownPos.y += 1;
                    Debug.Log("Done");

                }

            }
            i++;

        }
    }
}
