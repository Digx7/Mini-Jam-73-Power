// Digx7
// Will move an object using a Rigidbody2D
using UnityEngine;

public class ObjectMove : MonoBehaviour {

    [Tooltip("If this ObjectMover is activated")]
    [SerializeField] private bool isOn = false;

    [SerializeField] private bool StartOnAwake = false;

    [Header("Movement")]
    [SerializeField] private int moveSpeed;

    [SerializeField] private Rigidbody rb;

    public enum MovementMode { TowardDirection, TowardObject }

    [Tooltip("TowardDirection: Move in one preset direction\nTowardObject: Move toward a given GameObject")]
    [SerializeField] private MovementMode movementMode = MovementMode.TowardDirection;

    public enum MomentumMode { Velocity, AddForce }

    [Tooltip("Velocity: Move at constant speed\nAddForce: Move at faster and faster rate")]
    [SerializeField] private MomentumMode momentumMode = MomentumMode.Velocity;

    [Space]
    [Header("Toward Direction Mode")]
    [Tooltip("Only works in TowardDirection Movement Mode")]
    [SerializeField] private Vector3 directionToMove;

    [SerializeField] private bool useLocalSpace = false;

    [Header("Toward Object Mode")]
    [Tooltip("Only works in TowardObject Movement Mode")]
    [SerializeField] private GameObject objectToMoveTowards;

    [SerializeField] private bool lookForGameObjectOnAwake = false;
    [SerializeField] private string tagOfGameObjectToLookFor;

    private Vector3 forwardVel;
    private Vector3 horizontalVel;
    private Vector3 depthVel;

    public void Awake() {
        if (lookForGameObjectOnAwake) findGameObjectWithTag();
        if (StartOnAwake) isOn = true;
    }

    public void FixedUpdate() {
        if (isOn && movementMode == MovementMode.TowardDirection) {
            if (!useLocalSpace) {
              if(momentumMode == MomentumMode.Velocity) rb.velocity = directionToMove * moveSpeed;
              else rb.AddForce(directionToMove * moveSpeed,  ForceMode.Impulse);
            }
            else {
                forwardVel = transform.right * moveSpeed * directionToMove.x;
                horizontalVel = transform.up * moveSpeed * directionToMove.y;
                depthVel.z = transform.position.z * moveSpeed * directionToMove.z;

                if(momentumMode == MomentumMode.Velocity) rb.velocity = forwardVel + horizontalVel + depthVel;
                else rb.AddForce(forwardVel + horizontalVel + depthVel,  ForceMode.Impulse);
            }
        } else if (isOn && movementMode == MovementMode.TowardObject) {
            directionToMove = objectToMoveTowards.transform.position - gameObject.transform.position;
            Vector3.Normalize(directionToMove);

            if(momentumMode == MomentumMode.Velocity) rb.velocity = directionToMove * moveSpeed;
            else rb.AddForce(directionToMove * moveSpeed,  ForceMode.Impulse);
        }
    }

    public void toggleIsOn() {
        isOn = !isOn;
    }

    // --- Get and Set Funtions ---------------------------
    public int getMoveSpeed() {
        return moveSpeed;
    }

    public bool getIsOn() {
        return isOn;
    }

    public void setIsOn(bool input) {
        isOn = input;
    }

    public void zeroOutVelocity() {
        rb.velocity = new Vector3(0, 0, 0);
    }

    public Rigidbody getRB() {
        return rb;
    }

    public void setRB(Rigidbody _rb) {
        rb = _rb;
    }

    public void setDirectionToMoveTo(Vector3 direction) {
        directionToMove = direction;
    }

    public void setDirectionToMoveTo(Vector2 direction) {
        directionToMove = new Vector3(direction.x, direction.y, 0);
    }

    public void setDirectionToMoveTo3D(Vector2 direction) {
        directionToMove = new Vector3(direction.x, 0, direction.y);
    }

    public void setObjectToMoveTowards(GameObject target) {
        objectToMoveTowards = target;
    }

    // --- Auto set Object -----------------

    public bool doesThisObjectHaveTagImLookingFor(GameObject obj) {
        if (obj.tag == tagOfGameObjectToLookFor) return true;
        return false;
    }

    public void findGameObjectWithTag() {
        GameObject[] _object = GameObject.FindGameObjectsWithTag(tagOfGameObjectToLookFor);

        int i = 1;
        if (i == _object.Length) {
            objectToMoveTowards = _object[0];
        }
    }
}
