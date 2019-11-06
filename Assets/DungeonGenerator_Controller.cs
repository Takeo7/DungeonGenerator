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
    [Space]
    [Tooltip("Size of the dungeon in squares^2")]
    [SerializeField]
    int dungeon_size;
    int currentParentRoom;

    private void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        int length = dungeon_size;
        

        GameObject[] tempRooms = new GameObject[length];
        DungeonGenerator_Room[] tempRoomScript = new DungeonGenerator_Room[length];

        

        Debug.Log(length);
        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                tempRooms[0] = Instantiate(NewRoom(3));
                tempRoomScript[0] = tempRooms[0].GetComponent<DungeonGenerator_Room>();
                currentParentRoom = 0;
            }else if (i > length-4)
            {
                if (i > length - 3)
                {
                    if (i > length - 2)
                    {
                        GenerateRooms(tempRoomScript, tempRooms, i, 0);
                    }
                    else
                    {
                        GenerateRooms(tempRoomScript, tempRooms, i, 1);
                    }
                }
                else
                {
                    GenerateRooms(tempRoomScript, tempRooms, i, 2);
                }
            }
            else
            {
                GenerateRooms(tempRoomScript, tempRooms, i, 3);
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
            if (_tempRoomScript[currentParentRoom].IsConnected(d) == false)
            {
                rand = Random.Range(0, randNum+1);
                if (rand == randNum+1)
                {
                    rand = 0;
                }
                _tempRooms[i + d] = Instantiate(NewRoom(rand));
                _tempRoomScript[i + d] = _tempRooms[i + d].GetComponent<DungeonGenerator_Room>();

                //Colocar la room
                Debug.Log("Adding Room to Door " + d);
                NewRoomTransf(_tempRooms[currentParentRoom], d, _tempRooms[i + d]);





                _tempRoomScript[i + d].ConnectDoor(0);
            }
        }
        currentParentRoom++;
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

    void NewRoomTransf(GameObject c_Room,int c_doorNum, GameObject n_Room)
    {
        
        Transform c_door = c_Room.GetComponent<DungeonGenerator_Room>().GetDoor(c_doorNum);
        Transform n_door = n_Room.GetComponent<DungeonGenerator_Room>().GetDoor(0);

        //Position
        Vector3 doorPos = c_door.position;
        n_Room.GetComponent<DungeonGenerator_Room>().ConnectDoor(0);

        Vector3 finalPos = doorPos + n_door.localPosition;

        //Rotation
        Quaternion c_doorRot = c_door.rotation;

        Vector3 eulerRot = c_doorRot.eulerAngles + Quaternion.Inverse(c_doorRot).eulerAngles;

        Quaternion finalRot = Quaternion.Euler(eulerRot);

        n_Room.transform.position = finalPos;
        n_Room.transform.rotation = finalRot; 
    }
    

}
