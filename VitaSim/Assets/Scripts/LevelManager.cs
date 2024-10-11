using UnityEngine;
using System;

public interface LevelManager {
    public static LevelManager Inst;
    public void StartLevel(Action<int> OnFinish);
    public void CompleteLevel();
}