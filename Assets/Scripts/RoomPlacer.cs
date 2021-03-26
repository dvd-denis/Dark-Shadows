using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RoomPlacer : MonoBehaviour
{

    public Room[] RoomsPrfabs;
    public Room StartRoom;
    public static int RoomCount = 12;
    public NavMeshSurface nav;

    private Room[,] spawnedRooms;
    private bool lastRoom;

    public void Start()
    {
        spawnedRooms = new Room[RoomCount, RoomCount];
        spawnedRooms[5, 5] = StartRoom; 

        for (int i = 0; i < RoomCount; i++)
        {
            if (i == RoomCount - 1) lastRoom = true;
            PlaceOneRoom();
            //yield return new WaitForSecondsRealtime(0.7f);
        }
        nav.BuildNavMesh();
    }

    void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int MaxX = spawnedRooms.GetLength(0) - 1;
                int MaxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < MaxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < MaxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Room newRoom;

        if (!lastRoom) newRoom = Instantiate(RoomsPrfabs[Random.Range(0, RoomsPrfabs.Length  - 1)]); 
        else newRoom = Instantiate(RoomsPrfabs[RoomsPrfabs.Length - 1]);


        int limit = 500;

        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 20;
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }
        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int pos)
    {
        int MaxX = spawnedRooms.GetLength(0) - 1;
        int MaxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();
        if (room.doorUp != null && pos.y < MaxY && spawnedRooms[pos.x, pos.y + 1]?.doorUp != null) neighbours.Add(Vector2Int.up);
        if (room.doorDown != null && pos.y > 0 && spawnedRooms[pos.x, pos.y - 1]?.doorDown != null) neighbours.Add(Vector2Int.down);
        if (room.doorRight != null && pos.x < MaxX && spawnedRooms[pos.x + 1, pos.y]?.doorDown != null) neighbours.Add(Vector2Int.right);
        if (room.doorLeft != null && pos.x > 0 && spawnedRooms[pos.x - 1, pos.y]?.doorDown != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[pos.x + selectedDirection.x, pos.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up && selectedRoom.doorDown != null)
        {
            room.doorUp.SetActive(false);
            selectedRoom.doorDown.SetActive(false);
        }
        if (selectedDirection == Vector2Int.down && selectedRoom.doorUp != null)
        {
            room.doorDown.SetActive(false);
            selectedRoom.doorUp.SetActive(false);
        }
        if (selectedDirection == Vector2Int.left && selectedRoom.doorRight != null)
        {
            room.doorLeft.SetActive(false);
            selectedRoom.doorRight.SetActive(false);
        }
        if (selectedDirection == Vector2Int.right && selectedRoom.doorLeft != null)
        {
            room.doorRight.SetActive(false);
            selectedRoom.doorLeft.SetActive(false);
        }
        return true;
    }
}
