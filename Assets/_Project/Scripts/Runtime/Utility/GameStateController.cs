using UnityEngine;

public enum GameState
{
    Gameplay,
    Dialog,
    Store,
    Inventory,
    Chest
}

public static class GameStateController
{
    public static GameState _currentState;

    public static void ChangeState(GameState state)
    {
        if (_currentState != state)
        {
            _currentState = state;
            Debug.Log(_currentState);
            return;
        }
    }

    public static void StateGameplay()
    {
        ChangeState(GameState.Gameplay);
    }

}
