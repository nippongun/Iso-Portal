using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float regularMoveSpeed = 5f;
    [SerializeField] private float shiftMoveSpeed = 10f;
    [SerializeField] private float jumpIntensity = .5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float moveSpeed;
    private bool isGrounded;

    Vector3 forward;
    Vector3 right;
    Rigidbody rigidbody;

    void Awake()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0))*forward;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f,~(1 << 8));

        if(Input.anyKey){
            Movement();
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = shiftMoveSpeed;
        } else moveSpeed = regularMoveSpeed;

    }

    void Movement(){
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"),0,Input.GetAxis("VerticalKey"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 forwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 headingDirection = Vector3.Normalize(rightMovement + forwardMovement);

        transform.rotation = Quaternion.LookRotation(headingDirection);

        Debug.Log(Quaternion.LookRotation(headingDirection));
        transform.position += rightMovement;
        transform.position += forwardMovement;

        if (!isGrounded){
            rigidbody.velocity += headingDirection;
        }
        Jump();
    }


    void Jump(){   

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rigidbody.AddForce(Vector3.up * jumpIntensity,ForceMode.Impulse);
            //rigidbody.velocity = Vector3.up * jumpIntensity;
            isGrounded = false;
        }

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1 ) * Time.deltaTime;
        } else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1 ) * Time.deltaTime;
        }
    }
}
