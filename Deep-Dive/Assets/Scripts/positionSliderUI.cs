using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionSliderUI : MonoBehaviour
{
    [SerializeField] GameObject playerIcon;
    [SerializeField] GameObject treasureIcon;
    [SerializeField] GameObject bubbleIcon;
    [SerializeField] Transform UiStartPos;
    [SerializeField] Transform UiEndPos;

    SpriteRenderer bubbleIconSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerIcon.transform.position = UiStartPos.position;
        bubbleIconSprite = bubbleIcon.GetComponent<SpriteRenderer>();
        bubbleIconSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // update player icon
        float newY = Mathf.Lerp(UiStartPos.position.y, UiEndPos.position.y,
            MainGame.GetInstance().diveProgress);
        playerIcon.transform.position = new Vector3(playerIcon.transform.position.x, newY, 0);

        // update treasure icon
        if (MainGame.GetInstance().gotTreasure)
        {
            treasureIcon.transform.position = new Vector3(playerIcon.transform.position.x + 0.7f, 
                newY, 0);

        }

        // update bubble icon
        bubbleIconSprite.enabled = MainGame.GetInstance().isUnderWater;
        float bubbleScale = MainGame.GetInstance().hp / 100.0f;
        bubbleIcon.transform.localScale = new Vector3(bubbleScale + 0.7f, bubbleScale + 0.7f, 1);
    }
}
