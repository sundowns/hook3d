using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public enum TurnPhase
    {
        Player,
        Enemy,
        Hook
    };

    public TurnPhase current_phase;

    // Start is called before the first frame update
    void Start()
    {
        this.current_phase = TurnPhase.Player;
    }
}
