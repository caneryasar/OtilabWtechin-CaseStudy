using System;
using UnityEngine;

public class EventArchive : MonoBehaviour {
    
    public event Action<Vector2> OnGetFirstClickPosition;
    public event Action<Vector2> OnCurrentMousePosition;
    public event Action<bool> OnChangeGameState;


    public void InvokeOnGetFirstClickPosition(Vector2 pos) { OnGetFirstClickPosition?.Invoke(pos); }
    public void InvokeOnCurrentMousePosition(Vector2 pos) { OnCurrentMousePosition?.Invoke(pos); }
    public void InvokeOnChangeGameState(bool value) { OnChangeGameState?.Invoke(value); }
}