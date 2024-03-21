using UnityEngine;

namespace UzGameDev
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float rotateSpeed = 600;
        
        private Vector3 moveDirection;
        private Rigidbody body;
        
        private Animator animator;
        private readonly int Velocity = Animator.StringToHash("Velocity");
        
        //===============================================================
        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }
        //===============================================================
        private void FixedUpdate()
        {
            var inputX = Input.GetAxis("Horizontal");
            var inputY = Input.GetAxis("Vertical");
            moveDirection = new Vector3(inputX * moveSpeed, body.velocity.y, inputY * moveSpeed);
            
            body.velocity = moveDirection;
        }
        //===============================================================
        private void Update()
        {
            var animVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
            animator.SetFloat(Velocity, animVelocity.sqrMagnitude);
            
            if(moveDirection is {x:0, z:0 }) return;
            var rotateDirection = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotateSpeed * Time.deltaTime);
        }
        //===============================================================
    }
}