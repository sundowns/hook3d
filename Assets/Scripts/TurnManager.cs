using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPhase
{
    Player,
    Enemy,
    Hook
};

public class PhaseChangeEventArgs : EventArgs
{
    public TurnPhase phase;
}

public class TurnManager : MonoBehaviour
{
    public event EventHandler onTurnEnd;
    public event EventHandler<PhaseChangeEventArgs> onPhaseStart;
    public event EventHandler<PhaseChangeEventArgs> onPhaseEnd;

    public TurnPhase current_phase;
    private Dictionary<TurnPhase, PhaseManager> phases;
    private int total_phases;

    // TODO: delete
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        this.current_phase = TurnPhase.Player;
        this.total_phases = TurnPhase.GetNames(typeof(TurnPhase)).Length;
        this.phases = new Dictionary<TurnPhase, PhaseManager>();
        this.RegisterPhase<PlayerPhaseManager>(TurnPhase.Player);
        // this.phases.Add(TurnPhase.Player, new PlayerPhaseManager());
        this.RegisterPhase<EnemyPhaseManager>(TurnPhase.Enemy);
        // this.phases.Add(TurnPhase.Enemy, new EnemyPhaseManager());
        this.RegisterPhase<HookPhaseManager>(TurnPhase.Hook);
        // this.phases.Add(TurnPhase.Hook, new HookPhaseManager());

        this.timer = 0f;
    }

    void RegisterPhase<T>(TurnPhase new_phase) where T : PhaseManager, new()
    {
        var phase_manager = new T();
        this.onPhaseStart += phase_manager.OnPhaseStart;
        this.onPhaseEnd += phase_manager.OnPhaseEnd;
        this.phases.Add(new_phase, phase_manager);
    }

    void Update()
    {
        if (this.phases[this.current_phase].Update())
        {
            // use this once we have our phase managers reporting they're finished
            // this.NextPhase();
        }

        // remove this timer hack
        timer = timer + Time.deltaTime;
        if (timer > 1f)
        {
            this.NextPhase();
            timer = 0f;
        }
    }

    private void NextPhase()
    {
        onPhaseEnd?.Invoke(this, new PhaseChangeEventArgs { phase = this.current_phase });
        if ((int)this.current_phase < this.total_phases - 1)
        {

            this.current_phase = this.current_phase + 1;
            onPhaseStart?.Invoke(this, new PhaseChangeEventArgs { phase = this.current_phase });
        }
        else
        {
            this.NextTurn();
        }
    }

    private void NextTurn()
    {
        Debug.Log("Next Turn");
        onTurnEnd?.Invoke(this, EventArgs.Empty);
        this.current_phase = 0;
        onPhaseStart?.Invoke(this, new PhaseChangeEventArgs { phase = this.current_phase });
    }
}