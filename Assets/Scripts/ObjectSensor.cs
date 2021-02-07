// Digx7
// This script activates an event if it detected something in its collider or trigger
using System.Collections;
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

    [SerializeField] private bool allowDuplicates = true;
    [SerializeField] private float timeBetweenDetectingDuplicates = 0.1f;

    [Tooltip("Set the tag of any objects this sensor is looking for.")]
    [SerializeField] private List<string> tagsToListenFor;

    [Space]
    [Header("Events")]
    [SerializeField] private UnityEvent detectedSomething;

    private Collision lastCollision;
    private Collider lastCollider;

    private void DetectedSomething(Collision col) {
      if(allowDuplicates) InvokeDetectedSomething();
      else{
        if(newCollision(col)) InvokeDetectedSomething();
      }
    }

    private void DetectedSomething(Collider col) {
      if(allowDuplicates) InvokeDetectedSomething();
      else{
        if(newCollider(col)) InvokeDetectedSomething();
      }
    }

    // --- Events ----------------------------------------------

    private void InvokeDetectedSomething() {
        detectedSomething.Invoke();
    }

    // --- Confermation Functions ------------------------------

    private bool isThisAnObjectToWatchFor_Collision(Collision col) {
        foreach (string _tag in tagsToListenFor) {
            if (col.gameObject.tag == _tag) return true;
        }
        return false;
    }

    private bool isThisAnObjectToWatchFor_Collider(Collider col) {
        foreach (string _tag in tagsToListenFor) {
            if (col.gameObject.tag == _tag) return true;
        }
        return false;
    }

    private bool newCollision(Collision col) {
      if(col == lastCollision) return false;
      else{
        setLastCollision(col);
        return true;
        }
    }

    private bool newCollider(Collider col) {
      if(col == lastCollider) return false;
      else{
        setLastCollider(col);
        return true;
        }
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

    private void setLastCollision(Collision input) {
      lastCollision = input;
      StartCoroutine(clearCollisionAndCollider());
    }

    private void setLastCollider(Collider input) {
      lastCollider = input;
      StartCoroutine(clearCollisionAndCollider());
    }

    private IEnumerator clearCollisionAndCollider(){
      yield return new WaitForSeconds(timeBetweenDetectingDuplicates);
      lastCollider = null;
      lastCollision = null;
      yield return null;
    }

    // --- Collisions --------------------------------------------

    // Enter ---
    public void OnCollisionEnter(Collision col) {
        if (sensorMode == mode.Collider && detectOnEnter && getDetectEverything() || isThisAnObjectToWatchFor_Collision(col))
          DetectedSomething(col);
    }

    // Triggers when the player collides with a trigger
    public void OnTriggerEnter(Collider col) {
        if (sensorMode == mode.Trigger && detectOnEnter && getDetectEverything() || isThisAnObjectToWatchFor_Collider(col))
            DetectedSomething(col);
    }

    // Stay ---
    public void OnCollisionStay(Collision col) {
        if (sensorMode == mode.Collider && detectOnStay && getDetectEverything() || isThisAnObjectToWatchFor_Collision(col))
            DetectedSomething(col);
    }

    // Triggers when the player collides with a trigger
    public void OnTriggerStay(Collider col) {
        if (sensorMode == mode.Trigger && detectOnStay && getDetectEverything() || isThisAnObjectToWatchFor_Collider(col))
            DetectedSomething(col);
    }

    // Exit ---
    public void OnCollisionExit(Collision col) {
        if (sensorMode == mode.Collider && detectOnExit && getDetectEverything() || isThisAnObjectToWatchFor_Collision(col))
            DetectedSomething(col);
    }

    // Triggers when the player collides with a trigger
    public void OnTriggerExit(Collider col) {
        if (sensorMode == mode.Trigger && detectOnExit && getDetectEverything() || isThisAnObjectToWatchFor_Collider(col))
            DetectedSomething(col);
    }
}
