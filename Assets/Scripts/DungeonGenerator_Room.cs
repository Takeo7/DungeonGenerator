using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator_Room : MonoBehaviour
{
    [Header("Info of the room")]
    [Tooltip("Room ID")]
    [SerializeField]
    int ID;
    [SerializeField]
    int ParentRoomID;
    [Tooltip("Doors")]
    [SerializeField]
    Transform[] doors;
    [SerializeField]
    bool[] doorsConnected;
    bool isFullConnected;
    [Space]
    [Tooltip("Room Size")]
    [SerializeField]
    byte size;
    [SerializeField]
    Vector3 bounds;
    [SerializeField]
    Vector3 boundsAdd;
    [SerializeField]
    Collider[] cols;

    void Awake()
    {
        int length = doors.Length;
        doorsConnected = new bool[length];
        for (int i = 0; i < length; i++)
        {
            doorsConnected[i] = false;
        }
        
    }

    public Transform GetDoor(int i)
    {
        return doors[i];
    }

    public int GetDoorsCount()
    {
        return doors.Length;
    }

    public bool IsConnected(int i)
    {
        return doorsConnected[i];
    }

    public void ConnectDoor(int i)
    {
        doorsConnected[i] = true;        
    }

    public bool IsFullConnected()
    {
        return isFullConnected;
    }
    public void SetID(int id)
    {
        ID = id;
    }
    public int GetID() { return ID; }

    public void SetParentRoomID(int id)
    {
       ParentRoomID = id;
    }

    public bool CheckCollidersWithRooms(){

        Collider[] hits = Physics.OverlapBox(gameObject.transform.position+boundsAdd, bounds/2, Quaternion.identity);

        if (hits.Length > 0)
        {

            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i].CompareTag("Room"))
                {
                    return true;
                }
            }


        }
        return false;

    }


    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        if (DungeonGenerator_Controller.instance.showCubes == true)
        {
            Gizmos.DrawWireCube(transform.position + boundsAdd, bounds);
        }
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (DungeonGenerator_Controller.instance.GetNewRoomCount() == ID)
        {
            if (other.CompareTag("Room"))
            {
                Destroy(gameObject);
            }
        }       
    }*/
}
