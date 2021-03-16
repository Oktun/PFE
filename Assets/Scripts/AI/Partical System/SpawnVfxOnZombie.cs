using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class SpawnVfxOnZombie : MonoBehaviour
{

    public void SpawnVFX(Vector3 pos , GameObject particaleObject)
    {
        var go = particaleObject;
        GameObject partical = Instantiate(go, pos, Quaternion.identity) as GameObject;
        Destroy(partical, 2f);
    }

}
