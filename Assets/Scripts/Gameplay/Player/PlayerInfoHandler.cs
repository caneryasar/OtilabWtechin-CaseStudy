using UniRx;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour {

    public MeshRenderer renderer;
    public int colorIndex;

    private int _scorePower;
    
    private float _health = 100f;
    private int _currentScore = 2;
    
    
    
    private void Start() {
        
        //todo: trigger ui point event

        this.ObserveEveryValueChanged(_ => _health).Where(_ => _health > 0).Subscribe(_ => {

            //todo: trigger ui event
        });

    }

    private void ReduceHealth(int hitPoint) {
        
        
    }

    private void IncreasePoint() {
        
        
    }
}