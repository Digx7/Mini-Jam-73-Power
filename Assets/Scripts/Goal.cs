using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    public UnityEvent playerFoundFish;

    public void OnCollisionEnter(Collision col){
      if(col.gameObject.tag == playerTag) {
        Debug.Log("You found me!");
        playerFoundFish.Invoke();
      }
    }
}
