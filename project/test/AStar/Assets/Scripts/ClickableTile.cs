using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{   private bool mouseOver;
    public int x;
    public int y;
    public HexTilesMap map;
    void OnMouseUp()
    {
       map.SetStartPoint(this.transform.gameObject,x,y);
       map.GeneratePath();
    }
   
    private void OnRightMouseDown()
    {
        map.SetEndPoint(this.transform.gameObject, x, y);
        map.GeneratePath();
    }
    void Update()
    {
        if (mouseOver && Input.GetMouseButtonDown(1))
        {
            OnRightMouseDown();
        }
    }
 
    private void OnMouseEnter()
    {
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
    
}
