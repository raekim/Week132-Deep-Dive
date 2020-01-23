using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    bool gotEaten = false;
    [SerializeField] GameObject playerIcon;
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gotEaten)
        {
            // gets "absorbed" by the player icon
            Vector3 dist = playerIcon.transform.position - transform.position;
            if (dist.magnitude < 0.4f)
            {
                MainGame.GetInstance().IncreaseHP(20);
                Destroy(this.gameObject);
            }
            else
            {
                dist.Normalize();
                dist *= speed * Time.deltaTime;
                transform.position += dist;
                speed += 20f * Time.deltaTime;
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
