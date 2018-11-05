using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private bool pickedUp;

    private Vector3 initialPos;

    //the max distance from the object's spawn point before it's subject to destruction
    public float killDist;




    // Use this for initialization
    void Start()
    {
        initialPos = transform.position;
        InvokeRepeating("CheckSelfDestruct", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }




    //Must have a collider (non-trigger)
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        PickedUp = true;
    }

    // Called every frame when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        PickedUp = false;
    }


    public bool PickedUp
    {
        get
        {
            return pickedUp;
        }

        set
        {
            //Debug.Log(value ? "Picked Up" : "Dropped");
            pickedUp = value;
        }
    }

    private void CheckSelfDestruct()
    {
        if (Vector3.Distance(transform.position, initialPos) > killDist)
        {
            //Debug.Log("Goodbye, cruel world");
            Destroy(gameObject);
        }
    }



}
