using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_Room : MonoBehaviour
{
    [Header("Info of the room")]
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
}
