using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CarPropertyHandler : MonoBehaviour {

    public MeshRenderer renderer;
    public int colorIndex;
    public TextMeshProUGUI pointText;

    internal void Setup(int point, Material color) {
        
        renderer.material = color;
        pointText.text = $"{point}";
    }
    
}