using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour
{
    //[SerializeField] GameObject OjbectToFollow;

    public  Transform objectToFollow;
    public Transform[] ojbectFollowing;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < ojbectFollowing.Length; i++) 
        {
            //objectToFollow.transform.position = ojbectFollowing[i].transform.position;
            //objectToFollow.transform.rotation = ojbectFollowing[i].transform.rotation;

            ojbectFollowing[i].transform.rotation = objectToFollow.transform.rotation;
            ojbectFollowing[i].transform.position = objectToFollow.transform.position;
        }

    }


}
