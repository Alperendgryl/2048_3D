using UnityEngine;
public class RedZone : MonoBehaviour
{
    public static bool gameEnded = false;
    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();

        if (cube != null)
        {
            if (!cube.isMainCube)
            {
                gameEnded = true;
            }
        }
    }
}
