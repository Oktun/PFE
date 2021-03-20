using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class SpawnVfxOnZombie : MonoBehaviour
{
    

    public void SpawnVFX(Transform casterTransform , GameObject particaleObject, Transform playerTransform = null, bool isDead = false)
    {
        var go = particaleObject;
        GameObject partical = Instantiate(go, casterTransform) as GameObject;
        partical.transform.parent = null;
        if (isDead==true)
        {
            partical.transform.position += Vector3.up;
        }
        Destroy(partical, 2f);
    }

}
