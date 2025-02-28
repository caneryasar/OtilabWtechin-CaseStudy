using System;
using UnityEngine;

public class EventArchive : MonoBehaviour {
    
    public event Action<Vector2> OnGetFirstClickPosition;
    public void InvokeOnGetFirstClickPosition(Vector2 pos) { OnGetFirstClickPosition?.Invoke(pos); }
    
    public event Action<Vector2> OnCurrentMousePosition;
    public void InvokeOnCurrentMousePosition(Vector2 pos) { OnCurrentMousePosition?.Invoke(pos); }
    
    public event Action<bool> OnChangeGameState;
    public void InvokeOnChangeGameState(bool state) { OnChangeGameState?.Invoke(state); }
    

    public event Action<int> OnCityBlockPassed;
    public void InvokeOnCityBlockPassed(int count) { OnCityBlockPassed?.Invoke(count); }
    
    public event Action<GameObject> OnSpawnCars; 
    public void InvokeOnSpawnCars(GameObject cityBlock) { OnSpawnCars?.Invoke(cityBlock); }
    public event Action<GameObject, GameObject> OnDespawnCars; 
    public void InvokeOnDespawnCars(GameObject car1, GameObject car2) { OnDespawnCars?.Invoke(car1, car2); }

    /*
    public event Action OnRequestCar;
    public void InvokeOnRequestCar() { OnRequestCar?.Invoke();}
    
    public event Action<CarPropertyHandler,CarPropertyHandler> OnGetCarsInfo;
    public void InvokeOnGetCarsInfo(CarPropertyHandler car1, CarPropertyHandler car2) { OnGetCarsInfo?.Invoke(car1, car2); }
    */

    public event Func<CarPropertyHandler> OnGetCarInfo;
    public CarPropertyHandler InvokeOnGetCarInfo() { return OnGetCarInfo?.Invoke(); }
    
    /*
    public event Action<int, int> OnCheckForAvailableCars;
    public void InvokeOnCheckForAvailableCars(int carOneType,int carTwoType) { OnCheckForAvailableCars?.Invoke(carOneType, carTwoType); }
    */

    public event Func<int, Transform> OnCheckForIdleCar;
    public Transform InvokeOnCheckForIdleCar(int type) { return OnCheckForIdleCar?.Invoke(type); }
    
    public event Action<Transform, Transform> OnGetAvailableCars;
    public void InvokeOnGetAvailableCars(Transform car1, Transform car2) { OnGetAvailableCars?.Invoke(car1, car2); }


}