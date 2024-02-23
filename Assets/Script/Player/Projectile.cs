using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    float direction;
    bool isHitting;
    Animator anim;
    BoxCollider2D boxCollider;
    float lifetime;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitting) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHitting = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explodeTrigger");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float tempDirection)
    {
        lifetime = 0;
        direction = tempDirection;
        gameObject.SetActive(true);
        isHitting = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != tempDirection) //flip as the direction float value;
        {
            localScaleX = -localScaleX;
        }
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        Debug.Log(transform.localScale.x);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
