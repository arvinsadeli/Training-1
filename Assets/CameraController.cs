using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float cameraSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime * cameraSpeed), transform.position.y, transform.position.z);
    }
}
