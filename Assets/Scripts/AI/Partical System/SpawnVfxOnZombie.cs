using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class SpawnVfxOnZombie : MonoBehaviour
{
    

    public void SpawnVFX(Transform casterTransform , GameObject particaleObject, Transform playerTransform = null)
    {
        var go = particaleObject;
        GameObject partical = Instantiate(go, casterTransform) as GameObject;
        //partical.transform.position = pos;
        partical.transform.parent = null;
        partical.transform.position += Vector3.up;
        if (playerTransform)
        {
            //partical.transform.LookAt(playerTransform);
            //partical.transform.rotation = new Quaternion(0, -90, 0, 0);
        }
        Destroy(partical, 2f);
    }

}
