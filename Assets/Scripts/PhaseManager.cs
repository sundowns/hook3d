using System;
using UnityEngine;

public abstract class PhaseManager
{
    private TurnPhase phase;
    public PhaseManager(TurnPhase phase)
    {
        this.phase = phase;
    }

    // Return true if the phase is over
    public abstract bool Update();
    public void OnPhaseStart(object sender, PhaseChangeEventArgs e)
    {
        if (e.phase == phase)
            Debug.Log($"{phase} started.");
    }
    public void OnPhaseEnd(object sender, PhaseChangeEventArgs e)
    {
        if (e.phase == phase)
            Debug.Log($"{phase} ended.");
    }
}

public class PlayerPhaseManager : PhaseManager
{
    public PlayerPhaseManager() : base(TurnPhase.Player) { }

    public override bool Update()
    {
        return false;
    }
}

public class EnemyPhaseManager : PhaseManager
{
    public EnemyPhaseManager() : base(TurnPhase.Enemy) { }

    public override bool Update()
    {
        return false;
    }
}

public class HookPhaseManager : PhaseManager
{
    public HookPhaseManager() : base(TurnPhase.Hook) { }

    public override bool Update()
    {
        return false;
    }
}
