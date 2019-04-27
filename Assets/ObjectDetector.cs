using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isPetOnTheScene = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("chick"))
        {//ObjectPlacement.placementPose
            isPetOnTheScene = true;
           // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //   sphere.transform.position = new Vector3(0, 1.5f, 0);
           // Instantiate(sphere, ObjectPlacement.placementPose.position, ObjectPlacement.placementPose.rotation);
        }
    }
}
