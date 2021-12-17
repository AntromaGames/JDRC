using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TableController : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler, IDragHandler,IPointerClickHandler
{

    public Table table;

    public bool  isSimple;

    private Canvas canvas;
    private RectTransform rectTransform;

    [SerializeField] Color selectColor, normalColor;
    [SerializeField] Image image;
    [SerializeField] GameObject mooveSymbols, rotateSymbols;

    Transform classeTrans;
    public enum MooveMode {Idle, Moove,Rotate}
    public MooveMode mooveMode;

    private void Awake()
    {
        classeTrans = GameObject.Find("Classroom").transform;
        mooveMode = MooveMode.Idle;
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color= selectColor;
        transform.SetParent(classeTrans);
    }

    private void SwitchMode()
    {
        if (mooveMode == MooveMode.Idle)
        {
            mooveMode = MooveMode.Moove;
            mooveSymbols.SetActive(true);
        }
        else if (mooveMode == MooveMode.Moove)
        {
            mooveSymbols.SetActive(false);
            rotateSymbols.SetActive(true);
            mooveMode = MooveMode.Rotate;
        }
        else
        {
            rotateSymbols.SetActive(false);
            mooveMode = MooveMode.Idle;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(mooveMode == MooveMode.Moove)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        }else if (mooveMode == MooveMode.Rotate)
        {
            if(eventData.delta.y > 0)
            {
                rectTransform.Rotate(new Vector3(0, 0, 1), new Vector2(eventData.delta.x, eventData.delta.y).magnitude);
            }
            else
            {
                rectTransform.Rotate(new Vector3(0, 0, 1), -new Vector2(eventData.delta.x, eventData.delta.y).magnitude);
            }


        }
        else
        {
            return;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = normalColor;
        gameObject.tag = "Player";
        if (isSimple)
        {
            ClassroomMaker.instance.CreateNewSimpleTable();
        }
        else
        {
            ClassroomMaker.instance.CreateNewDoubleTable();
        }
    }

    public void SaveTable()
    {
        if (gameObject.tag == "Player")
        {
            float angle = 0;

            Vector3 axis = new Vector3(0, 0, 0);

            transform.localRotation.ToAngleAxis(out angle, out axis);
            if (transform.rotation.z < 0)
            {
                angle = -angle;
            }

            table = new Table(1, rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, angle, isSimple);
            GameManager.instance.AddTableToPlan(table);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SwitchMode();
    }
}
