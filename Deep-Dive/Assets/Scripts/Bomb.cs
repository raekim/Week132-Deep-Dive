using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoveMode
{
    HORIZONTAL, CIRCULAR
}

public class Bomb : MonoBehaviour
{
    [SerializeField] bool left;
    [SerializeField] MoveMode moveMode;
    [SerializeField] float speed;
    [SerializeField] float radius;

    float newX, newY;
    float deg = 0;
    Vector3 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        centerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (moveMode)
        {
            case MoveMode.HORIZONTAL:
                deg += speed * Time.deltaTime * ((!left) ? -1 : 1);
                deg %= 360;
                newX = radius * Mathf.Cos(Mathf.Deg2Rad * deg);
                transform.position = centerPos + new Vector3(newX, 0, 0);
                break;
            case MoveMode.CIRCULAR:
                deg += speed * Time.deltaTime * ((!left) ? -1 : 1);
                deg %= 360;
                newX = radius * Mathf.Cos(Mathf.Deg2Rad * deg);
                newY = radius * Mathf.Sin(Mathf.Deg2Rad*deg);
                transform.position = centerPos + new Vector3(newX, newY, 0);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().Hit();
        }
    }
}
