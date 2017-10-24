using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour {

    private bool _mouseOver = false;
    private bool _mouseClick = false;

    public string stringToEdit = "Woah!";
    public Rect infoWindowRect;
    public Rect speechBubbleRect;

    private void Start()
    {
       Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //The x and y values for the boxes need to be adjusted appropriately!!

       infoWindowRect = new Rect(screenPos.x + 20, screenPos.y, 200, 200);
       speechBubbleRect = new Rect(screenPos.x - 80, screenPos.y - 50, 100, 100);
    }

    private void OnGUI()
    {
        GUI.skin.textField.wordWrap = true;

        if (_mouseOver)
            infoWindowRect = GUI.Window(0, infoWindowRect, InfoWindow, this.name);

        if (_mouseClick)
            GUI.TextField(speechBubbleRect, "Hello World! I belive this is my first thought...");
    }

    private void OnMouseDown()
    {
        _mouseClick = true;

        CancelInvoke("_UnsetMouseClick"); //To prevent multiple simultaneous clicks

        Invoke("_UnsetMouseClick", 3);
    }

    private void OnMouseEnter()
    {
        Invoke("_SetMouseOver", 1.25f);
    }

    private void OnMouseExit()
    {
        _mouseOver = false;
        CancelInvoke("_SetMouseOver");
    }

    void InfoWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
            print("Got a click");

        //THIS HAD TO BE UPDATED WITH HEALTH, HAPPINESS, DISEASE METER
    }

    void _SetMouseOver()
    {
        _mouseOver = true;
    }

    void _UnsetMouseClick()
    {
        _mouseClick = false;
    }
}
