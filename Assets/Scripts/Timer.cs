// Digx7
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private bool StartOnAwake = false;
    [SerializeField] private bool looping = false;
    [SerializeField] private float currentTime;
    [SerializeField] private float targetTime;
    private float timeDifference;
    private bool isTimerGoing = false;

    public enum CountDirection {CountUp, CountDown}

    [SerializeField] private CountDirection countDirection = CountDirection.CountUp;

    [SerializeField] private UnityEvent TimerDone;

    // --- Main Methods -------------------------

    public void Awake(){
      if(StartOnAwake) startTimer();
    }

    public void startTimer(){
      if(!isTimerGoing){
        //Debug.Log("Starting Timer");
        StartCoroutine(timer());
      }
    }

    public void stopTimer(){
      isTimerGoing = false;
      StopAllCoroutines();
    }

    public void restartTimer(){
      //Debug.Log("Timer is being restarted");
      StopCoroutine(timer());
      StartCoroutine(timer());
    }

    public void TimerDoneEvent(){
      TimerDone.Invoke();
      //Debug.Log("Timer is done");
    }

    private bool shouldTimerLoop(){
      if(looping) {
        //Debug.Log("Timer should be looping");
        return true;
      }
      else return false;
    }

    // --- Get/Set -----------------------------

    public void setCurrentTime(float input){
      currentTime = input;
    }

    public void setTargetTime(float input){
      currentTime = input;
    }

    public float getCurrentTime(){
      return currentTime;
    }

    public float getTargetTime(){
      return targetTime;
    }

    public float getTimeDifference(){
      if(countDirection == CountDirection.CountUp)
        return targetTime - currentTime;
      else return currentTime - targetTime;
    }

    // --- IEnumerators ---------------------

    private IEnumerator timer(){
      isTimerGoing = true;
      //Debug.Log("Time difference is " + getTimeDifference());
      yield return new WaitForSeconds(getTimeDifference());
      //Debug.Log("Time should be done");
      TimerDoneEvent();
      isTimerGoing = false;
      if(shouldTimerLoop())startTimer();
      yield return null;
    }
}
