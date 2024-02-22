using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;
    [SerializeField] int damage;

    float cooldownTimer = Mathf.Infinity;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;
    // Start is called before the first frame update

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        //attack in sight
        if (IsPlayerInSight())
        { 
            if(cooldownTimer >= attackCooldown) //if not cooldown
            {
                //attack

            }
        }
    }

    bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance, //x
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), //y
        0f, //z
        Vector2.left, 0f, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * Mathf.Sign(transform.localScale.x) * colliderDistance, //x
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)); // y
    }
}
