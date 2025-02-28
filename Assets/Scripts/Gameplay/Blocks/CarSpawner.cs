using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour {
    
    private EventArchive _eventArchive;
    
    private List<Transform> _spawnPoints;
    
    private CarPropertyHandler _requestedCarOne;
    private CarPropertyHandler _requestedCarTwo;
    
    private Transform _requestedCarOneTransform;
    private Transform _requestedCarTwoTransform;

    private GameObject _parent;

    private int _spawnIndexOne;
    private int _spawnIndexTwo;

    private void Awake() {
        
        _spawnPoints = new List<Transform>();

        _parent = transform.parent.gameObject;

        for(var i = 0; i < transform.childCount; i++) {  _spawnPoints.Add(transform.GetChild(i)); }

        _eventArchive = FindFirstObjectByType<EventArchive>();

        // _eventArchive.OnGetCarsInfo += GetCarInfo;
        _eventArchive.OnGetAvailableCars += GetIdleCars;
        _eventArchive.OnSpawnCars += CheckAndSpawn;
    }

    private IEnumerator Start() {
        
        // _eventArchive.InvokeOnRequestCar();

        _requestedCarOne = _eventArchive.InvokeOnGetCarInfo();
        _requestedCarTwo = _eventArchive.InvokeOnGetCarInfo();
        
        yield return new WaitForSeconds(.1f);
        
        // _eventArchive.InvokeOnCheckForAvailableCars(_requestedCarOne.carType, _requestedCarTwo.carType);
        _requestedCarOneTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarOne.carType);
        _requestedCarTwoTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarTwo.carType);

        yield return new WaitForSeconds(.1f);
        Spawn();
    }

    /*
    private void GetCarInfo(CarPropertyHandler car1, CarPropertyHandler car2) {

        _requestedCarOne = car1;
        _requestedCarTwo = car2;
        
        _eventArchive.InvokeOnCheckForAvailableCars(_requestedCarOne.carType, _requestedCarTwo.carType);
    }
    */

    private void GetIdleCars(Transform car1Transform, Transform car2Transform) {
        
        _requestedCarOneTransform = car1Transform;
        _requestedCarTwoTransform = car2Transform;
    }


    private void CheckAndSpawn(GameObject target) {

        if(target != _parent) { return; }
            
        Despawn();
            
        // _eventArchive.InvokeOnRequestCar();
            
        _requestedCarOne = _eventArchive.InvokeOnGetCarInfo();
        _requestedCarTwo = _eventArchive.InvokeOnGetCarInfo();
        
        Debug.Log($"new cars: {_requestedCarOne.name}, {_requestedCarTwo.name}");
        
        // _eventArchive.InvokeOnCheckForAvailableCars(_requestedCarOne.carType, _requestedCarTwo.carType);
        _requestedCarOneTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarOne.carType);
        _requestedCarTwoTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarTwo.carType);
        GetSpawnPoints();
            
        var spawnPointOne = _spawnPoints[_spawnIndexOne];
        var spawnPointTwo = _spawnPoints[_spawnIndexTwo];

            
        //todo respawn cars
        DOVirtual.DelayedCall(.15f, () => {
            
            if(_requestedCarOneTransform) {
                
                _requestedCarOneTransform.gameObject.SetActive(true);
                _requestedCarOneTransform.position = spawnPointOne.position;
                //todo: send an event with this transform for the cars to catch themselves and setup themselves 
            }
            else {
                
                var spawnedCarOne = Instantiate(_requestedCarOne, spawnPointOne.position, Quaternion.identity);
                _requestedCarOneTransform = spawnedCarOne.transform;
            }
            
            if(_requestedCarTwoTransform) {
                
                _requestedCarTwoTransform.gameObject.SetActive(true);
                _requestedCarTwoTransform.position = spawnPointTwo.position;
                //todo: send an event with this transform for the cars to catch themselves and setup themselves 
            }
            else {
                
                var spawnedCarTwo = Instantiate(_requestedCarTwo, spawnPointTwo.position, Quaternion.identity);
                _requestedCarTwoTransform = spawnedCarTwo.transform;
            }
        });

    }

    private void Spawn() {
        
        GetSpawnPoints();
            
        var spawnPointOne = _spawnPoints[_spawnIndexOne];
        var spawnPointTwo = _spawnPoints[_spawnIndexTwo];

        var spawnedCarOne = Instantiate(_requestedCarOne, spawnPointOne.position, Quaternion.identity);
        _requestedCarOneTransform = spawnedCarOne.transform;        
        var spawnedCarTwo = Instantiate(_requestedCarTwo, spawnPointTwo.position, Quaternion.identity);
        _requestedCarTwoTransform = spawnedCarTwo.transform;
    }

    private void Despawn() {
        
        _eventArchive.InvokeOnDespawnCars(_requestedCarOneTransform.gameObject, _requestedCarTwoTransform.gameObject);

        Debug.Log($"despawned cars: {_requestedCarOneTransform.gameObject.name}, {_requestedCarTwoTransform.gameObject.name}");
        
        _requestedCarOne = null;
        _requestedCarTwo = null;
        _requestedCarOneTransform = null;
        _requestedCarTwoTransform = null;
    }

    private void GetSpawnPoints() {
        
        _spawnIndexOne = Random.Range(0, _spawnPoints.Count);
        _spawnIndexTwo = Random.Range(0, _spawnPoints.Count);

        if(_spawnIndexOne == _spawnIndexTwo) {

            while(_spawnIndexOne == _spawnIndexTwo) {
                
                _spawnIndexTwo = Random.Range(0, _spawnPoints.Count);
            }
            
        }
    }
}