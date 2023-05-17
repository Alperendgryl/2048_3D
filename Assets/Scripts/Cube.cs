using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int staticID = 0;
    [SerializeField] private TMP_Text[] numbersText;

    [HideInInspector] public int cubeID;
    [HideInInspector] public Color cubeColor;
    [HideInInspector] public int cubeNumber;
    [HideInInspector] public Rigidbody cubeRigidbody;
    [HideInInspector] public bool isMainCube;

    private MeshRenderer cubeMeshRenderer;

    public static TrailRenderer trailRenderer;

    private void Awake()
    {
        cubeID = staticID++;
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        cubeRigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void setColor(Color color)
    {
        cubeColor = color;
        cubeMeshRenderer.material.color = color;
    }

    public void setNumber(int number)
    {
        cubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            numbersText[i].text = number.ToString();
        }
    }
}
