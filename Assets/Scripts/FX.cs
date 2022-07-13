using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;
    [SerializeField] private AudioSource[] cubeSfx;

    public static FX Instance; //singleton

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCubeFX(Vector3 position, Color color)
    {
        cubeExplosionFX.transform.position = position;
        color.a = 1;
        cubeExplosionFX.startColor = color;
        cubeExplosionFX.Play();

        cubeSfx[1].Play(); // concatenated
    }

    public void PlayMiss()
    {
        if (cubeSfx != null) // !concatenated = 0 ,concatenated = 1
        {
            if (!cubeSfx[0].isPlaying)
            {
                cubeSfx[0].Play();
            } 
            else cubeSfx[0].Stop();
        }

    }

    public void PlayThrowFX()
    {
        if (cubeSfx != null && !cubeSfx[2].isPlaying) 
        {
            cubeSfx[2].Play();
        }
    }
}
