using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupables : MonoBehaviour
{
    [SerializeField] int healthAmount;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().ReceiveHealth(healthAmount);
            gameObject.SetActive(false);
        }
    }
}
