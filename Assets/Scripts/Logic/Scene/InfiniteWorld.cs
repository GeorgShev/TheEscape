using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Scene
{
    public class InfiniteWorld : MonoBehaviour
    {
        public List<GameObject> tilePrefabs; 
        public List<GameObject> environmentPrefabs; 
        public float tileSize = 10f; 
        public float bufferZone = 2f; 
        public int environmentPoolSize = 20; 

        private Transform _player; 
        private Dictionary<Vector2Int, GameObject> _activeTiles = new Dictionary<Vector2Int, GameObject>(); 
        private Queue<GameObject> _environmentPool = new Queue<GameObject>(); 
        private Vector2Int _currentCenterTile; 
        private bool _isUpdatingTiles = false;

      

        public void InitPlayer(Transform playerTransform)
        {
            _player = playerTransform;
            
            
            _currentCenterTile = GetTileCoordinate(_player.position);
            
            HashSet<Vector2Int> usedCoordinates = new HashSet<Vector2Int>();
            
            foreach (var tile in tilePrefabs)
            {
                Vector2Int tileCoord = GetTileCoordinate(tile.transform.position);

                if (usedCoordinates.Add(tileCoord))
                {
                    _activeTiles.Add(tileCoord, tile);
                }
            }
            
            
            UpdateWorld(_currentCenterTile);
            
            for (int i = 0; i < environmentPoolSize; i++)
            {
                GameObject envPrefab = environmentPrefabs[Random.Range(0, environmentPrefabs.Count)];
                GameObject env = Instantiate(envPrefab, Vector3.zero, Quaternion.identity);
                env.SetActive(false);
                _environmentPool.Enqueue(env);
            }
        }

        private void Update()
        {
            if (_player && !_isUpdatingTiles)
            {
                Vector2Int playerTile = GetTileCoordinate(_player.position);
                Vector3 playerPos = _player.position;
                Vector3 centerTilePos = new Vector3(
                    (_currentCenterTile.x + 0.5f) * tileSize,
                    0,
                    (_currentCenterTile.y + 0.5f) * tileSize
                );

                
                if (Vector3.Distance(playerPos, centerTilePos) > bufferZone)
                {
                    Vector2Int newCenterTile = _currentCenterTile;

                    if (playerTile.x > _currentCenterTile.x) newCenterTile.x++;
                    else if (playerTile.x < _currentCenterTile.x) newCenterTile.x--;

                    if (playerTile.y > _currentCenterTile.y) newCenterTile.y++;
                    else if (playerTile.y < _currentCenterTile.y) newCenterTile.y--;

                    StartCoroutine(UpdateWorldWithDelay(newCenterTile));
                }
            }
        }

        private IEnumerator UpdateWorldWithDelay(Vector2Int newCenterTile)
        {
            _isUpdatingTiles = true;
            yield return new WaitForSeconds(0.1f); 
            UpdateWorld(newCenterTile);
            _isUpdatingTiles = false;
        }

        private void UpdateWorld(Vector2Int newCenterTile)
        {
            
            Dictionary<Vector2Int, GameObject> oldTiles = new Dictionary<Vector2Int, GameObject>(_activeTiles);
            _activeTiles.Clear();

            
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector2Int tileCoord = new Vector2Int(newCenterTile.x + x, newCenterTile.y + y);
                    
                    
                    if (oldTiles.TryGetValue(tileCoord, out GameObject existingTile))
                    {
                        _activeTiles.Add(tileCoord, existingTile);
                    }
                    else
                    {
                        
                        foreach (var tile in tilePrefabs)
                        {
                            if (!oldTiles.ContainsValue(tile) && !_activeTiles.ContainsValue(tile))
                            {
                                MoveTileToPosition(tile, tileCoord);
                                break;
                            }
                        }
                    }
                }
            }

            _currentCenterTile = newCenterTile;
        }

        private void MoveTileToPosition(GameObject tile, Vector2Int tileCoord)
        {
            
            Vector3 tilePosition = new Vector3(
                tileCoord.x * tileSize,
                0,
                tileCoord.y * tileSize
            );
            
            
            List<Transform> children = new List<Transform>();
            foreach (Transform child in tile.transform)
            {
                children.Add(child);
            }

            
            tile.transform.position = tilePosition;

            
            if (tile.transform.childCount == 0 && _environmentPool.Count > 0)
            {
                SpawnEnvironment(tilePosition, tile);
            }

            _activeTiles.Add(tileCoord, tile);
        }

        private void SpawnEnvironment(Vector3 tilePosition, GameObject parentTile)
        {
            if (_environmentPool.Count > 0)
            {
                int objectsToSpawn = Random.Range(1, 4);
                int attempts = 0;
                int maxAttempts = 5;

                for (int i = 0; i < objectsToSpawn; i++)
                {
                    if (_environmentPool.Count == 0) break;

                    GameObject env = _environmentPool.Dequeue();
                    env.SetActive(true);

                   
                    Vector3 localPosition = FindValidPosition(tilePosition, parentTile.transform, maxAttempts);

                    if (localPosition != Vector3.zero)
                    {
                        env.transform.SetParent(parentTile.transform);
                        env.transform.localPosition = localPosition;
                    }
                    else
                    {
                        env.SetActive(false);
                        _environmentPool.Enqueue(env);
                    }
                }
            }
        }
        
        private Vector3 FindValidPosition(Vector3 tilePosition, Transform parentTransform, int maxAttempts)
        {
            float minDistanceBetweenObjects = 25f; 
            float objectRadius = 1f; 
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                
                Vector3 localPosition = new Vector3(
                    Random.Range(-tileSize / 2 + objectRadius, tileSize / 2 - objectRadius),
                    0,
                    Random.Range(-tileSize / 2 + objectRadius, tileSize / 2 - objectRadius)
                );

                
                Vector3 worldPosition = parentTransform.TransformPoint(localPosition);

                
                if (IsPositionValid(worldPosition, minDistanceBetweenObjects))
                {
                    return localPosition;
                }

                attempts++;
            }

            return Vector3.zero;
        }
        private bool IsPositionValid(Vector3 position, float minDistance)
        {
           
            foreach (var tile in _activeTiles.Values)
            {
                foreach (Transform child in tile.transform)
                {
                    if (Vector3.Distance(position, child.position) < minDistance)
                    {
                        return false; 
                    }
                }
            }

            return true; 
        }

        private Vector2Int GetTileCoordinate(Vector3 position)
        {
            return new Vector2Int(
                Mathf.FloorToInt(position.x / tileSize),
                Mathf.FloorToInt(position.z / tileSize)
            );
        }
    }
}