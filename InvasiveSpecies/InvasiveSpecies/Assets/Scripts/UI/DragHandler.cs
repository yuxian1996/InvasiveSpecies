using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject itemBeingDragged;
    public GameObject reference;

    public Dropdown dropdownList;

    public GameObject mySprite1;
    public GameObject mySprite2;
    public GameObject mySprite3;

    Image image;
    Vector3 startPosition;
    Transform startParent;

    void Awake()
    {
        
    }

    void Start()
    {
        image = GetComponent<Image>();
        if (dropdownList != null)
        {
            if (dropdownList.value == 0)
            {
                reference = mySprite1;
            }
                
            else if (dropdownList.value == 1)
            {
                reference = mySprite2;
            }
                
            else if (dropdownList.value == 2)
            {
                reference = mySprite3;
            }  
        }
    }

    void Update()
    {
        if (dropdownList != null)
        {
            if (dropdownList.value == 0)
            {
                reference = mySprite1;
                image.sprite = mySprite1.GetComponent<SpriteRenderer>().sprite;
            }
            else if (dropdownList.value == 1)
            {
                reference = mySprite2;
                image.sprite = mySprite2.GetComponent<SpriteRenderer>().sprite;
            }
            else if (dropdownList.value == 2)
            {
                reference = mySprite3;
                image.sprite = mySprite3.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = Instantiate(reference);
        itemBeingDragged.name = reference.name;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        itemBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        

        //if(itemBeingDragged != null)
        //{
        //    itemBeingDragged.transform.position = Input.mousePosition;

        //    if (GetComponent<Properties>().type == 1)
        //        GameSystem.herbivores.Add(itemBeingDragged);

        //    if (GetComponent<Properties>().type == 2)
        //        GameSystem.carnivores.Add(itemBeingDragged);

        //    if (GetComponent<Properties>().type == 3)
        //        GameSystem.plants.Add(itemBeingDragged);
        //}

        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
            transform.position = startPosition;
    }
}
