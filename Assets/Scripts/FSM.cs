// Digx7
// This script should manger the higher level FSM funcitons
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    [Header("Run time ---------------")]
    [Tooltip("This is the number of the current state in the list of all the states.  The list starts at 0 and works its way up.  You can set this before hand to determine the starting state")]
    [SerializeField] private int indexOfCurrentState = 0;

    [Tooltip("This is the currently active state.  You don't need to adjust this in the inspector but can use it for debugging if the FSM seems to be malfuntioning.")]
    [SerializeField] private State currentState;

    [Space]
    [Header("States and Triggers ----")]
    [Tooltip("This is the list of all the triggers.  These are what the FSM uses to know if it should transition from one state to the next.")]
    [SerializeField] private List<trigger> triggers;

    [Tooltip("This is the list of all the States.  These are the varous states that the FSM can be in.  The FSM can only have one active state at a time.  This state is displayed above in the current state.")]
    [SerializeField] private List<State> states;

    // Main functions -----------------------

    public void activateTriggerWithName(string name) {
        transitionToNewState(findNewStateNameUsingTriggerName(name));
    }

    public string findNewStateNameUsingTriggerName(string triggerName) {
        if (doesTriggerExist(triggerName)) {
            Debug.Log("There is a trigger with the name '" + triggerName + "'");
            if (doesTransitionWithTriggerNameExistOnCurrentState(triggerName)) {
                Debug.Log("There is a transtion with the '" + triggerName + "' trigger on the current state '" + currentState.name + "'");
                Transition _transiton = findTransitionWithTriggerNameOnCurrentState(triggerName);
                Debug.Log("This transition is called '" + _transiton.name + "'");
                Debug.Log("The next state that should be transitioned to is '" + _transiton.newStateName + "'");

                return _transiton.newStateName;
            } else return null;
        } else return null;
    }

    public void transitionToNewState(string newStateName) {
        if (doesStateExist(newStateName)) {
            Debug.Log("The state named '" + newStateName + "' does exist");
            callOnEndStateEventOnCurrentState();
            updateCurrentStateWithName(newStateName);
            Debug.Log("The current state should have been updated");
            callOnStartStateEventOnCurrentState();
        }
    }

    // currentState --------------------------

    public void updateCurrentStateWithName(string name) {
        if (doesStateExist(name)) {
            currentState = findStateWithName(name);
        }
    }

    public void updateCurrentStateWithIndex(int index) {
        if (doesStateExistIndex(index)) {
            currentState = findStateWithIndex(index);
        }
    }

    public string getCurrentStateName(){
      return currentState.name;
    }

    // Transitions

    public Transition findTransitionWithNameOnCurrentState(string name) {
        return currentState.findTransitionWithName(name);
    }

    public Transition findTransitionWithTriggerNameOnCurrentState(string triggerName) {
        return currentState.findTransistionWithTriggerName(triggerName);
    }

    public bool doesTransitionExistOnCurrentState(string name) {
        return currentState.doesTransitionExist(name);
    }

    public bool doesTransitionWithTriggerNameExistOnCurrentState(string triggerName) {
        return currentState.doesTransitionWithTriggerNameExist(triggerName);
    }

    public int getNumOfTransitionsOnCurrentState() {
        return currentState.getNumOfTransitions();
    }

    // Event holder

    public void callOnStartStateEventOnCurrentState() {
        currentState.callOnStartStateEvent();
    }

    public void callOnEndStateEventOnCurrentState() {
        currentState.callOnEndStateEvent();
    }

    // States -------------------------------

    public State findStateWithName(string name) {
        foreach (State _state in states) {
            if (name == _state.name) return _state;
        }
        return null;
    }

    public State findStateWithIndex(int index) {
        return states[index];
    }

    public bool doesStateExist(string name) {
        if (findStateWithName(name) != null) return true;
        else return false;
    }

    public bool doesStateExistIndex(int index) {
        if (index <= getNumOfStates() - 1) return true;
        else return false;
    }

    public int getNumOfStates() {
        return states.Count;
    }

    // Triggers ----------------------------

    public trigger findTriggerWithName(string name) {
        foreach (trigger _trigger in triggers) {
            if (name == _trigger.name) return _trigger;
        }
        return null;
    }

    public bool doesTriggerExist(string name) {
        if (findTriggerWithName(name) != null) return true;
        else return false;
    }

    public int getNumOfTriggers() {
        return triggers.Count;
    }

    // Awake and Update -------------------

    public void Awake() {
        updateCurrentStateWithIndex(indexOfCurrentState);
        callOnStartStateEventOnCurrentState();
    }
}
