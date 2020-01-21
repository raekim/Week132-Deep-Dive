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
        float newY = Mathf.Lerp(UiStartPos.position.y, UiEndPos.position.y,
            MainGame.GetInstance().diveProgress);
        playerIcon.transform.position = new Vector3(playerIcon.transform.position.x, newY, 0);

        bubbleIconSprite.enabled = MainGame.GetInstance().isUnderWater;
    }
}
