// Digx7
// This script should hold the code foreach trigger
using UnityEngine;

[System.Serializable]
public class trigger {

    [Tooltip("The name of this trigger.  This is what is used by outside code to update the current state.\n(Name is case sensitive)")]
    [SerializeField] public string name;

    /*[SerializeField] public enum types {Int, Bool, Float}
    [SerializeField] public types type;
    [SerializeField] public float value = 0;

    public void activate(){
      Debug.Log("Trigger " + name + " has been activated");
    }*/
}