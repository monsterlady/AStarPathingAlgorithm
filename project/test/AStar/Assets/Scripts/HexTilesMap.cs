using Pathing;
using System.Collections.Generic;
using UnityEngine;
public class HexTilesMap : MonoBehaviour
{
    
    public TileType[] tileTypes;
    
    int mapX = 8;
    int mapY = 8;
    GameObject _startPoint,_endPoint;
    Color _starPointColor,_endPointColor,_tempColor;
    int _startPositionX, _startPositionY,_endPositionX, _endPositionY;
    int[,] _tiles; 
    Node[,] _nodes;
    GameObject[,] _objects;
    MapData _mapData;

    IList<IAStarNode> _lastPath;
    List<Color> _lastPathColor;

    // Start is called before the first frame update
    void Start()
    {
        HexMapData();
        GenarateMapVisual();
        AddNeighborsToEachNode();
        //CheckNodeStatus();
    } 
    void HexMapData()
    {
        _mapData = new MapData();
        _tiles = _mapData.GetData();
    }

    void GenarateMapVisual() {
        _nodes = new Node[mapX,mapY];
        _objects = new GameObject[mapX, mapY];
        for (int x = 0; x < mapX; x++) {
            for (int y = 0; y < mapY; y++) { 
                _nodes[x, y] = new Node(x, y, _mapData.GetCostDaysByTilesType(x, y));
                TileType tt = tileTypes[_tiles[x, y]];
                
                Vector3 vec = y % 2 == 0 ? 
                    new Vector3(x, 0, y*0.75f) :
                    new Vector3(x - 0.5f , 0, y*0.75f);
                GameObject go = Instantiate(tt.tileVisualPrefab, vec, Quaternion.identity);

                if (_tiles[x, y] != 4) {
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.map = this;
                    ct.x = x;
                    ct.y = y;
                }
                _objects[x, y] = go;
            }
        }
    }
    void AddNeighborsToEachNode() {
        int[] dx = new int[] { -1, 1,  0,  1, 0, 1};
        int[] dy = new int[] {  0, 0, -1, -1, 1, 1};
        for (int x = 0; x < mapX; x++) {
            for (int y = 0; y < mapY; y++) {
                for (int i = 0; i < dx.Length; i++) {
                    int nx = x + dx[i];
                    int ny = y + dy[i];
                    if (i >= 2 && y % 2 == 1) {
                        nx -= 1;
                    }
                    if (nx < 0 || nx >= mapX || ny < 0 || ny >= mapY) {
                        continue;
                    }
                    if (_nodes[nx, ny].GetCostDays() > 0) {
                        _nodes[x, y].AddNeighborToList(_nodes[nx, ny]);
                    }
                }
            }
        }

        Debug.Log(_nodes[3, 0]);
        foreach(var node in _nodes[0, 0].Neighbours) {
            Debug.Log("neighbour: " + ((Node)node).ToString());
        }
    }

    void CheckNodeStatus()
    {
        foreach (var node in _nodes)
        {
            Debug.Log(node.ToString());
        }
    }
    public void SetStartPoint(GameObject clickedObject,int x,int y)
    {
        if (_endPoint != null && clickedObject == _endPoint)
        {
            return;
        }

        //如果第一个是空的
        if(_startPoint == null){
            _startPoint = clickedObject;
            _startPositionX = x;
            _startPositionY = y;
            _starPointColor = _startPoint.GetComponent<Renderer>().material.color;
            _startPoint.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            if(_startPositionX == x && _startPositionY == y && _startPoint == clickedObject)
            {
                //恢复颜色,如果是已经设置过的位置
                _startPoint.GetComponent<Renderer>().material.color = _starPointColor;
                _startPoint = null;
                _startPositionX = -1;
                _startPositionY = -1;
                return;
            }
            else// 更换位置
            {
                //恢复颜色
                _startPoint.GetComponent<Renderer>().material.color = _starPointColor;
                //重新分配对象
                _startPoint = clickedObject;
                _startPositionX = x;
                _startPositionY = y;
                _starPointColor = _startPoint.GetComponent<Renderer>().material.color;
                _startPoint.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        
        Debug.Log("StartPoint postion : " + _startPositionX + ", " + _startPositionY);
    }
    public void SetEndPoint(GameObject transformGameObject, int x, int y)
    {
        if (_startPoint != null && transformGameObject == _startPoint)
        {
            return;
        }

        if (_endPoint == null)
        {
            _endPoint = transformGameObject;
            _endPositionX = x;
            _endPositionY = y;
            _endPointColor = _endPoint.GetComponent<Renderer>().material.color;
            _endPoint.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            if ((_endPositionX == x && _endPositionY == y) || _endPoint == transformGameObject)
            {
                _endPoint.GetComponent<Renderer>().material.color = _endPointColor;
                _endPoint = null;
                _endPositionX = -1;
                _endPositionY = -1;
                return;
            }
            else
            {
                _endPoint.GetComponent<Renderer>().material.color = _endPointColor;
                _endPoint = transformGameObject;
                _endPositionX = x;
                _endPositionY = y;
                _endPointColor = _endPoint.GetComponent<Renderer>().material.color;
                _endPoint.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        
        Debug.Log("EndPoint postion : " + _endPositionX + ", " + _endPositionY);
    }

    public void GeneratePath() {
        if (_lastPath != null) {
            for (int i = 1; i < _lastPath.Count - 1; ++i) {
                int x = ((Node)_lastPath[i]).GetX();
                int y = ((Node)_lastPath[i]).GetY();
                _objects[x, y].GetComponent<Renderer>().material.color = _lastPathColor[i - 1];
            }
        }

        if (_startPoint == null || _endPoint == null) {
            return;
        }

        Node start = _nodes[_startPositionX, _startPositionY];
        Node end = _nodes[_endPositionX, _endPositionY];
        IList<IAStarNode> path = AStar.GetPath(start, end);
        if (path == null) {
            return;
        }

        _lastPath = path;
        _lastPathColor = new List<Color>();
        for (int i = 1; i < path.Count - 1; ++i) {
            int x = ((Node)path[i]).GetX();
            int y = ((Node)path[i]).GetY();
            _lastPathColor.Add(_objects[x, y].GetComponent<Renderer>().material.color);
            _objects[x, y].GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
