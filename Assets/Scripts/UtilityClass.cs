using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityClass
{
    public static T GetRandomFromList<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }

}
