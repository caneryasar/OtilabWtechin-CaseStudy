using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CarPropertyHandler : MonoBehaviour {

    private EventArchive _eventArchive;
    

    public int carType;
    public MeshRenderer carRenderer;
    public int colorIndex;
    public TextMeshProUGUI pointText;
    private int _currentPointPower;
    private int _currentPoint;

    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnSetupSpawnedCar += Setup;
        // _eventArchive.OnGetHitCarInfo += SendInfo;
        _eventArchive.OnCarHitCheck += CheckHit;

    }

    private void CheckHit(Transform target) {
        
        if(target != transform) { return; }

        _eventArchive.InvokeOnCarSendInfo(_currentPoint);
    }

    private int SendInfo(Transform car) {
        
        if(car == transform) { Debug.Log($"hit this: {car.name}");}
        
        return car != transform ? 0 : _currentPoint;
    }

    private void Setup(Transform target, int point) {
        
        if(target != transform) { return; }
        
        var mat = _eventArchive.InvokeOnGetPowerColor(point);

        _currentPoint = point;
        _currentPointPower = (int)Mathf.Log(point, 2);
        
        carRenderer.material = mat;
        
        var newFormat = MiscHelper.FormatScore(_currentPoint);
        pointText.text = newFormat;
        pointText.outlineColor = mat.color;

        // Debug.Log($"{transform.name} => point: {_currentPoint}, power: {_currentPointPower}, color: {mat.color}");
    }
    
}