using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{

    [SerializeField] float attackCooldown;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject[] arrows;
    float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Attack()
    {
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    int FindArrow()
    {
        for(int i = 0; i< arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
