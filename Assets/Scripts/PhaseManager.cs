using System.Collections;
using System.Collections.Generic;
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
}

public class PlayerPhaseManager : PhaseManager
{
    public PlayerPhaseManager() : base(TurnPhase.Player) { }

    public override bool Update()
    {
        Debug.Log($"Player phase");

        return false;
    }
}

public class EnemyPhaseManager : PhaseManager
{
    public EnemyPhaseManager() : base(TurnPhase.Enemy) { }

    public override bool Update()
    {
        Debug.Log($"Enemy phase");

        return false;
    }
}

public class HookPhaseManager : PhaseManager
{
    public HookPhaseManager() : base(TurnPhase.Hook) { }

    public override bool Update()
    {
        Debug.Log($"Hook phase");

        return false;
    }
}
