using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// CONTROLLER DO NOTHING WITHOUT CALLBACK & INSTANTIATING
/// </summary>
public class UI_ItemController : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler, // drag and drop
    IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler,   // item infromation show
    IPointerClickHandler    // item auto move
{
    public ItemInstance itemInstance
    { get; private set; }
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Image backgroundImage;

    Vector3 prevPosition;
    Vector2 pointerOffset;
    UI_ItemContainer currentContainer;

    // generator
    static GameObject controllerPrefab;
    static Queue<GameObject> _objectPool;


    static public UI_ItemController New(ItemInstance item, UI_ItemContainer container, int index)
    {
        UI_ItemController controller = null;

        // initialize statics
        if (_objectPool == null)
            _objectPool = new Queue<GameObject>();
        if (controllerPrefab == null)
            controllerPrefab = Resources.Load<GameObject>("Prefabs/UI_ItemController");

        // check object pool
        if (_objectPool.Count < 1)
        {
            GameObject newControllerObject = GameObject.Instantiate(controllerPrefab);
            controller = newControllerObject.GetComponent<UI_ItemController>();
        }
        else
            controller = _objectPool.Dequeue().GetComponent<UI_ItemController>();

        // refresh data of controller
        controller.ItemChange(item, container, index);
        controller.gameObject.SetActive(true);
        //controller.transform.localScale *= CanvasScaler.;

        return controller;
    }
    static public void Delete(UI_ItemController controller)
    {
        controller.ItemChange(null, null);
        _objectPool.Enqueue(controller.gameObject);

        controller.gameObject.SetActive(false);
    }

    public void ItemChange(ItemInstance item, UI_ItemContainer container, int index = -1)
    {
        itemInstance = item;
        //parentStroage = inStorage;

        if (item != null && container != null)
        {
            itemImage.sprite = itemInstance.SO.ItemImage;
            /*backgroundImage.sprite change code*/
            transform.SetParent(container.ItemContollerParent.transform);
            currentContainer = container;
        }

        if (index >= 0 && index < container.ItemContollerParent.transform.childCount)
        {
            backgroundImage.rectTransform.position = container.ItemContollerParent.transform.GetChild(index).transform.position;
            prevPosition = backgroundImage.rectTransform.position;
        }
        else if (index != -1)
            Debug.LogError("Logic error in UI_ItemController : ItemChange");
    }

    // get sibling index and put inside
    // change parent of controller (compare currentContainer with raycasted container)





    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        prevPosition = backgroundImage.transform.position;
        pointerOffset = eventData.position - (Vector2)backgroundImage.rectTransform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        backgroundImage.rectTransform.position = eventData.position - pointerOffset;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        Vector2 curSorPoint = eventData.position - pointerOffset;

        eventData.position = curSorPoint;
        GameManager.Instance.MainCanvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            switch(results[i].gameObject.tag)
            {
                case "Slot":
                    if (currentContainer.ImmigrateTo(this
                        , results[i].gameObject.GetComponentInParent<UI_ItemContainer>()
                        , results[i].gameObject.transform.GetSiblingIndex()))

                        backgroundImage.rectTransform.position = results[i].gameObject.transform.position;
                    else
                        backgroundImage.rectTransform.position = prevPosition;

                    return;

                default:
                    backgroundImage.rectTransform.position = prevPosition;
                    break;
            }
        }

        // raycast with offset and choose action
        // if tag is slot, call Add and Remove of each containers.
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // enable detail ui
        // refresh detail ui
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        // disable detail ui
    }

    void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
    {
        // ?
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // dosmt
    }
}
