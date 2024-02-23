using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] Transform enemy;

    [Header("Movement Parameter")]
    [SerializeField] float speed;
    Vector3 initScale;
    bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] float idleDuration;
    float idleTimer;

    [Header("Enemy Anim")]
    Animator anim;

    private void Awake()
    {
        anim = enemy.GetComponent<Animator>();
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("isRunning", false);
    }

    void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("isRunning", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
       
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                ChangeDirection();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        anim.SetBool("isRunning", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }
}
