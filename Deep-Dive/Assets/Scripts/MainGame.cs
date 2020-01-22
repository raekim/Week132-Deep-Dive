using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Singleton
    static MainGame Instance;
    public static MainGame GetInstance() { return Instance; }

    float diveDist;   // distance from start point to finish(treasure) point

    [SerializeField] GameObject seaGround;
    [SerializeField] GameObject waterBg;

    // public variables
    public GameObject Player;
    public Transform startPoint;
    public Transform finishPoint;
    public float diveProgress;  // from the surface to the treasue
    public bool isUnderWater = false;

    private void Awake()
    {
        if(!Instance)
            Instance = this;
        startPoint = transform.Find("startPoint");
        finishPoint = transform.Find("finishPoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        diveDist = Mathf.Abs(startPoint.position.y - finishPoint.position.y);
        diveProgress = Mathf.Abs(startPoint.position.y - Player.transform.position.y) / diveDist;
        seaGround.transform.position = finishPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update dive progress
        diveProgress = Mathf.Abs(startPoint.position.y - Player.transform.position.y) / diveDist;
        isUnderWater = diveProgress > 0;

        // camera move
        if (Player.transform.position.y < -6)
        {
            // camera views underwater
            Vector3 camNewPos = Camera.main.transform.position;
            camNewPos.y = Player.transform.position.y;
            camNewPos.y = Mathf.Clamp(camNewPos.y, finishPoint.transform.position.y + 2.5f, -10);
            Camera.main.transform.position = camNewPos;

            waterBg.transform.position = new Vector3(waterBg.transform.position.x,
                Camera.main.transform.position.y, waterBg.transform.position.z);
        }
        else
        {
            // camera views surface
            Vector3 camNewPos = Camera.main.transform.position;
            camNewPos.y = 0;
            Camera.main.transform.position = camNewPos;

            waterBg.transform.position = new Vector3(waterBg.transform.position.x,
                -10, waterBg.transform.position.z);
        }
    }
}
