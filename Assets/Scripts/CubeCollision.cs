using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    Cube cube;
    public float minTorque;
    public float maxTorque;
    public static int currentScore;

    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        if (otherCube != null && cube.cubeID > otherCube.cubeID) // check if concatenated with other cubes
        {
            if (cube.cubeNumber == otherCube.cubeNumber) // check if both cubes have same number
            {
                //Destroy the two cubes:
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);

                Vector3 contactPoint = collision.contacts[0].point;

                if (otherCube.cubeNumber < CubeSpawner.Instance.maxCubeNumber) // check if cubes' numbers less than maxTorque number in CubeSpawner:
                {

                    Cube newCube = CubeSpawner.Instance.Spawn(cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f); // spawn a new cube
                    float pushForce = Random.Range(1f, 5f); // add some torque: //push the new cube up and forward:
                    newCube.GetComponent<Rigidbody>().AddForce(Vector3.one * pushForce, ForceMode.Impulse);

                    currentScore += cube.cubeNumber * 2;

                    float randomValue = Random.Range(minTorque, maxTorque); // add some torque
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.GetComponent<Rigidbody>().AddTorque(randomDirection);
                }


                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f); // the explosion should affect surrounded cubes too:
                float explosionForce = 400f;
                float explosionRadius = 1.5f;

                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                }

                FX.Instance.PlayCubeFX(contactPoint, cube.cubeColor);
                
            }
            //else
            //{
            //    FX.Instance.PlayMiss(); //Not concatenated
            //}
        }
    }
}
