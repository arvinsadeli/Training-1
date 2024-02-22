using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] float checkDelay;
    [SerializeField] LayerMask playerLayer;
    Vector3[] directions = new Vector3[4];
    Vector3 destination;
    float checkTimer;
    bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    void CheckForPlayer()
    {
        CalculateDirection();

        for(int i = 0; i< directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            
            if(hit.collider != null && !isAttacking)
            {
                isAttacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    void CalculateDirection()
    {
        directions[0] = transform.right * range; //right direction
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    void Stop()
    {
        destination = transform.position;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
