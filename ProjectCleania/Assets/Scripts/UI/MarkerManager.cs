using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerManager : MonoBehaviour
{
    public enum MarkerType { Player, Merchant, BlackSmith, Portal, Quest, CompleteQuest, Storage };

    public MarkerType Marker = MarkerType.Player;

    public Sprite PlayerMarker;
    public Sprite MerchantMakrer;
    public Sprite BlackSmithMarker;
    public Sprite PortalMarker;
    public Sprite QuestMarker;
    public Sprite CompleteQuestMarker;
    public Sprite StorageMarker;

    private Image MarkerImage;
    private RectTransform MarkerRectTransform;

    private void Awake()
    {
        MarkerImage = GetComponent<Image>();
        MarkerRectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        switch (Marker)
        {
            case MarkerType.Player:
                MarkerImage.sprite = PlayerMarker;
                break;
            case MarkerType.Merchant:
                MarkerImage.sprite = MerchantMakrer;
                break;
            case MarkerType.BlackSmith:
                MarkerImage.sprite = BlackSmithMarker;
                break;
            case MarkerType.Portal:
                MarkerImage.sprite = PortalMarker;
                break;
            case MarkerType.Quest:
                MarkerImage.sprite = QuestMarker;
                break;
            case MarkerType.CompleteQuest:
                MarkerImage.sprite = CompleteQuestMarker;
                break;
            case MarkerType.Storage:
                MarkerImage.sprite = StorageMarker;
                break;
        }

        MarkerRectTransform.sizeDelta = new Vector2(MarkerImage.sprite.rect.width * 0.2f, MarkerImage.sprite.rect.height * 0.2f);
    }

    void Update()
    {
        
    }
}
