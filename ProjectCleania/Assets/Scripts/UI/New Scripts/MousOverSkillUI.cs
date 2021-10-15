using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MousOverSkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool existRune = false;

    public string skillName;
    public string runeName;
    [TextArea]
    public string resource;
    [TextArea]
    public string detail;
    [TextArea]
    public string runeDetail;

    public GameObject textSkillName;
    public GameObject textResource;
    public GameObject textDetails;
    public GameObject textRuneName;
    public GameObject textRuneDetail;

    public GameObject detailPanel;

    GameObject[] details;

    void Awake()
    {

        textSkillName.GetComponent<Text>().text = skillName;
        textRuneName.GetComponent<Text>().text = runeName;
        textResource.GetComponent<Text>().text = resource;
        textDetails.GetComponent<Text>().text = detail;
        textRuneDetail.GetComponent<Text>().text = runeDetail;

        if (!existRune)
        {
            textRuneName.gameObject.SetActive(false);
            textRuneDetail.transform.parent.gameObject.SetActive(false);
        }
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        detailPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailPanel.SetActive(false);
    }


}

