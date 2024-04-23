using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] particles;

    public void PlayVFX(VfxType vfxType, Vector3 createPosition)
    {
        GameObject particle = Instantiate(particles[(int)vfxType], createPosition, Quaternion.identity);

        particle.GetComponent<ParticleSystem>().Play();

        Destroy(particle, 1f);
    }
    public enum VfxType
    {
        boxSpawn,
        houseSpawn,
        merge
    }

}
