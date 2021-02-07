using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grumbleLabsInterface : MonoBehaviour
{
    [SerializeField] private grumbleAMP grumbleAMP;
    [SerializeField] private int songNumber, layerNumber;

    public void CallPlayNewSong(){
      grumbleAMP.PlaySong(songNumber,layerNumber);
    }

    public void CallCrossFadeToNewLayer(){
      grumbleAMP.CrossFadeToNewLayer(layerNumber);
    }
}
