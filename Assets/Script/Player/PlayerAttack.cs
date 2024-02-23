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
    Health health;
    // Start is called before the first frame update

    [Header("Sound")]
    [SerializeField] AudioClip fireballSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack() && !health.dead)
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
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
