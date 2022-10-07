using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float velocity;
    public float rotateSpeed;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("Horizontal");
            player.transform.position += player.transform.forward * velocity * Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") >= 0)
        {
            player.transform.position += player.transform.right * velocity;
        }
        if (Input.GetAxis("Vertical") <= 0)
        {
            player.transform.position += player.transform.right * -1 * velocity;
        }
        
    }
}
