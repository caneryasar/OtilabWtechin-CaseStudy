using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {
    
    private EventArchive _eventArchive;
    
    
    internal int passedBlocks = 0;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("BlockShuffleTrigger")) {
            
            // Debug.Log($"BlockShuffle trigger {other.name} hit");
            
            passedBlocks++;
            
            _eventArchive.InvokeOnCityBlockPassed(passedBlocks);
        }

        if(other.CompareTag("Car")) {
            
            //todo: send event hit car with parameter car
            //get return value of the car point

            // _eventArchive.InvokeOnHitCar(other.transform);
            _eventArchive.InvokeOnCarHitCheck(other.transform);
        }
    }
}