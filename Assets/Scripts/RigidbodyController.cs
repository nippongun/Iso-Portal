using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{

    [SerializeField] private float regularMoveSpeed = 2;
    [SerializeField] private float shiftMoveSpeed = 3;
    [SerializeField] private float jumpIntensity = 0.5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float moveSpeed;
    [SerializeField]private bool isGrounded;

    Vector3 forward;
    Vector3 right;
    Rigidbody rb;

    void Awake()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0))*forward;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2 + 0.1f,~(1 << 8));
        if(Input.anyKey){
            
            forward = Camera.main.transform.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0,90,0))*forward;

            Movement();
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = shiftMoveSpeed;
        } else moveSpeed = regularMoveSpeed;

    }

    void Movement(){
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 forwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 headingDirection = Vector3.Normalize(rightMovement + forwardMovement);
        
        rb.AddForce(headingDirection,ForceMode.VelocityChange);
        //transform.rotation = Quaternion.LookRotation(headingDirection);
        transform.forward = headingDirection;

        if(Input.GetKey(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector3.up * jumpIntensity,ForceMode.Impulse);
            //rigidbody.velocity += Vector3.up * jumpIntensity;
            isGrounded = false;
        }
        Jump();
    }


    void Jump(){   

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1 ) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1 ) * Time.deltaTime;
        }
    }
}
