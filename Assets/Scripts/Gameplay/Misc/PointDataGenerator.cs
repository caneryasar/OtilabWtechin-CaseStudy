using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointDataGenerator : MonoBehaviour {

    private EventArchive _eventArchive;
    public PointDataContainer dataContainer;

    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();

        _eventArchive.OnGetCarInfo += ReturnRandomCar;
        
        GenerateColors();
    }

    private CarPropertyHandler ReturnRandomCar() { return dataContainer.carPrefabs[Random.Range(0, dataContainer.carPrefabs.Count)].GetComponent<CarPropertyHandler>(); }


    private void GenerateColors() {

        for(var i = 0; i < 10; i++) {
            
            var r = Random.Range(0f, 255f);
            var g = Random.Range(0f, 255f);
            var b = Random.Range(0f, 255f);
        
            var color = new Color(r, g, b, 255f);
        
            dataContainer.generatedColors.Add(color);
        }
    }
}