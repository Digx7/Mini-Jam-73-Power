using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grumbleLabsInterface : MonoBehaviour
{
    [SerializeField] private grumbleAMP grumbleAMP;
    [SerializeField] private int songNumber, layerNumber;

    public void CallPlayNewSong(){
      if(grumbleAMP.PlaySong(songNumber,layerNumber)) StartCoroutine(CallNewSong());  
    }

    public void CallCrossFadeToNewLayer(){
      grumbleAMP.CrossFadeToNewLayer(layerNumber);
    }

    IEnumerator CallNewSong(){
      yield return null;
      while(grumbleAMP.PlaySong(songNumber,layerNumber)){
        yield return null;
      }
      yield return null;
    }
}
