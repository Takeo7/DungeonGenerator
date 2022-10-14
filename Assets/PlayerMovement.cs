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
    CharacterController controller;
    [SerializeField]
    Transform cam;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Rigidbody rb;

    [Space]
    [SerializeField]
    float speed;
    [SerializeField]
    bool IsWalking = false;
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
            if (IsWalking == false)
            {
                IsWalking = !IsWalking;
                anim.SetBool("IsWalking", IsWalking);
            }
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Debug.Log("moveDir: " + moveDir + "\nnovedir.normalized: " + moveDir.normalized);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else if (IsWalking == true)
        {
            IsWalking = !IsWalking;
            anim.SetBool("IsWalking", IsWalking);
        }
    }
}
