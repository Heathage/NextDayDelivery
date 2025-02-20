using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float healthRegen;
    [SerializeField]
    private float regenTime;
    private float timeTillNextHeal;
    public float currentHealth;
    [SerializeField]
    private GameObject canvas;
    private RawImage image;
    [SerializeField]
    private PostProcessVolume postPro;
    private ChromaticAberration healthEffect;

    private void Start()
    {
        image = canvas.GetComponent<RawImage>();
        postPro.profile.TryGetSettings(out healthEffect);
        //healthEffect.intensity.value = 1;
        currentHealth = health;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckHealth();
        if ( timeTillNextHeal >= regenTime)
        {
            if(currentHealth < 100)
            {
                currentHealth += healthRegen;
                
            }
            else if(currentHealth >= health)
            {
                currentHealth = health;
            }
            timeTillNextHeal = 0f;
        }
        else
        {
            timeTillNextHeal += Time.deltaTime;
        }
    }
    private void CheckHealth()
    {
        if(currentHealth == 0)
        {
            Destroy(this.gameObject);
        }
        else if(currentHealth >= 0 & currentHealth <= 25)
        {
            healthEffect.intensity.value = 1;
            //image.color = new Color(image.color.r, image.color.g, image.color.b, .7f);
            //image.transform.localScale = Vector3.Lerp(image.transform.localScale, new Vector3(1f, 1f, 0f), 20 * Time.deltaTime);
        }
        else if (currentHealth > 25 & currentHealth <= 50)
        {
            healthEffect.intensity.value = .7f;
            //image.color = new Color(image.color.r, image.color.g, image.color.b, .5f);
            //image.transform.localScale = Vector3.Lerp(image.transform.localScale, new Vector3(1.5f, 1.5f, 0f), 20 * Time.deltaTime);
        }
        else if (currentHealth > 50 & currentHealth <= 75)
        {
            healthEffect.intensity.value = .4f;

            //image.color = new Color(image.color.r, image.color.g, image.color.b, .2f);
            //image.transform.localScale = Vector3.Lerp(image.transform.localScale, new Vector3(2f, 2f, 0f), 20 * Time.deltaTime);
        }
        else if (currentHealth > 75 & currentHealth <= 100)
        {
            healthEffect.intensity.value = 0f;

            //image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            //image.transform.localScale = Vector3.Lerp(image.transform.localScale, new Vector3(3f, 3f, 0f), 20 * Time.deltaTime);
        }

    }
}
