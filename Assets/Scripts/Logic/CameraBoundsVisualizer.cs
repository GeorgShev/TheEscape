using Enemy;
using UnityEngine;

namespace Logic
{
    public class CameraBoundsVisualizer : MonoBehaviour
    {
        public float padding = 1f; 
        public float minSpawnDistance = 2f; 
        public float maxSpawnDistance = 20f; 
        public int maxAttempts = 100; 
        public EnemyPool EnemyPool;
        
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main; 
        }

        void OnDrawGizmos()
        {
            if (mainCamera == null) return;

            
            Vector3[] corners = GetCameraCornersAtYZero();

            
            Gizmos.color = Color.red; 
            DrawCameraBounds(corners);

            
            Gizmos.color = Color.blue;
            Vector3 center = GetBoundsCenter(corners);
            DrawFlatCircle( center, maxSpawnDistance);
        }
        
        void DrawFlatCircle(Vector3 center, float radius)
        {
            
            Vector3 previousPoint = center + new Vector3(radius, 0, 0);
            int segments = 16; 

            for (int i = 1; i <= segments; i++)
            {
                float angle = 2 * Mathf.PI * i / segments;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

                
                Gizmos.DrawLine(previousPoint, nextPoint);
                previousPoint = nextPoint;
            }
        }

        Vector3[] GetCameraCornersAtYZero()
        {
            Vector3[] corners = new Vector3[4];

            
            corners[0] = GetWorldPositionAtYZero(new Vector3(0, 0, mainCamera.nearClipPlane)); // Левый нижний
            corners[1] = GetWorldPositionAtYZero(new Vector3(1, 0, mainCamera.nearClipPlane)); // Правый нижний
            corners[2] = GetWorldPositionAtYZero(new Vector3(0, 1, mainCamera.nearClipPlane)); // Левый верхний
            corners[3] = GetWorldPositionAtYZero(new Vector3(1, 1, mainCamera.nearClipPlane)); // Правый верхний

            return corners;
        }

        Vector3 GetWorldPositionAtYZero(Vector3 viewportPoint)
        {
            
            Ray ray = mainCamera.ViewportPointToRay(viewportPoint);

            
            Plane yPlane = new Plane(Vector3.up, Vector3.zero);

            
            if (yPlane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }

            
            return ray.GetPoint(100f); 
        }

        void DrawCameraBounds(Vector3[] corners)
        {
            
            Gizmos.DrawLine(corners[0], corners[1]); 
            Gizmos.DrawLine(corners[1], corners[3]); 
            Gizmos.DrawLine(corners[3], corners[2]); 
            Gizmos.DrawLine(corners[2], corners[0]);
        }

        public Vector3 GetRandomPositionOutsideCamera()
        {
            if (mainCamera == null) return Vector3.zero;

            Vector3 spawnPosition;
            bool positionFound = false;
            int attempts = 0;

            do
            {
                spawnPosition = GenerateRandomPosition();
                positionFound = IsPositionValid(spawnPosition);
                attempts++;
            } while (!positionFound && attempts < maxAttempts);

            return positionFound ? spawnPosition : Vector3.zero;
        }

        Vector3 GenerateRandomPosition()
        {
            
            Vector3[] corners = GetCameraCornersAtYZero();

            
            Vector3 spawnPosition;
            int side = Random.Range(0, 4); 

            switch (side)
            {
                case 0: 
                    spawnPosition = new Vector3(
                        corners[0].x - padding,
                        0,
                        Random.Range(corners[0].z, corners[2].z)
                    );
                    break;
                case 1: 
                    spawnPosition = new Vector3(
                        corners[1].x + padding,
                        0,
                        Random.Range(corners[1].z, corners[3].z)
                    );
                    break;
                case 2: 
                    spawnPosition = new Vector3(
                        Random.Range(corners[0].x, corners[1].x),
                        0,
                        corners[0].z - padding
                    );
                    break;
                case 3: 
                    spawnPosition = new Vector3(
                        Random.Range(corners[2].x, corners[3].x),
                        0,
                        corners[2].z + padding
                    );
                    break;
                default:
                    spawnPosition = Vector3.zero;
                    break;
            }

            return spawnPosition;
        }
        
        Vector3 GetBoundsCenter(Vector3[] corners)
        {
            // Вычисляем среднее арифметическое всех углов
            Vector3 center = Vector3.zero;
            foreach (var corner in corners)
            {
                center += corner;
            }
            center /= corners.Length; // Делим на количество углов

            return center;
        }

        bool IsPositionValid(Vector3 position)
        {
           
            float distanceFromCamera = Vector3.Distance(position, mainCamera.transform.position);
            if (distanceFromCamera > maxSpawnDistance)
            {
                return false;
            }

            
            foreach (var enemy in EnemyPool.enemyPool) 
            {
                if (Vector3.Distance(position, enemy.transform.position) < minSpawnDistance)
                {
                    return false;
                }
            }

            return true;
        }
    }
}