using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("health")]
    [SerializeField] float startingHealth;
    public float currentHealth { get; private set; }
    Animator anim;
    bool dead;
    // Start is called before the first frame update

    [Header("iFrames")]
    [SerializeField] float IFramesDuration;
    [SerializeField] int numberOffFlashes;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            //receive damage
            anim.SetTrigger("hurtTrigger");
            StartCoroutine(Invunerability());
        }
        else
        {
            //dead
            if(!dead)
            {
                GetComponent<PlayerMovement>().enabled = false;
                anim.SetTrigger("dieTrigger");
                dead = true;
            }
        }
    }

    public void ReceiveHealth(int healthAmount)
    {
        currentHealth += Mathf.Clamp(currentHealth + healthAmount, 0, startingHealth);
    }

    IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8,9, true);
        for(int i=0; i<numberOffFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, .5f);
            yield return new WaitForSeconds(IFramesDuration / (numberOffFlashes) / 2 );
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(IFramesDuration / (numberOffFlashes) / 2);
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

}
