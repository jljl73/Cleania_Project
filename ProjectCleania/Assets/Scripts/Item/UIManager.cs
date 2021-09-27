using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    bool isActiveItemPanel = true;
    public GameObject itemPanel;
    public GameObject ItemPanel { get { return itemPanel; } }

    public ItemInventory itemInventory_;
    public ItemInventory GetItemInventory { get { return itemInventory_; } }

    public Canvas canvas_;
    public Canvas GetCanvas { get { return canvas_; } }

    
    private void Start()
    {
        OnOffItemPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffItemPanel();
        }
    }

    void OnOffItemPanel()
    {
        if (isActiveItemPanel)
            itemPanel.transform.Translate(new Vector3(2000, 0, 0));
        else
            itemPanel.transform.Translate(new Vector3(-2000, 0, 0));

        isActiveItemPanel = !isActiveItemPanel;
    }

}
