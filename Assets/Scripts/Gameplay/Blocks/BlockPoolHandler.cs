using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockPoolHandler : MonoBehaviour {

    private EventArchive _eventArchive;

    public List<GameObject> cityBlocks;
    public List<GameObject> idlingCarPrefabs;

    public float pushForwardAmount = 240f;

    private int _currentIndex;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();

        _eventArchive.OnCityBlockPassed += count => _currentIndex = count - 1;
        // _eventArchive.OnCheckForAvailableCars += CheckForCars;
        _eventArchive.OnCheckForIdleCar += CheckIdleCar;
        _eventArchive.OnDespawnCars += GetIdlingCars;

        for(var i = 0; i < transform.childCount; i++) {
            
            var child = transform.GetChild(i);

            if(child.CompareTag("CityBlock")) {
                
                cityBlocks.Add(child.gameObject);
            }
        }
        
        
        this.ObserveEveryValueChanged(_ => _currentIndex).Where(x => _currentIndex > 0).Subscribe(x => {

            // Debug.Log($"passed city block index: {x}");

            var targetIndex = x - 1;
            
            //todo: send selected city block as event make the cityBlock catch itself and despawn-respawn
            
            
            _eventArchive.InvokeOnSpawnCars(cityBlocks[targetIndex]);
            
            var localPos = cityBlocks[targetIndex].transform.localPosition;
            localPos.x += pushForwardAmount;
            cityBlocks[targetIndex].transform.localPosition = localPos;
        });
    }

    private Transform CheckIdleCar(int type) {

        Transform idleCarTransform = null;
        GameObject idleCar = null;

        foreach(var car in idlingCarPrefabs) {

            var carProperties = car.GetComponent<CarPropertyHandler>();

            if(carProperties.carType == type) {
                
                idleCarTransform = car.transform;
                idleCar = car;
                
                break;
            }
        }
        
        if(idleCar) { idlingCarPrefabs.Remove(idleCar); }

        return idleCarTransform;
    }

    private void GetIdlingCars(GameObject car1, GameObject car2) {
        
        idlingCarPrefabs.Add(car1);
        idlingCarPrefabs.Add(car2);
        
        car1.SetActive(false); 
        car2.SetActive(false);
    }

    private void CheckForCars(int type1, int type2) {

        Transform carOneTransform = null;
        Transform carTwoTransform = null;
        
        GameObject carOne = null;
        GameObject carTwo = null;
        
        foreach(var car in idlingCarPrefabs) {

            var carProperties = car.GetComponent<CarPropertyHandler>();

            if(carProperties.carType == type1 && !carOneTransform) {
                
                carOneTransform = car.transform;
                carOne = car;
            }
            
            if(carProperties.carType == type2 && !carTwoTransform) {
                
                carTwoTransform = car.transform;
                carTwo = car;
            }
        }
        
        if(carOne) { idlingCarPrefabs.Remove(carOne); }
        if(carTwo) { idlingCarPrefabs.Remove(carTwo); }
        
        _eventArchive.InvokeOnGetAvailableCars(carOneTransform, carTwoTransform);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        
    }

    // Update is called once per frame
    private void Update() {
        
    }
}