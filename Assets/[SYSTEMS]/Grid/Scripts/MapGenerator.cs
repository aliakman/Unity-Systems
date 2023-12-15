using Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Map[] maps;
        [SerializeField] private int mapIndex;

        [SerializeField] private Transform tilePrefab;
        [SerializeField] private Transform obstaclePrefab;
        [SerializeField] private Transform navmeshFloor;
        [SerializeField] private Transform navmeshMaskPrefab;

        [SerializeField] private Vector2 maxMapSize;

        [Range(0, 1)]
        [SerializeField] private float outlinePercent;

        [SerializeField] private float tileSize;
        private List<Coord> allTileCoords;
        private Queue<Coord> shuffledTileCoords;
        private Queue<Coord> shuffledOpenTileCoords;
        private Transform[,] tileMap;

        private Map currentMap;
        
        private void Start()
        {
            GenerateMap();
        }

        private void OnNewWave(int _waveNumber)
        {
            mapIndex = _waveNumber - 1;
            GenerateMap();
        }

        public void GenerateMap()
        {
            currentMap = maps[mapIndex];
            tileMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];
            System.Random _prng = new System.Random(currentMap.seed);
            GetComponent<BoxCollider>().size = new Vector3(currentMap.mapSize.x * tileSize, .5f, currentMap.mapSize.y * tileSize);

            // Generating coords
            allTileCoords = new List<Coord>();
            for (int x = 0; x < currentMap.mapSize.x; x++){
                for (int y = 0; y < currentMap.mapSize.y; y++)
                    allTileCoords.Add(new Coord(x, y));
            }
            shuffledTileCoords = new Queue<Coord>(Utilities.ShuffledArray<Coord>(allTileCoords.ToArray(), currentMap.seed));

            //Create map holder object
            string _holderName = "Generated Map";
            if (transform.Find(_holderName))
                DestroyImmediate(transform.Find(_holderName).gameObject);

            Transform _mapHolder = new GameObject(_holderName).transform;
            _mapHolder.parent = transform;

            // Spawning tiles
            for (int x = 0; x < currentMap.mapSize.x; x++) {
                for (int y = 0; y < currentMap.mapSize.y; y++) {
                    Vector3 _tilePos = CoordToPosition(x, y);
                    Transform _newTile = Instantiate(tilePrefab, _tilePos, Quaternion.Euler(Vector3.right * 90));
                    _newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
                    _newTile.parent = _mapHolder;
                    tileMap[x, y] = _newTile;
                }
            }

            //Spawning obstacles
            bool[,] _obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];

            int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
            int _currentObstacleCount = 0;
            List<Coord> _allOpenCoords = new List<Coord>(allTileCoords);

            for (int i = 0; i < obstacleCount; i++)
            {
                Coord _randomCoord = GetRandomCoord();
                _obstacleMap[_randomCoord.x, _randomCoord.y] = true;
                _currentObstacleCount++;

                if (_randomCoord != currentMap.mapCentre && MapIsFullyAccessable(_obstacleMap, _currentObstacleCount))
                {
                    float _obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)_prng.NextDouble());
                    Vector3 _obstaclePos = CoordToPosition(_randomCoord.x, _randomCoord.y);

                    Transform _newObstacle = Instantiate(obstaclePrefab, _obstaclePos + Vector3.up * _obstacleHeight / 2f, Quaternion.identity);
                    _newObstacle.parent = _mapHolder;
                    _newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, _obstacleHeight, (1 - outlinePercent) * tileSize);

                    Renderer _obstacleRenderer = _newObstacle.GetComponent<Renderer>();
                    Material _obstacleMaterial = new Material(_obstacleRenderer.sharedMaterial);
                    float _colourPercent = _randomCoord.y / (float)currentMap.mapSize.y;
                    _obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColor, _colourPercent);
                    _obstacleRenderer.sharedMaterial = _obstacleMaterial;

                    _allOpenCoords.Remove(_randomCoord);
                }
                else
                {
                    _obstacleMap[_randomCoord.x, _randomCoord.y] = false;
                    _currentObstacleCount--;
                }
            }

            shuffledOpenTileCoords = new Queue<Coord>(Utilities.ShuffledArray<Coord>(_allOpenCoords.ToArray(), currentMap.seed));

            // Creating the navmesh mask
            Transform _maskLeft = Instantiate(navmeshMaskPrefab, Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity);
            _maskLeft.parent = _mapHolder;
            _maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

            Transform _maskRight = Instantiate(navmeshMaskPrefab, Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity);
            _maskRight.parent = _mapHolder;
            _maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

            Transform _maskTop = Instantiate(navmeshMaskPrefab, Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity);
            _maskTop.parent = _mapHolder;
            _maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

            Transform _maskBottom = Instantiate(navmeshMaskPrefab, Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity);
            _maskBottom.parent = _mapHolder;
            _maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

            navmeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y, .5f) * tileSize;
        
        }

        private bool MapIsFullyAccessable(bool[,] _obstacleMap, int _currentObstacleCount)
        {
            bool[,] _mapFlags = new bool[_obstacleMap.GetLength(0), _obstacleMap.GetLength(1)];
            Queue<Coord> _queue = new Queue<Coord>();
            _queue.Enqueue(currentMap.mapCentre);
            _mapFlags[currentMap.mapCentre.x, currentMap.mapCentre.y] = true;

            int _accessableTileCount = 1;

            while (_queue.Count > 0)
            {
                Coord _tile = _queue.Dequeue();

                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        int _neighbourX = _tile.x + x;
                        int _neighbourY = _tile.y + y;

                        if(x == 0 || y == 0) {
                            if(_neighbourX >= 0 && _neighbourX < _obstacleMap.GetLength(0) && _neighbourY >= 0 && _neighbourY < _obstacleMap.GetLength(1)) {
                                if(!_mapFlags[_neighbourX, _neighbourY] && !_obstacleMap[_neighbourX, _neighbourY]) {
                                    _mapFlags[_neighbourX, _neighbourY] = true;
                                    _queue.Enqueue(new Coord(_neighbourX, _neighbourY));
                                    _accessableTileCount++;
                                }
                            }
                        }
                    }
                }
            }

            int _targetAccessableTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - _currentObstacleCount);
            return _targetAccessableTileCount == _accessableTileCount;
        }

        private Vector3 CoordToPosition(int _x, int _y)
        {
            return new Vector3(-currentMap.mapSize.x / 2f + .5f + _x, 0, -currentMap.mapSize.y / 2f + .5f + _y) * tileSize;
        }

        private Transform GetTileFromPosition(Vector3 _pos)
        {
            int _x = Mathf.RoundToInt(_pos.x / tileSize + (currentMap.mapSize.x - 1) / 2f);
            int _y = Mathf.RoundToInt(_pos.z / tileSize + (currentMap.mapSize.y - 1) / 2f);
            _x = Mathf.Clamp(_x, 0, tileMap.GetLength(0) - 1);
            _y = Mathf.Clamp(_y, 0, tileMap.GetLength(1) - 1);
            return tileMap[_x, _y];
        }

        private Coord GetRandomCoord()
        {
            Coord _randomCoord = shuffledTileCoords.Dequeue();
            shuffledTileCoords.Enqueue(_randomCoord);
            return _randomCoord;
        }

        public Transform GetRandomOpenTile()
        {
            Coord _randomCoord = shuffledOpenTileCoords.Dequeue();
            shuffledTileCoords.Enqueue(_randomCoord);
            return tileMap[_randomCoord.x, _randomCoord.y];
        }

    }

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator == (Coord _c1, Coord _c2)
        {
            return _c1.x == _c2.x && _c1.y == _c2.y;
        }

        public static bool operator != (Coord _c1, Coord _c2)
        {
            return !(_c1 == _c2);
        }
    }

    [System.Serializable]
    public class Map
    {
        public Coord mapSize;
        [Range(0, 1)]
        public float obstaclePercent;
        public int seed;
        public float minObstacleHeight;
        public float maxObstacleHeight;
        public Color foregroundColour;
        public Color backgroundColor;

        public Coord mapCentre
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }

    }

}