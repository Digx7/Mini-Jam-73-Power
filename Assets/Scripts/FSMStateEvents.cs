// Digx7
// This script should hold all the code for each event of each state
using UnityEngine;
using UnityEngine.Events;

public class FSMStateEvents : MonoBehaviour {

    [Tooltip("This event is called when the state that is attached to it is first updated")]
    [SerializeField] private UnityEvent StartState;

    [Tooltip("This event is called when the state that is attached to it is updated for a different state")]
    [SerializeField] private UnityEvent EndState;

    public void callOnStartState() {
        StartState.Invoke();
    }

    public void callOnEndState() {
        EndState.Invoke();
    }
}