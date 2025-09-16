using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    public static CubePool Instance { get; private set; }

    [SerializeField] private GameObject cubePrefab; // Prefab for the cube
    [SerializeField] private int poolSize = 20; // Initial number of cubes in the pool

    private Queue<GameObject> pool = new Queue<GameObject>(); // Queue to store available cubes

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Pre-instantiate cubes and add them to the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.SetActive(false);
            pool.Enqueue(cube);
        }
    }

    /// <summary>
    /// Gets a cube from the pool and spawns it at the given position and rotation.
    /// </summary>
    /// <param name="position">World position to place the cube.</param>
    /// <param name="rotation">Rotation to apply to the cube.</param>
    /// <returns>The activated Cube GameObject.</returns>
    public GameObject GetCube(Vector3 position, Quaternion rotation)
    {
        GameObject cube;

        if (pool.Count > 0)
        {
            cube = pool.Dequeue();
            cube.SetActive(true);
        }
        else
        {
            cube = Instantiate(cubePrefab);
        }

        cube.transform.SetPositionAndRotation(position, rotation);

        var controller = cube.GetComponent<CubeController>();
        controller.Init(); 

        return cube;
    }

    /// <summary>
    /// Deactivates the cube and returns it to the pool.
    /// </summary>
    /// <param name="cube">The cube GameObject to return.</param>
    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        pool.Enqueue(cube);
    }
}