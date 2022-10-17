using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Space]
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform cam;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    NavMeshAgent nma;

    [Space]
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float runSpeed;
    [SerializeField]
    bool IsWalking = false;
    [SerializeField]
    bool IsRunning = false;
    [SerializeField]
    bool IsJumping = false;
    [SerializeField]
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (IsRunning == false)
                {
                    IsRunning = !IsRunning;
                    anim.SetBool("IsRunning", IsRunning);

                }

                float targetAngle_r = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle_r = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle_r, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle_r, 0f);

                Vector3 moveDir_r = Quaternion.Euler(0f, targetAngle_r, 0f) * Vector3.forward;
                Debug.Log("runDir: " + moveDir_r + "\nrundir.normalized: " + moveDir_r.normalized);
                nma.Move(moveDir_r.normalized * runSpeed * Time.deltaTime);
            }
            else
            {
                if (IsWalking == false)
                {
                    IsWalking = !IsWalking;
                    anim.SetBool("IsWalking", IsWalking);
                }
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                Debug.Log("moveDir: " + moveDir + "\nnovedir.normalized: " + moveDir.normalized);
                nma.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
            }
            
        }
        else if (IsWalking == true)
        {
            IsWalking = !IsWalking;
            anim.SetBool("IsWalking", IsWalking);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = !IsRunning;
            anim.SetBool("IsRunning", IsRunning);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("IsJumping");
        }


    }
}
