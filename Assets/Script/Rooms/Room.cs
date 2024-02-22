using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    Vector3[] initialPosition;
    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = new Vector3[enemies.Length];
        for(int i = 0; i<enemies.Length; i++)
        {
            if(enemies[i] != null)
                initialPosition[i] = enemies[i].transform.position;

        }
    }

    public void ActivateRoom(bool _status)
    {
        //activate/deactivate enemies

        for (int i = 0; i<enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
