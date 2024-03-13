using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomIDGenerator
{
    public static string GenerateRandomID()
    {
        return "[" + Random.Range(10000, 99999) + "]";
    }
}
