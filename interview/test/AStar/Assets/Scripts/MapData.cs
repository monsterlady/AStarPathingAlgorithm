using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    private int[,] _tiles;
    private int mapX = 8;
    private int mapY = 8;
    public MapData()
    {
        _tiles = new int[mapX,mapY];
        //Desert type 0
        _tiles[0, 7] = 0;
        _tiles[1, 7] = 0;
        _tiles[0, 6] = 0;
        _tiles[1, 6] = 0;
        _tiles[2, 6] = 0;
        _tiles[0, 5] = 0;
        _tiles[2, 5] = 0;
        _tiles[6, 5] = 0;
        _tiles[0, 4] = 0;
        _tiles[6, 4] = 0;
        _tiles[7, 4] = 0;
        _tiles[7, 3] = 0;
        _tiles[2, 2] = 0;
        //Mountain Type 1
        _tiles[0, 0] = 1;
        _tiles[1, 0] = 1;
        _tiles[2, 0] = 1;
        _tiles[2, 1] = 1;
        _tiles[4, 3] = 1;
        _tiles[4, 4] = 1;
        _tiles[5, 5] = 1;
        _tiles[6, 6] = 1;
        _tiles[5, 7] = 1;
        _tiles[6, 7] = 1;
        //Grass Type 2
        _tiles[4, 0] = 2;
        _tiles[6, 0] = 2;
        _tiles[0, 1] = 2;
        _tiles[5, 1] = 2;
        _tiles[3, 2] = 2;
        _tiles[6, 2] = 2;
        _tiles[6, 3] = 2;
        _tiles[3, 5] = 2;
        _tiles[4, 5] = 2;
        _tiles[7, 5] = 2;
        _tiles[4, 6] = 2;
        _tiles[2, 7] = 2;
        _tiles[4, 7] = 2;
        _tiles[7, 7] = 2;
        //Forest Type 3
        _tiles[3, 0] = 3;
        _tiles[5, 0] = 3;
        _tiles[1, 1] = 3;
        _tiles[3, 1] = 3;
        _tiles[4, 1] = 3;
        _tiles[0, 2] = 3;
        _tiles[1, 2] = 3;
        _tiles[3, 3] = 3;
        _tiles[5, 3] = 3;
        _tiles[1, 4] = 3;
        _tiles[3, 4] = 3;
        _tiles[5, 4] = 3;
        _tiles[1, 5] = 3;
        _tiles[3, 6] = 3;
        _tiles[5, 6] = 3;
        _tiles[7, 6] = 3;
        //Water Type 4
        _tiles[7, 0] = 4;
        _tiles[6, 1] = 4;
        _tiles[7, 1] = 4;
        _tiles[4, 2] = 4;
        _tiles[5, 2] = 4;
        _tiles[7, 2] = 4;
        _tiles[0, 3] = 4;
        _tiles[1, 3] = 4;
        _tiles[2, 3] = 4;
        _tiles[2, 4] = 4;
        _tiles[3, 7] = 4;
    }

    public int[,] GetData()
    {
        return _tiles;
    }
    
    public float GetCostDaysByTilesType(int x,int y)
    {
        int type = _tiles[x, y];
        float costdays = 0;
        switch (type)
        {
            case 0:
                costdays = 5f;
                break;
            case 1:
                costdays = 10f;
                break;
            case 2:
                costdays = 1f;
                break;
            case 3:
                costdays = 3f;
                break;
            case 4 :
                costdays = -1f;
                break;
        }
        return costdays;
    }
}
