using UniRx;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour {

    public MeshRenderer renderer;
    public int colorIndex;
    
    internal float health = 100f;
    internal int currentScore = 2;
    
    
    
    private void Start() {
        
        //todo: trigger ui point event

        this.ObserveEveryValueChanged(_ => health).Where(_ => health > 0).Subscribe(_ => {

            //todo: trigger ui event
        });

    }

    private void ReduceHealth(int hitPoint) {
        
        
    }

    private void IncreasePoint() {
        
        
    }
}