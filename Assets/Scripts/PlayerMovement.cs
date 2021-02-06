using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [Space]
    [SerializeField] private bool movementIsOn = true;
    [SerializeField] private float forwardSpeedAdjuster = 1.0f;
    [SerializeField] private float backwardSpeedAdjuster = 1.0f;
    [SerializeField] private float leftSpeedAdjuster = 1.0f;
    [SerializeField] private float rightSpeedAdjuster = 1.0f;
    [SerializeField] private float upSpeedAdjuster = 1.0f;
    [SerializeField] private float downSpeedAdjuster = 1.0f;
    [Space]
    [SerializeField] private bool rotationIsOn = true;
    [SerializeField] private float upDownRotationAdjuster = 0.5f;
    [SerializeField] private float leftRightRotationAdjuster = 0.5f;

    public void Awake(){
      if(rb == null) rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void moveObjectHorizonally(Vector2 input){

      Vector2 _input = horizontalInputProcessing(input);

      Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
      locVel.x = _input.x * Time.deltaTime;
      //locVel.x = 0.0f;
      locVel.z = _input.y * Time.deltaTime;
      locVel.y = 0.0f;
      if(movementIsOn)rb.velocity = transform.TransformDirection(locVel);

      //rb.velocity = new Vector3(_input.x, 0, _input.y) * Time.deltaTime;
    }

    public void moveObjectVertical(float input){
      Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
      locVel.x = 0.0f;
      locVel.z = 0.0f;
      locVel.y = verticalMovementProcessing(input) * Time.deltaTime;
      if(movementIsOn)rb.velocity = transform.TransformDirection(locVel);

      //rb.velocity = new Vector3(0, verticalMovementProcessing(input), 0) * Time.deltaTime;
    }

    public void rotateObject(Vector2 input){
      Vector3 rotation = new Vector3();

      rotation.y += input.x * leftRightRotationAdjuster;
      rotation.x += -input.y * upDownRotationAdjuster;

      if(rotationIsOn)gameObject.transform.Rotate(rotation);
    }

    public void turnOffMovement(){
      movementIsOn = false;
      rb.velocity = new Vector3(0,0,0);
    }

    public void turnOnMovement(){
      movementIsOn = true;
    }

    public void turnOffRotation(){
      rotationIsOn = false;
    }

    public void turnOnRotation(){
      rotationIsOn = true;
    }

    private Vector2 horizontalInputProcessing(Vector2 input){
      Vector2 output = input;

      if(output.x > 0.1) output.x *= rightSpeedAdjuster;
      else if(output.x < -0.1) output.x *= leftSpeedAdjuster;
      else output.x = 0;

      if(output.y < -0.1) output.y *= backwardSpeedAdjuster;
      else if(output.y > 0.1) output.y *= forwardSpeedAdjuster;
      else output.y = 0;

      return output;
    }

    private float verticalMovementProcessing(float input){
      float output = input;

      if(output > 0.1) output *= upSpeedAdjuster;
      else if(output < -0.1) output *= downSpeedAdjuster;
      else output = 0;

      return output;
    }
}
