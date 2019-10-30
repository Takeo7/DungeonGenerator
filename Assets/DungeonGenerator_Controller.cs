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

    [SerializeField]
    [Header("Neded to Generate:")]
    [Tooltip("Array of Rooms")]
    GameObject[] one_prefabs;
    GameObject[] two_prefabs;
    GameObject[] three_prefabs;
    GameObject[] four_prefabs;
    [Space]
    [SerializeField]
    [Tooltip("Size of the dungeon in squares^2")]
    int dungeon_size;

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


        for (int i = 1; i < length; i++)
        {
            if (i < 4)
            {
                if (i < 3)
                {
                    if (i < 2)
                    {
                        rand = 0;
                        
                    }
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

                        //Colocar la room



                        tempRoomScript[i].ConnectDoor(0);
                        i++;
                    }
                }

                currentParentRoom++;

            }

            

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
