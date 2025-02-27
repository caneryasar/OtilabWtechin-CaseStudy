using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockPoolHandler : MonoBehaviour {

    private EventArchive _eventArchive;
    
    public List<GameObject> cityBlocks;

    public float pushForwardAmount = 240f;

    private int _currentIndex;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();

        _eventArchive.OnCityBlockPassed += count => _currentIndex = count - 1;

        for(var i = 0; i < transform.childCount; i++) {
            
            var child = transform.GetChild(i);

            if(child.CompareTag("CityBlock")) {
                
                cityBlocks.Add(child.gameObject);
            }
        }

        this.ObserveEveryValueChanged(_ => _currentIndex).Where(x => _currentIndex > 0).Subscribe(x => {

            // Debug.Log($"passed city block index: {x}");

            var targetIndex = x - 1;
            
            var localPos = cityBlocks[targetIndex].transform.localPosition;
            localPos.x += pushForwardAmount;
            cityBlocks[targetIndex].transform.localPosition = localPos;
        });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        
    }
}