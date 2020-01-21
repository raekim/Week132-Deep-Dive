using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Singleton
    static MainGame Instance;
    public static MainGame GetInstance() { return Instance; }

    float diveDist;   // distance from start point to finish(treasure) point

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
    }

    // Start is called before the first frame update
    void Start()
    {
        diveDist = Mathf.Abs(startPoint.position.y - finishPoint.position.y);
        diveProgress = Mathf.Abs(startPoint.position.y - Player.transform.position.y) / diveDist;
    }

    // Update is called once per frame
    void Update()
    {
        // Main Camera y pos follows player
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
            Player.transform.position.y, Camera.main.transform.position.z);

        // Update dive progress
        diveProgress = Mathf.Abs(startPoint.position.y - Player.transform.position.y) / diveDist;

        isUnderWater = diveProgress > 0;
    }
}
