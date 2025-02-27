using System;
using UnityEngine;
using Action = Unity.Android.Gradle.Manifest.Action;

public class EventArchive : MonoBehaviour {
    
    public event Action<Vector2> OnGetFirstClickPosition;
    public event Action<Vector2> OnCurrentMousePosition;
    public event Action<bool> OnChangeGameState;


    public event Action<int> OnCityBlockPassed;
    

    public void InvokeOnGetFirstClickPosition(Vector2 pos) { OnGetFirstClickPosition?.Invoke(pos); }
    public void InvokeOnCurrentMousePosition(Vector2 pos) { OnCurrentMousePosition?.Invoke(pos); }
    public void InvokeOnChangeGameState(bool state) { OnChangeGameState?.Invoke(state); }
    public void InvokeOnCityBlockPassed(int count) { OnCityBlockPassed?.Invoke(count); }
}