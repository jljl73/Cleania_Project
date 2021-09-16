using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUI : MonoBehaviour
{
    enum MouseCursorTarget { Default, Enemy, Entrance, Loot, Merchant, Storage, Talk, BlackSmith };

    public List<Texture2D> cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    

    private void Start()
    {
        Cursor.SetCursor(cursorTexture[0], hotSpot, cursorMode);
    }

    private void Update()
    {
        
    }

    bool IsMouseCollide()
    {
        bool isCollide = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.CompareTag("Enemy"))
            {
                Cursor.SetCursor(cursorTexture[0], hotSpot, cursorMode);
            }
        }

        return isCollide;
    }

    //void OnMouseEnter()
    //{
    //    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);
    //}
}
