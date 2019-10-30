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

    private void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        int length = dungeon_size;
        int rand;

        GameObject[] tempRooms = new GameObject[length];
        DungeonGenerator_Room[] tempRoomScript = new DungeonGenerator_Room[length];
        int currentParentRoom;
        

        tempRooms[0] = Instantiate(NewRoom(3));
        tempRoomScript[0] = tempRooms[0].GetComponent<DungeonGenerator_Room>();
        currentParentRoom = 0;

        Debug.Log(length);
        for (int i = 1; i < length; i++)
        {
            if (i > length-4)
            {
                if (i > length - 3)
                {
                    if (i > length - 2)
                    {
                        int doorsLength = tempRoomScript[currentParentRoom].GetDoorsCount();
                        for (int d = 0; d < doorsLength; d++)
                        {
                            if (tempRoomScript[currentParentRoom].IsConnected(d) == false)
                            {
                                rand = 0;
                                tempRooms[i] = Instantiate(NewRoom(rand));
                                tempRoomScript[i] = tempRooms[i].GetComponent<DungeonGenerator_Room>();

                                //Colocar la room
                                Vector3 finalPos;




                                tempRoomScript[i].ConnectDoor(0);
                                i++;
                            }
                        }

                        currentParentRoom++;
                    }
                    else
                    {
                        int doorsLength = tempRoomScript[currentParentRoom].GetDoorsCount();
                        for (int d = 0; d < doorsLength; d++)
                        {
                            if (tempRoomScript[currentParentRoom].IsConnected(d) == false)
                            {
                                rand = 1;
                                tempRooms[i] = Instantiate(NewRoom(rand));
                                tempRoomScript[i] = tempRooms[i].GetComponent<DungeonGenerator_Room>();

                                //Colocar la room
                                Vector3 finalPos;




                                tempRoomScript[i].ConnectDoor(0);
                                i++;
                            }
                        }

                        currentParentRoom++;
                    }
                }
                else
                {
                    int doorsLength = tempRoomScript[currentParentRoom].GetDoorsCount();
                    for (int d = 0; d < doorsLength; d++)
                    {
                        if (tempRoomScript[currentParentRoom].IsConnected(d) == false)
                        {
                            rand = 0;
                            tempRooms[i] = Instantiate(NewRoom(rand));
                            tempRoomScript[i] = tempRooms[i].GetComponent<DungeonGenerator_Room>();

                            //Colocar la room
                            




                            tempRoomScript[i].ConnectDoor(0);
                            i++;
                        }
                    }

                    currentParentRoom++;
                }
            }
            else
            {
                int doorsLength = tempRoomScript[currentParentRoom].GetDoorsCount();
                for (int d = 0; d < doorsLength; d++)
                {
                    if (tempRoomScript[currentParentRoom].IsConnected(d) == false)
                    {
                        rand = Random.Range(0, 3);
                        tempRooms[i] = Instantiate(NewRoom(rand));
                        tempRoomScript[i] = tempRooms[i].GetComponent<DungeonGenerator_Room>();

                        //Colocar la room
                        Vector3 finalPos;

                        


                        tempRoomScript[i].ConnectDoor(0);
                        i++;
                    }
                }

                currentParentRoom++;

            }
            Debug.Log(i);
        }
    }

    GameObject NewRoom(int i)
    {
        int randomRoom;
        switch (i)
        {
            case 0:
                randomRoom = Random.Range(0, one_prefabs.Length);
                return one_prefabs[randomRoom];
            case 1:
                randomRoom = Random.Range(0, two_prefabs.Length);
                return two_prefabs[randomRoom];
            case 2:
                randomRoom = Random.Range(0, three_prefabs.Length);
                return three_prefabs[randomRoom];
            case 3:
                randomRoom = Random.Range(0, four_prefabs.Length);
                return four_prefabs[randomRoom];
            default:
                randomRoom = Random.Range(0, one_prefabs.Length);
                return one_prefabs[randomRoom];
        }
        
    }

}
