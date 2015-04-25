using System;

[Flags]
public enum GameState 
{ 
    // main flow
    PressPlay  = 1,
    Playing    = 2,
    GameOver   = 4,
    // pig
    PigAlive   = 8,
    PigDead    = 16,
    // snow
    NotSnowing = 32,
    Showing1   = 64,
    Showing2   = 128,
    Showing3   = 256,
    Showing4   = 512,
    Showing5   = 1024
}

public class StateChangeEventArgs
{ 
    public GameState oldState;
    public GameState newState;
}

public static class GameStates
{
    private static GameState gameState
        = GameState.PressPlay & GameState.PigAlive & GameState.NotSnowing;

    public delegate void StateChangeHandler(StateChangeEventArgs e);
    
    public static event StateChangeHandler OnBeforeStateChange
        = null;

    public static GameState GameState
    {
        get { return gameState; }
        set 
        { 
            if (OnBeforeStateChange != null) { OnBeforeStateChange(new StateChangeEventArgs() { oldState = GameState, newState = value }); }
            gameState = value; 
        }
    }
}
