using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public static GameState CurrentState { get; set; } = GameState.TalkToDoctor;
}
