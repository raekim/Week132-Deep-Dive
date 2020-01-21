using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float goDownSpeed = 1;
    [SerializeField] float goUpSpeed = 0.5f;

    public Transform startPoint;
    public Transform finishPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > finishPoint.position.y)
                transform.Translate(new Vector3(0, -goDownSpeed, 0) * Time.deltaTime);
        }
        else
        {
            if(transform.position.y < startPoint.position.y)
                transform.Translate(new Vector3(0, goUpSpeed, 0) * Time.deltaTime);
        }
    }
}
