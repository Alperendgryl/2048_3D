using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    // Singleton class
    public static CubeSpawner Instance;

    Queue<Cube> cubesQueue = new Queue<Cube>();

    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; // 8192
    private int maxPower = 13; // (2^12)

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        InitializeCubesQueue();
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
            AddCubeToQueue();
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();

        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("Not Enough Cubes !");
                return null;
            }
        }

        Cube cube = cubesQueue.Dequeue(); //remove from queue
        cube.transform.position = position; //default pos
        cube.setNumber(number);
        cube.setColor(GetColor(number));
        cube.gameObject.SetActive(true);

        return cube;
    }

    public Cube SpawnRandom()
    {
        return Spawn(RandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }
    
    public int RandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 7)); //Between 2^1 - 2^6 = 2 - 64
    }

    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1]; // ex. ((log16 / log2) - 1) --> (4/1) - 1 = 3
    }
}
