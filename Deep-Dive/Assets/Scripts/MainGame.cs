using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] GameObject MainCam;
    [SerializeField] GameObject Player;
    Player PlayerScript;
    [SerializeField] GameObject SliderUI;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform finishPoint;

    float totalDepth;   // distance from start point to finish(treasure) point

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = Player.GetComponent<Player>();
        PlayerScript.startPoint = startPoint;
        PlayerScript.finishPoint = finishPoint;
    }

    // Update is called once per frame
    void Update()
    {
        // Main Camera follows player
        MainCam.transform.position = Player.transform.position + new Vector3(0, 0, -1);
    }
}
