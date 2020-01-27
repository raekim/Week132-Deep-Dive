using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    // Singleton
    static MainGame Instance;
    public static MainGame GetInstance() { return Instance; }
    public bool gameWin = false;
    public bool gameOver = false;

    float diveDist;   // distance from start point to finish(treasure) point

    [SerializeField] GameObject treasure;
    [SerializeField] GameObject waterBg;
    [SerializeField] float hpDecreasePerSec;
    [SerializeField] GameObject winObj;
    [SerializeField] GameObject loseObj;
    [SerializeField] Sprite winChestSprite;

    // public variables
    public GameObject Player;
    GameObject seaGround;
    public Transform startPoint;
    public Transform finishPoint;
    public float diveProgress;  // from the surface to the treasue
    public float treasureProgress;
    public bool isUnderWater = false;
    public bool gotTreasure = false;
    public float hp = 100;

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        startPoint = transform.Find("startPoint");
        finishPoint = transform.Find("finishPoint");
        seaGround = GameObject.Find("sea-ground");
    }

    // Start is called before the first frame update
    void Start()
    {
        diveDist = Mathf.Abs(startPoint.position.y - finishPoint.position.y);
        seaGround.transform.position = finishPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            // reload current scene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (gameOver)
        {
            if (gameWin)
            {
                winObj.SetActive(true);
                if(!isUnderWater)
                {
                    // win trigger ON
                    treasure.GetComponent<SpriteRenderer>().sprite = winChestSprite;
                }
            }
            else
            {
                Time.timeScale = 0;
                loseObj.SetActive(true);
            }
        }

        UpdateStatus();
        UpdateCameraAndBackground();
        UpdateHP();
    }

    private void UpdateStatus()
    {
        // Update dive progress
        diveProgress = Mathf.Abs(startPoint.position.y - Player.transform.position.y) / diveDist;
        // is the player underwater?
        isUnderWater = Player.transform.position.y < -6;
        if(!isUnderWater && gotTreasure)
        {
            gameOver = true;
            gameWin = true;
        }
    }

    private void UpdateHP()
    {
        // Update hp
        if (isUnderWater)
        {
            hp -= hpDecreasePerSec * Time.deltaTime;
            if (hp <= 0)
            {
                hp = 0;
                gameOver = true;
                gameWin = false;
            }
        }
        else
        {
            hp = 100;
        }
    }

    private void UpdateCameraAndBackground()
    {
        // camera move
        if (isUnderWater)
        {
            // bg : out of water
            Vector3 camNewPos = Camera.main.transform.position;
            camNewPos.y = Player.transform.position.y;
            camNewPos.y = Mathf.Clamp(camNewPos.y, finishPoint.transform.position.y + 2.5f, -10);
            Camera.main.transform.position = camNewPos;

            waterBg.transform.position = new Vector3(waterBg.transform.position.x,
                Camera.main.transform.position.y, waterBg.transform.position.z);
        }
        else
        {
            // bg : underwater
            Vector3 camNewPos = Camera.main.transform.position;
            camNewPos.y = 0;
            Camera.main.transform.position = camNewPos;
            waterBg.transform.position = new Vector3(waterBg.transform.position.x,
                -10, waterBg.transform.position.z);
        }
    }

    public void IncreaseHP(int amount)
    {
        hp += amount;
        if (hp > 100) hp = 100;
    }

    public void DecreaseHP(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            // Game Over
            gameOver = true;
            gameWin = false;
        }
    }
}
