using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {  get; private set; }

    //Bu fonksiyon PlayerState'teki bilgiyi yerine yazarak çalıstıracak
    public void InceptionState(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState) 
    { 
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
