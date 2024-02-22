using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    float cooldownTimer = Mathf.Infinity;
    Animator anim;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    void Attack()
    {
        anim.SetTrigger("attackTrigger");
        cooldownTimer = 0;
        //pool fireball
        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    int FindFireBall()
    {
        for (int i = 0; i<fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
