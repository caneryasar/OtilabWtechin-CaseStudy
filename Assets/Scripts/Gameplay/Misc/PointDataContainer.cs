using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointDataContainer", menuName = "Scriptable Objects/PointDataContainer")]
public class PointDataContainer : ScriptableObject {

    public List<Color> generatedColors;
    public List<GameObject> carPrefabs;
}