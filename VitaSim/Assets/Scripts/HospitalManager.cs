using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    TalkToDoctor,
    PickUpKit,
    DeliverKit
}

public class HospitalManager : MonoBehaviour, LevelManager
{
    [SerializeField] public GameState State;

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.TalkToDoctor:
                HandleTalkToDoctor();
                break;
            case GameState.PickUpKit:
                HandlePickUpKit();
                break;
            case GameState.DeliverKit:
                HandleDeliverKit();
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        UpdateGameState(GameStateManager.CurrentState);
    }

    void HandleTalkToDoctor()
    {
        UIManager.Instance.UpdateTaskText("Talk to the doctor");
        UIManager.Instance.UpdateDailyTaskText("0/3 Tasks completed");
    }

    void HandlePickUpKit()
    {
        UIManager.Instance.UpdateTaskText("Pickup Kit");
        UIManager.Instance.UpdateDailyTaskText("1/3 Tasks completed");
    }

    void HandleDeliverKit()
    {
        UIManager.Instance.UpdateTaskText("Deliver Kit to doctor");
        UIManager.Instance.UpdateDailyTaskText("2/3 Tasks completed");
    }

    void LevelManager.StartLevel(Action<int> OnFinish)
    {
        throw new NotImplementedException();
    }

    void LevelManager.CompleteLevel()
    {
        throw new NotImplementedException();
    }

}
