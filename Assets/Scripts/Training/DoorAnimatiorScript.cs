using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimatiorScript : MonoBehaviour
{
    Animator animator;
    bool doorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "TrainingTank") {
            this.doorOpen = true;
            DoorControl("Open");
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (this.doorOpen) {
            this.doorOpen = false;
            DoorControl("Close");
        }
    }

    private void DoorControl(string state) {
        this.animator.SetTrigger(state);
    }
}
