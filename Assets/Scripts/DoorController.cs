using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
    }

    private void OnDoorwayOpen(int id){
        if (id == this.id)
        {
            LeanTween.moveLocalY(gameObject, 3f,1f).setEaseOutQuad();
        }
        
    }
    private void OnDoorwayClose(int id){
        if (id == this.id)
        {
            LeanTween.moveLocalY(gameObject,1f,1f).setEaseInQuad();
        }   
    }

    private void OnDestoy(){
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit -= OnDoorwayClose;
    }
}
