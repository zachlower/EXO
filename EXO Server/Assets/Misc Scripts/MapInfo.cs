using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo {
    // Room Info
    public static int MAX_MONSTER = 3;
    public Room[,] rooms;
    public Room startRoom;
    public Room endRoom;
    public enum MapSize
    {
        MAP_SMALL,
        MAP_MEDIUM,
        MAP_LARGE
    };
    int currRow, currCol;
    int rows, cols;
    public MapInfo(MapSize size)
    {
        rows = 3; cols = 3;
        rooms = new Room[rows,cols];
        
        rooms[0, 0] = new NavRoom();
        rooms[0, 1] = new NavRoom();
        rooms[0, 2] = new TrapRoom();

        rooms[1, 1] = new CombatRoom();

        rooms[2, 1] = new NavRoom();
        rooms[2, 0] = new TrapRoom();
        rooms[2, 2] = new NavRoom();

        currRow = 0; currCol = 0;
        startRoom = rooms[currRow, currCol];
        
        endRoom = rooms[2, 2];
    }

    public byte getAdjacentRooms() {
        byte b = 0;
        //bits 1,2,4,8 encode l,r,u,d
        if (currCol - 1 >= 0)
        {
            if (rooms[currRow,currCol-1] != null) b = (byte)(b | 1);
        }
        if (currCol + 1 < cols)
        {
            if (rooms[currRow, currCol+1] != null) b = (byte)(b | 2);
        }
        if (currRow - 1 >= 0)
        {
            if (rooms[currRow-1, currCol] != null) b = (byte)(b | 4);
        }
        if (currRow + 1 < rows)
        {
            if (rooms[currRow+1, currCol] != null) b = (byte)(b | 8);
        }
        return b;
    }

    public Room moveInDir(GameController.Direction dir) {
        switch (dir) {
            case GameController.Direction.Left:
                currCol--;
                break;
            case GameController.Direction.Right:
                currCol++;
                break;
            case GameController.Direction.Up:
                currRow--;
                break;
            case GameController.Direction.Down:
                currRow++;
                break;
        }
        return rooms[currRow, currCol];
    }
}
