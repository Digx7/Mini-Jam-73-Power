// Digx7
// This script should hold the code for each idividual state
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State {

    [Tooltip("This is the name of this state.  This name is used to both track and identify the state.  When naming a state give it a clear and simple name that you will remember")]
    [SerializeField] public string name;

    [Tooltip("This is the description of the state.  This is not used by the code in anyway but can instead be use to explain what this state is suppose to do.")]
    [SerializeField] private string description;

    [Tooltip("This should reference a gameObject with the FSMStateEvents script on it.  The two events on that FSMStateEvents script are called when this state starts and when it is replaced with another state.")]
    [SerializeField] private FSMStateEvents eventsHolder;

    [Tooltip("This is a list of all the transtions that this state has.  A transtion is what tells then the FSM when it should transition to a new state and what state to transition to.")]
    [SerializeField] private List<Transition> transitions;

    // Transitions -----------------------

    public Transition findTransitionWithName(string name) {
        foreach (Transition _transiton in transitions) {
            if (name == _transiton.name) return _transiton;
        }
        return null;
    }

    public Transition findTransistionWithTriggerName(string triggerName) {
        foreach (Transition _transiton in transitions) {
            if (triggerName == _transiton.triggerName) return _transiton;
        }
        return null;
    }

    public bool doesTransitionWithTriggerNameExist(string triggerName) {
        if (findTransistionWithTriggerName(triggerName) != null) return true;
        else return false;
    }

    public bool doesTransitionExist(string name) {
        if (findTransitionWithName(name) != null) return true;
        else return false;
    }

    public int getNumOfTransitions() {
        return transitions.Count;
    }

    // Event holders -----------------------

    public FSMStateEvents getEventHolder() {
        return eventsHolder;
    }

    public void callOnStartStateEvent() {
        eventsHolder.callOnStartState();
    }

    public void callOnEndStateEvent() {
        eventsHolder.callOnEndState();
    }
}