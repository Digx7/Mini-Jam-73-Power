// Digx
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
    [SerializeField] private float destroyTimeDelay = 0.0f;
    [SerializeField] private bool destroyOnImpactWithAnyColliders = false;
    [SerializeField] private bool destroyOnImpactWithAnyTriggers = false;
    [SerializeField] private bool destroyOnAwake = false;
    [SerializeField] private List<string> tagsThatCanDestroyThisObject;
    // Object health? If we want it to destroy with a few hits?

    public void Awake() {
        if (destroyOnAwake) _Destory();
    }

    public void _Destory() {
        Destroy(gameObject, destroyTimeDelay);
    }

    public bool canThisObjectDamageMe(Collision col) {
        foreach (string _tag in tagsThatCanDestroyThisObject) {
            if (col.gameObject.tag == _tag) return true;
        }
        return false;
    }

    public bool canThisObjectDamageMe_Collider(Collider col) {
        foreach (string _tag in tagsThatCanDestroyThisObject) {
            if (col.gameObject.tag == _tag) return true;
        }
        return false;
    }

    public void OnCollisionEnter(Collision col) {
        if (destroyOnImpactWithAnyColliders) _Destory();
        else if (canThisObjectDamageMe(col)) _Destory();
    }

    public void OnTriggerEnter(Collider col) {
        if (destroyOnImpactWithAnyTriggers) _Destory();
        else if (canThisObjectDamageMe_Collider(col)) _Destory();
    }

    // called when object broadcasts it's destroyed
    public void objectDestroyed() {
        //AudioSource objectDestoyedSound = GetComponent<AudioSource>();
        //objectDestoyedSound.Play();
        Debug.Log("object destoyed sound");
    }
}
