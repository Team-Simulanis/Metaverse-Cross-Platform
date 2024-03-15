using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleportHandler : MonoBehaviour
{
    public Transform[] spawnPosition;    // Start is called before the first frame update

    private void Start() 
    {
        var randomIndex = Random.Range(0, spawnPosition.Length);
        var randomTransform = spawnPosition[randomIndex];
        transform.position = randomTransform.position;
    }
}
