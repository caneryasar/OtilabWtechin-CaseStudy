using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private EventArchive _eventArchive;
    
    public GameObject player;
    public MeshRenderer playerMeshRenderer;

    public float moveAmount;
    public float _movementSpeed;
    
    public enum PlayerPos { LEFT, RIGHT }
    public PlayerPos playerPos;

    private float _movementSpeedFactor = 1;
    private float _movementResetCounter;
    
    private bool _isPaused = true;
    private bool _isAlive = true;
    
    private Vector2 _swipeStart;
    private Vector2 _swipeCurrent;

    private readonly float ScreenWidth = Screen.width;



    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnChangeGameState += state => _isPaused = state;
        _eventArchive.OnGetFirstClickPosition += startPosition => _swipeStart = startPosition;
        _eventArchive.OnCurrentMousePosition += currentPosition => _swipeCurrent = currentPosition;

        playerPos = PlayerPos.RIGHT;
    }

    private void Start() {
        
    }

    
    private void Update() {
        
        if(!_isAlive) { return; }
        
        if(_isPaused) { return; }
        
        transform.position += transform.forward * (_movementSpeed * _movementSpeedFactor * Time.deltaTime);

        if(_swipeStart != _swipeCurrent) {

            var currentSwipeDistance = _swipeCurrent.x - _swipeStart.x;
            // Debug.Log($"Swipe distance: {currentSwipeDistance}");
            
            if(Mathf.Abs(currentSwipeDistance) >= ScreenWidth * .25f) {

                // Debug.Log($"swipe identified");
                
                if(currentSwipeDistance > 0) {

                    if(playerPos.Equals(PlayerPos.LEFT)) {

                        //todo: implement dotween
                        
                        var localPos = player.transform.localPosition;
                        // localPos.x = Mathf.Lerp(localPos.x, moveAmount, _movementSpeed * Time.deltaTime);
                        localPos.x = moveAmount;
                        player.transform.localPosition = localPos;

                        playerPos = PlayerPos.RIGHT;
                    }
                }
                else {

                    if(playerPos.Equals(PlayerPos.RIGHT)) {
                        
                        //todo: implement dotween
                        var localPos = player.transform.localPosition;
                        // localPos.x = Mathf.Lerp(localPos.x, -moveAmount, _movementSpeed * Time.deltaTime);
                        localPos.x = -moveAmount;
                        player.transform.localPosition = localPos;
                        
                        playerPos = PlayerPos.LEFT;
                    }
                }
            }
        }
    }
}