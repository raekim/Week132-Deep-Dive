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
    [SerializeField] Sprite[] sprites;

    Animator myAnimator;
    Vector3 startPoint;
    Vector3 finishPoint;
    SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = MainGame.GetInstance().startPoint.position;
        finishPoint = MainGame.GetInstance().finishPoint.position;
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGame.GetInstance().gameOver)
        {
            // Swim up
            Vector3 newPos = transform.position;
            float speed = goUpSpeed;
            newPos += new Vector3(0, speed, 0) * Time.deltaTime;
            // Clamp player position
            newPos.x = Mathf.Clamp(newPos.x, playerMinX, playerManX);
            newPos.y = Mathf.Clamp(newPos.y, finishPoint.y, startPoint.y);
            transform.position = newPos;
            return;
        }
        Swim();

        if (Input.GetKey(KeyCode.DownArrow))
        {
            mySpriteRenderer.sprite = sprites[1];
        }
        else
        {
            mySpriteRenderer.sprite = sprites[0];
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.name == "Treasure")
        {
            // Got treasure!
            MainGame.GetInstance().gotTreasure = true;
            obj.transform.parent = transform;
            obj.transform.position = transform.position + new Vector3(0, 2, 0);
        }
    }

    public void Hit()
    {
        MainGame.GetInstance().DecreaseHP(20);
        myAnimator.SetTrigger("Hit");
        // super mode
    }
}
