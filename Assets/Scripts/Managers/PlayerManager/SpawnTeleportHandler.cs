using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleportHandler : MonoBehaviour
{
    public Transform[] spawnPosition;    // Start is called before the first frame update
    void Start()   // will 
    {
        int randomIndex = Random.Range(0, spawnPosition.Length);
        Transform randomTransform = spawnPosition[randomIndex];
        this.transform.position = randomTransform.position;
    }

}
