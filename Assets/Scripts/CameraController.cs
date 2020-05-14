using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{SOUTH, EAST, NORTH, WEST}
public class CameraController : MonoBehaviour
{
    Camera c;
    Direction direction;
    void Awake(){
        if (c == null)
        {
            c = gameObject.GetComponent<Camera>();
        }
    }

    void Update(){
        CameraControl();
    }

    void CameraControl(){
        if(Input.GetKeyDown(KeyCode.E)){
            direction = (Direction) (((int)direction + 1) % 4); 
            Debug.Log("Camera direction" + direction);
            CameraRotate(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            direction = (Direction) (((int)direction - 1) % 4); 
            CameraRotate(false);
        }
        

    }

    void CameraRotate(bool dir){
        bool xBool = transform.position.x > 0;
        bool zBool = transform.position.z > 0;

        if (dir)
        {
            if (xBool == zBool)
            {
                transform.position = Vector3.Scale(transform.position,new Vector3(-1,1,1));
            } else if(xBool != zBool){
                transform.position = Vector3.Scale(transform.position,new Vector3(1,1,-1));
            }
            transform.eulerAngles += new Vector3(0,-90,0);
        } else if(!dir){
            if (xBool == zBool)
            {
                transform.position = Vector3.Scale(transform.position,new Vector3(1,1,-1));
            } else if(xBool != zBool){
                transform.position = Vector3.Scale(transform.position,new Vector3(-1,1,1));
            }
            transform.eulerAngles += new Vector3(0,90,0);
        }
    }
}
