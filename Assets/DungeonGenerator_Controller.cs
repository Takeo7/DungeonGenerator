using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_Controller : MonoBehaviour
{

    #region Singleton

    static DungeonGenerator_Controller instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    
    [Header("Neded to Generate:")]
    [Tooltip("Arrays of Rooms")]
    [SerializeField]
    GameObject[] one_prefabs;
    [SerializeField]
    GameObject[] two_prefabs;
    [SerializeField]
    GameObject[] three_prefabs;
    [SerializeField]
    GameObject[] four_prefabs;
    [SerializeField]
    GameObject[] end_prefabs;
    [Space]
    [Tooltip("Size of the dungeon in squares^2")]
    [SerializeField]
    int dungeon_size;
    int currentParentRoom;
    int newRoomCount;

    //-----

    GameObject[] tempRooms;
    DungeonGenerator_Room[] tempRoomScript;

    private void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        int length = dungeon_size;

        tempRooms = new GameObject[length];
        tempRoomScript = new DungeonGenerator_Room[length];



        Debug.Log(length);
        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                tempRooms[0] = Instantiate(NewRoom(3));
                tempRoomScript[0] = tempRooms[0].GetComponent<DungeonGenerator_Room>();
                currentParentRoom = 0;
                newRoomCount++;
                Debug.Log("Current Parent Room: " + currentParentRoom + " ------ i: " + i);
            }
            else
            {
                GenerateRooms(tempRoomScript, tempRooms, i, 3);
            }
        }
        for (int i = 0; i < length; i++)
        {
            int doors = tempRoomScript[i].GetDoorsCount();
            
            for (int d = 0; d < doors; d++)
            {
                if (tempRoomScript[i].IsConnected(d) == false)
                {
                    int rand = Random.Range(0, end_prefabs.Length);
                    GameObject tempEnd = Instantiate(end_prefabs[rand], tempRoomScript[i].GetDoor(d).position, tempRoomScript[i].GetDoor(d).rotation,tempRooms[i].transform);
                }
            }
        }
    }

    void GenerateRooms(DungeonGenerator_Room[] _tempRoomScript, GameObject[] _tempRooms, int i, int randNum)
    {
        int rand;

        int doorsLength = _tempRoomScript[currentParentRoom].GetDoorsCount();
        Debug.Log("Door num: " + currentParentRoom + " has " + doorsLength + " doors.");
        for (int d = 0; d < doorsLength; d++)
        {
            if (_tempRoomScript[currentParentRoom].IsConnected(d) == false && newRoomCount<dungeon_size)
            {
                rand = Random.Range(1, randNum+1);
                if (rand == randNum+1)
                {
                    rand = 1;
                }
                _tempRooms[newRoomCount] = Instantiate(NewRoom(rand));
                _tempRoomScript[newRoomCount] = _tempRooms[newRoomCount].GetComponent<DungeonGenerator_Room>();
                Debug.Log("New Room in pos: " + (newRoomCount));

                //Colocar la room
                Debug.Log("Adding Room to Door " + d);
                LocateNewRoom(_tempRooms[currentParentRoom], d, _tempRooms[newRoomCount]);




                _tempRoomScript[currentParentRoom].ConnectDoor(d); //Connect parent door
                Debug.Log("Door: " + d + " of Parent Room connected.");
                _tempRoomScript[newRoomCount].ConnectDoor(0); //Connect first door on new room
                Debug.Log("Door: " + 0 + " of new Room connected.");
                newRoomCount++;
            }
            
        }
        currentParentRoom++;
        Debug.Log("Current Parent Room: " + currentParentRoom + " ------ i: " + i);
    }

    GameObject NewRoom(int i)
    {
        int randomRoom;
        switch (i)
        {
            case 0:
                randomRoom = Random.Range(0, one_prefabs.Length-1);
                return one_prefabs[randomRoom];
            case 1:
                randomRoom = Random.Range(0, two_prefabs.Length-1);
                return two_prefabs[randomRoom];
            case 2:
                randomRoom = Random.Range(0, three_prefabs.Length-1);
                return three_prefabs[randomRoom];
            case 3:
                randomRoom = Random.Range(0, four_prefabs.Length-1);
                return four_prefabs[randomRoom];
            default:
                randomRoom = Random.Range(0, one_prefabs.Length-1);
                return one_prefabs[randomRoom];
        }
        
    }

    void LocateNewRoom(GameObject c_Room,int c_doorNum, GameObject n_Room) //TODO: NO FUNCIONA JODER!!!!
    {
        Debug.Log("ROOM TO FOCUS: " + c_Room);
        Transform c_door = c_Room.GetComponent<DungeonGenerator_Room>().GetDoor(c_doorNum);
        Transform n_door = n_Room.GetComponent<DungeonGenerator_Room>().GetDoor(0);

        Vector3 finalPos;

        if (c_door.position.x > c_Room.transform.position.x)
        {
            finalPos = c_door.position + new Vector3(n_door.localPosition.z, 0);
        }
        else if(c_door.position.x < c_Room.transform.position.x)
        {
            finalPos = c_door.position - new Vector3(n_door.localPosition.z, 0);
        }else if (c_door.position.z > c_Room.transform.position.z)
        {
            finalPos = c_door.position + new Vector3(0, 0, n_door.localPosition.z);
        }
        else
        {
            finalPos = c_door.position - new Vector3(0, 0, n_door.localPosition.z);
        }

        n_Room.transform.position = finalPos;

        //Rotation 
        n_Room.transform.LookAt(c_Room.transform.position);
    }
    

}
