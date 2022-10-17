using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_Controller : MonoBehaviour
{

    #region Singleton

    public static DungeonGenerator_Controller instance;
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

    [Header("Debug")]
    public bool showCubes = true;
    [Space]
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
    [SerializeField]
    GameObject endFloor;
    [Space]
    [Tooltip("Initial Room door's")]
    [SerializeField]
    [Range(1,4)]
    int initialRoomDoors = 1;
    [Space]
    [Tooltip("Size of the dungeon in squares^2")]
    [SerializeField]
    int dungeon_size;
    int currentParentRoom;
    int newRoomCount;
    [Space]
    [SerializeField]
    NavMeshController_ nvmc;

    //-----

    GameObject[] tempRooms;
    DungeonGenerator_Room[] tempRoomScript;

    private void Start()
    {
        //GenerateDungeon();
        StartCoroutine("CreateDungeonCoroutine");
    }

    //void GenerateDungeon()
    //{
    //    int length = dungeon_size;

    //    tempRooms = new GameObject[length];
    //    tempRoomScript = new DungeonGenerator_Room[length];



    //    for (int i = 0; i < length; i++)
    //    {
    //        if (i == 0)
    //        {
    //            tempRooms[0] = Instantiate(NewRoom(3));
    //            tempRoomScript[0] = tempRooms[0].GetComponent<DungeonGenerator_Room>();
    //            currentParentRoom = 0;
    //            tempRoomScript[0].EnableColliders();
    //            newRoomCount++;
    //        }
    //        else
    //        {
    //            //GenerateRooms(tempRoomScript, tempRooms, i, 3);
    //            StartCoroutine("CreateDungeonCoroutine",i);
                
    //        }
    //    }
    //    for (int i = 0; i < length; i++)
    //    {
    //        if (tempRooms[i] != null)
    //        {
    //            int doors = tempRoomScript[i].GetDoorsCount();

    //            for (int d = 0; d < doors; d++)
    //            {
    //                if (tempRoomScript[i].IsConnected(d) == false)
    //                {
    //                    int rand = Random.Range(0, end_prefabs.Length);
    //                    GameObject tempEnd = Instantiate(end_prefabs[rand], tempRoomScript[i].GetDoor(d).position, tempRoomScript[i].GetDoor(d).rotation, tempRooms[i].transform);
    //                }
    //            }
    //        }           
    //    }
    //}

    //void GenerateRooms(DungeonGenerator_Room[] _tempRoomScript, GameObject[] _tempRooms, int i, int randNum)
    //{
    //    int rand;

    //    int doorsLength = _tempRoomScript[currentParentRoom].GetDoorsCount();
    //    for (int d = 0; d < doorsLength; d++)
    //    {
    //        if (_tempRoomScript[currentParentRoom].IsConnected(d) == false && newRoomCount<dungeon_size)
    //        {
    //            rand = Random.Range(1, randNum+1); // poner 1 en rand num
    //            if (rand == randNum+1)
    //            {
    //                rand = 1;
    //            }
    //            _tempRooms[newRoomCount] = Instantiate(NewRoom(rand));
    //            _tempRoomScript[newRoomCount] = _tempRooms[newRoomCount].GetComponent<DungeonGenerator_Room>();
    //            _tempRoomScript[newRoomCount].SetID(newRoomCount);
    //            _tempRoomScript[newRoomCount].SetParentRoomID(currentParentRoom);
    //            //Colocar la room
    //            LocateNewRoom(_tempRooms[currentParentRoom], d, _tempRooms[newRoomCount],i);




                
    //            //_tempRoomScript[newRoomCount].ConnectDoor(0); //Connect first door on new room
                
    //        }
            
    //    }
    //    if (tempRooms[currentParentRoom + 1] != null)
    //    {
    //        currentParentRoom++;
    //    }
    //}

    GameObject NewRoom(int i)
    {
        int randomRoom;
        switch (i)
        {
            case 0:

                if (one_prefabs.Length != 0)
                {
                    randomRoom = Random.Range(0, one_prefabs.Length);
                    if (randomRoom == one_prefabs.Length)
                    {
                        randomRoom = 0;
                    }
                    return one_prefabs[randomRoom];
                }
                else
                {
                    return two_prefabs[0];
                }
                
            case 1:
                if (two_prefabs.Length != 0)
                {
                    randomRoom = Random.Range(0, two_prefabs.Length);
                    if (randomRoom == two_prefabs.Length)
                    {
                        randomRoom = 0;
                    }
                    return two_prefabs[randomRoom];
                }
                else
                {
                    return two_prefabs[0];
                }
                
            case 2:
                if (three_prefabs.Length != 0)
                {
                    randomRoom = Random.Range(0, three_prefabs.Length);
                    if (randomRoom == three_prefabs.Length)
                    {
                        randomRoom = 0;
                    }
                    return three_prefabs[randomRoom];
                }
                else
                {
                    return two_prefabs[0];
                }
                
            case 3:
                if (four_prefabs.Length != 0)
                {
                    randomRoom = Random.Range(0, four_prefabs.Length);
                    if (randomRoom == four_prefabs.Length)
                    {
                        randomRoom = 0;
                    }
                    return four_prefabs[randomRoom];
                }
                else
                {
                    return two_prefabs[0];
                }
               
            default:
                return two_prefabs[0];
        }
        
    }

    void LocateNewRoom(GameObject c_Room,int c_doorNum, GameObject n_Room, int i) //TODO: Collider check
    {
        Transform c_door = c_Room.GetComponent<DungeonGenerator_Room>().GetDoor(c_doorNum);
        Transform n_door = n_Room.GetComponent<DungeonGenerator_Room>().GetDoor(0);
        Vector3 finalPos = Vector3.zero;

        finalPos = c_door.position + new Vector3(n_door.position.x * (c_door.forward.x * 1.1f), 0 , n_door.position.z * (c_door.forward.z * 1.1f));

        n_Room.transform.position = finalPos;

        //Rotation 
        //n_Room.transform.LookAt(c_Room.transform.position);

        n_Room.transform.LookAt(c_door.transform.position);
        n_Room.transform.position += c_door.position - n_door.position;

        
        if( n_Room.GetComponent<DungeonGenerator_Room>().CheckCollidersWithRooms()){
            Destroy(n_Room.gameObject);

        }

    }
    
    public int GetNewRoomCount()
    {
        return newRoomCount;
    }
    public void SetNewRoomCount() { newRoomCount++; }


    IEnumerator CreateDungeonCoroutine()
    {
        int i = 0;

        tempRooms = new GameObject[dungeon_size];
        tempRoomScript = new DungeonGenerator_Room[dungeon_size];

        while (i < dungeon_size)
        {
            if (tempRooms[i] == null)
            {
                if (i == 0)
                {
                    tempRooms[0] = Instantiate(NewRoom(initialRoomDoors-1));
                    tempRoomScript[0] = tempRooms[0].GetComponent<DungeonGenerator_Room>();
                    currentParentRoom = 0;
                    newRoomCount++;
                }
                else
                {
                    yield return GenerateRoomsCoroutine(i);
                }
            }           
            i++;
            
        }


        // Pone End en las puertas restantes
        for (int j = 0; j < dungeon_size; j++)
        {
            if (tempRooms[j] != null)
            {
                int doors = tempRoomScript[j].GetDoorsCount();

                for (int d = 0; d < doors; d++)
                {
                    if (tempRoomScript[j].IsConnected(d) == false)
                    {
                        int rand = Random.Range(0, end_prefabs.Length);
                        GameObject tempEnd = Instantiate(end_prefabs[rand], tempRoomScript[j].GetDoor(d).position, tempRoomScript[j].GetDoor(d).rotation);//, tempRooms[j].transform);
                    }
                    else
                    {
                        GameObject tempendfloor = Instantiate(endFloor, tempRoomScript[j].GetDoor(d).position, tempRoomScript[j].GetDoor(d).rotation);
                    }
                    
                }
            }
        }

        //Finished dungeon
        nvmc.BakeNavMesh();
        Debug.Log("Navmesh build");
        Debug.Log(newRoomCount);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
    }
    IEnumerator GenerateRoomsCoroutine(int i)
    {
        int rand;
        Debug.Log("CurrentParentRoomCount: " + currentParentRoom);
        int doorsLength = tempRoomScript[currentParentRoom].GetDoorsCount();
        int d = 0;

        while (d < doorsLength)
        {
            if (tempRoomScript[currentParentRoom].IsConnected(d) == false && newRoomCount < dungeon_size)
            {
                rand = Random.Range(1, 4); // poner 1 en rand num
                if (rand == 4)
                {
                    rand = 1;
                }
                tempRooms[newRoomCount] = Instantiate(NewRoom(rand));
                tempRoomScript[newRoomCount] = tempRooms[newRoomCount].GetComponent<DungeonGenerator_Room>();
                tempRoomScript[newRoomCount].SetID(newRoomCount);
                tempRoomScript[newRoomCount].SetParentRoomID(currentParentRoom);
                //Colocar la room
                LocateNewRoom(tempRooms[currentParentRoom], d, tempRooms[newRoomCount], i);

                //_tempRoomScript[newRoomCount].ConnectDoor(0); //Connect first door on new room
                //newRoomCount++;
            }
            //yield return new WaitForSeconds(0.2f);
            yield return new WaitForEndOfFrame();
            if (tempRooms.Length-1 >= newRoomCount)
            {
                if (tempRooms[newRoomCount] != null)
                {
                    tempRoomScript[newRoomCount].ConnectDoor(0);
                    tempRoomScript[currentParentRoom].ConnectDoor(d); //Connect parent door
                    newRoomCount++;
                    
                }
            }
            d++;
        }
        if (tempRooms[currentParentRoom+1] != null)
        {
            currentParentRoom++;
        }
        
    }

}
