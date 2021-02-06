// Digx7
// This script activates an event if it detected something in its collider or trigger
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSensor : MonoBehaviour {

    public enum mode { Collider, Trigger }

    [Header("Main")]
    [Tooltip("Collider: for if you have a generic  box collider attached.\nTrigger: for if you have the collider set as a trigger.")]
    [SerializeField] private mode sensorMode = mode.Collider;

    [Tooltip("Set this is you want to detect everything regardless of any tags.")]
    [SerializeField] private bool detectEverything = false;

    [SerializeField] private bool detectOnEnter = true;
    [SerializeField] private bool detectOnStay = false;
    [SerializeField] private bool detectOnExit = false;

    [Tooltip("Set the tag of any objects this sensor is looking for.")]
    [SerializeField] private List<string> tagsToListenFor;

    [Space]
    [Header("Events")]
    [SerializeField] private UnityEvent detectedSomething;

    private Collision lastCollision;
    private Collider lastCollider;

    // --- Events ----------------------------------------------

    public void InvokeDetectedSomething() {
        detectedSomething.Invoke();
    }

    // --- Confermation Functions ------------------------------

    public bool isThisAnObjectToWatchFor_Collision(Collision col) {
        foreach (string _tag in tagsToListenFor) {
            if (col.gameObject.tag == _tag) return true;
        }
        return false;
    }

    public bool isThisAnObjectToWatchFor_Collider(Collider col) {
        foreach (string _tag in tagsToListenFor) {
            if (col.gameObject.tag == _tag) return true;
        }
        return false;
    }

    // --- Get variables -------------------------------------------

    public Collision getLastCollision() {
        return lastCollision;
    }

    public Collider getLastCollider() {
        return lastCollider;
    }

    public bool getDetectEverything() {
        return detectEverything;
    }

    // --- Collisions --------------------------------------------

    // Enter ---
    public void OnCollisionEnter(Collision col) {
        if (detectOnEnter && getDetectEverything() || isThisAnObjectToWatchFor_Collision(col))
            InvokeDetectedSomething();
    }

    // Triggers when the player collides with a trigger
    public void OnTriggerEnter(Collider col) {
        if (detectOnEnter && getDetectEverything() || isThisAnObjectToWatchFor_Collider(col))
            InvokeDetectedSomething();
    }

    // Stay ---
    public void OnCollisionStay(Collision col) {
        if (detectOnStay && getDetectEverything() || isThisAnObjectToWatchFor_Collision(col))
            InvokeDetectedSomething();
    }

    // Triggers when the player collides with a trigger
    public void OnTriggerStay(Collider col) {
        if (detectOnStay && getDetectEverything() || isThisAnObjectToWatchFor_Collider(col))
            InvokeDetectedSomething();
    }

    // Exit ---
    public void OnCollisionExit(Collision col) {
        if (detectOnExit && getDetectEverything() || isThisAnObjectToWatchFor_Collision(col))
            InvokeDetectedSomething();
    }

    // Triggers when the player collides with a trigger
    public void OnTriggerExit(Collider col) {
        if (detectOnExit && getDetectEverything() || isThisAnObjectToWatchFor_Collider(col))
            InvokeDetectedSomething();
    }
}
