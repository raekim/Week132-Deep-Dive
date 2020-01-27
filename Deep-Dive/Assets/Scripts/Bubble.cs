using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    bool gotEaten = false;
    [SerializeField] GameObject playerIcon;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // gets eaten
        if (gotEaten)
        {
            // gets "absorbed" by the player icon
            // dist : 현재 프레임에서 공기방울과 플레이어 위치 UI 사이의 거리 벡터
            Vector3 dist = playerIcon.transform.position - transform.position;
            if (dist.magnitude < 0.4f)
            {
                // 공기방울이 UI에 "먹히고", 플레이어 체력을 회복
                MainGame.GetInstance().IncreaseHP(20);
                Destroy(this.gameObject);
            }
            else
            {
                // 공기방울이 UI 방향으로 날아간다.
                dist.Normalize();
                dist *= speed * Time.deltaTime;
                transform.position += dist;
                speed += 20f * Time.deltaTime; // 날아가는 속도가 일정하게 빨라진다.
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !gotEaten)
        {
            gotEaten = true;
        }
    }
}
