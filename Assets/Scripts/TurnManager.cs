using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPhase
{
    Player,
    Enemy,
    Hook
};

public class TurnManager : MonoBehaviour
{
    public TurnPhase current_phase;
    private Dictionary<TurnPhase, PhaseManager> phases;
    private int total_phases;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        this.current_phase = TurnPhase.Player;
        this.total_phases = TurnPhase.GetNames(typeof(TurnPhase)).Length;
        this.phases = new Dictionary<TurnPhase, PhaseManager>();
        this.phases.Add(TurnPhase.Player, new PlayerPhaseManager());
        this.phases.Add(TurnPhase.Enemy, new EnemyPhaseManager());
        this.phases.Add(TurnPhase.Hook, new HookPhaseManager());

        this.timer = 0f;
    }

    void Update()
    {
        if (this.phases[this.current_phase].Update())
        {
            // TODO: use this once we have our phase managers reporting they're finished
            // this.NextTurn();
        }

        // TODO: remove this timer hack
        timer = timer + Time.deltaTime;
        if (timer > 3f)
        {
            this.NextTurn();
            timer = 0f;
        }
    }

    // can we use an event system somehow to trigger this?
    // https://www.youtube.com/watch?v=gx0Lt4tCDE0
    private void NextTurn()
    {
        if ((int)this.current_phase < this.total_phases - 1)
            this.current_phase = this.current_phase + 1;
        else this.current_phase = 0;
    }
}