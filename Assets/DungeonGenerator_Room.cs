using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_Room : MonoBehaviour
{
    [Header("Info of the room")]
    [Tooltip("Doors")]
    [SerializeField]
    Transform[] doors;
    bool[] doorsConnected;
    bool isFullConnected;
    [Space]
    [Tooltip("Room Size")]
    [SerializeField]
    byte size;

    public Transform[] GetDoors()
    {
        return doors;
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
}
