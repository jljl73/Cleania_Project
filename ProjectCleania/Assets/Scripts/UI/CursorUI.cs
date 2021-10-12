using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    enum MouseCursorTarget { Default, Enemy, Entrance, Loot, Merchant, Storage, Talk, BlackSmith };

    public List<Texture2D> cursorTexture;
    private Texture2D currentCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Update()
    {
        string hitTag = GetMouseCollide();

        switch (hitTag)
        {
            case "Enemy":
                currentCursorTexture = cursorTexture[1];
                break;
            case "NPC":
                currentCursorTexture = cursorTexture[6];
                break;
            default:
                currentCursorTexture = cursorTexture[0];
                break;
        }

        Cursor.SetCursor(currentCursorTexture, hotSpot, cursorMode);
    }

    string GetMouseCollide()
    {
        string result = "";

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            result = raycastHit.collider.tag;
        }

        return result;
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
