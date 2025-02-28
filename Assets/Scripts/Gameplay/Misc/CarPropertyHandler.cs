using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CarPropertyHandler : MonoBehaviour {

    public int carType;
    public MeshRenderer carRenderer;
    public int colorIndex;
    public TextMeshProUGUI pointText;
    internal int currentPointPower;

    internal void Setup(int point, Color color) {
        
        carRenderer.materials[colorIndex].color = color;
        pointText.text = $"{point}";
    }
    
}