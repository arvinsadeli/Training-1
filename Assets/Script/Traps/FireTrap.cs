using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] float damage;
    [Header("Firetrap Timers")]
    [SerializeField] float activationDelay;
    [SerializeField] float activeTime;

    Animator anim;
    SpriteRenderer spriteRenderer;
    bool triggered;
    bool active;

    [Header("Sound")]
    [SerializeField] AudioClip fireTrapSound;


    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                //trigger trap
                StartCoroutine(ActivateFireTrap());
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        //trurn the sprite to red to notify player
        triggered = true;
        spriteRenderer.color = Color.red;

        //wait for delay, activate trap, turn on animation
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireTrapSound);
        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("isActivate", true);

        //deactivate trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("isActivate", false);
    }
}
