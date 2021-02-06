// Digx7
// This script should hold all the code for each transition
using UnityEngine;

[System.Serializable]
public class Transition {

    [Tooltip("Name: the name of this transition\nTriggerName: the name of the trigger that causes this transition\nNewStateName: the name of the new state to transition to\n(All names are case sensitive)")]
    [SerializeField]
    public string name,
                                   triggerName,
                                   newStateName;
}