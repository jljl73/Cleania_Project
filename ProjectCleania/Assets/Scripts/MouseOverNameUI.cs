using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
    ;

public class MouseOverNameUI : ObjectUI
{
    public bool IsWorldCoordinate = false;              // UI ǥ�� ����
    public Vector2 UIShowPosition = Vector2.zero;       // UI ǥ�� ��ġ

    public string DisplayName;                      // �̸�
    public string StateName;                        // ���� �̸�

    private AbilityStatus objAbilityStatus;

    private void Awake()
    {
        objAbilityStatus = GetComponent<AbilityStatus>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    if (objAbilityStatus != null)
        //    {
        //        objAbilityStatus.AttackedBy(objAbilityStatus, 1.0f);
        //        print("attacked!");
        //    }
        //}

        if (IsInUIBorder(Camera.main.WorldToScreenPoint(transform.position)))
            ShowUI();
        else
            base.DestroyUI();
    }

    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.gameObject == this.transform.gameObject)
            {
                float mag = Vector3.Distance(ray.origin, this.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ray.origin, ray.direction * mag);
            }
        }
    }

    void ShowUI()
    {
        base.JudgeCreateDestroy();

        switch (transform.tag)
        {
            case "Item":
                break;

            case "Enemy":
                if (IsMouseCollide())
                {
                    UpdateHPState();
                    base.ActiveUI(true);
                }
                else
                    base.ActiveUI(false);
                break;

            default:
                if (IsMouseCollide())
                {
                    base.ActiveUI(true);
                    if (IsWorldCoordinate)
                        UpdateUIPosition();
                }
                else
                    base.ActiveUI(false);
                break;
        }
    }
    void UpdateHPState()
    {
        if (GetImageComponent("HP") != null)
            GetImageComponent("HP").fillAmount = objAbilityStatus.HP / objAbilityStatus[Ability.Stat.MaxHP];
    }
    
    bool IsMouseCollide()
    {
        bool isCollide = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.gameObject == this.transform.gameObject)
                isCollide = true;
        }

        return isCollide;
    }

    void UpdateUIPosition()
    {
        if (!uiObjectInstExist) return;

        Vector3 dy = Vector3.up * UIShowPosition.y;
        Vector3 dx = Vector3.right * UIShowPosition.x;
        uiObjectInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + dy - dx);
    }

    protected override void InstantiateUIObject()
    {
        base.InstantiateUIObject();

        if (GetTextComponent("Name") != null)
            GetTextComponent("Name").text = DisplayName;

        if (GetTextComponent("State") != null)
            GetTextComponent("State").text = StateName;
    }

    private void OnDestroy()
    {
        base.DestroyUI(); ;
    }
}
