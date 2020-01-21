using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float goDownSpeed = 1;
    [SerializeField] float goUpSpeed = 0.5f;
    [SerializeField] float goSideSpeed = 0.5f;
    [SerializeField] float playerMinX;
    [SerializeField] float playerManX;

    Vector3 startPoint;
    Vector3 finishPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = MainGame.GetInstance().startPoint.position;
        finishPoint = MainGame.GetInstance().finishPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Swim();
    }

    private void Swim()
    {
        Vector3 newPos = transform.position;

        // Swim down
        float speed = (Input.GetKey(KeyCode.DownArrow)) ? -goDownSpeed : goUpSpeed;
        newPos += new Vector3(0, speed, 0) * Time.deltaTime;

        // Swim sideways
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            speed = -goSideSpeed;
            newPos += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            speed = goSideSpeed;
            newPos += new Vector3(speed, 0, 0) * Time.deltaTime;
        }

        // Clamp player position
        newPos.x = Mathf.Clamp(newPos.x, playerMinX, playerManX);
        newPos.y = Mathf.Clamp(newPos.y, finishPoint.y, startPoint.y);

        transform.position = newPos;
    }
}
