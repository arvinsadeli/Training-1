using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]

    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] int damage;

    [Header("Collider Parameters")]
    [SerializeField] float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;


    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;
    float cooldownTimer = Mathf.Infinity;
    // Start is called before the first frame update

    [Header("Attack Sound")]
    [SerializeField] AudioClip attackSound;

    Patrol patrol;

    //reference
    Animator anim;
    Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        patrol = GetComponentInParent<Patrol>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsPlayerInSight());
        cooldownTimer += Time.deltaTime;

        //attack in sight
        if (IsPlayerInSight())
        { 
            if(cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0) //if not cooldown
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttackTrigger");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if (patrol != null)
            patrol.enabled = !IsPlayerInSight();
    }

    bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance, //x
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), //y
        0f, //z
        Vector2.left, 0f, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance, //x
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); // y
    }

    void DamagePlayer()
    {
        //if player still in range damage him
        if (IsPlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
