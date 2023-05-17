using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;

    [SerializeField] private float cubeSpawnTime = 0.3f;

    [SerializeField] private TouchSlider touchSlider;
    [SerializeField] private Cube mainCube;
    [SerializeField] private GameObject slider;

    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    private void Start()
    {
        SpawnCube();
        canMove = true;
        eventListener();
    }

    private void Update()   
    {
        if (isPointerDown)
            mainCube.transform.position = Vector3.Lerp(
               mainCube.transform.position,
               cubePos,
               moveSpeed * Time.deltaTime); //change the position of mainCube.
    }

    private void eventListener()
    {
        // Listen slider events
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }
    private void OnDestroy()
    {
        // Remove listeners
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX; //limit the X axis of main cube.
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            slider.SetActive(false); // After throwing, player cannot change the position of the cube until new cube is spawned. (slider deactivated)
            FX.Instance.PlayThrowFX();
            isPointerDown = false;
            canMove = false;

            mainCube.GetComponent<Rigidbody>().AddForce(Vector3.forward * pushForce, ForceMode.Impulse); // Add force to the cube.

            Invoke("SpawnNewCube", cubeSpawnTime); //create a new cube
        }
    }

    private void SpawnNewCube()
    {
        slider.SetActive(true); //(slider activated)
        mainCube.isMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.isMainCube = true;

        // reset cubePos variable
        cubePos = mainCube.transform.position;
    }


}
