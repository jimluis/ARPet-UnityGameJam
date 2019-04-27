using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class ObjectPlacement : MonoBehaviour
{

    //Interacting with the ARSessionOrigin obj is a key to allowing us
    //interact with the world around us
    private ARSessionOrigin arOrigin;
    //
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public GameObject placementIndicator;
    public GameObject objectToPlace;



    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }


    void Update()
    {
        //On every frame update we want to check the world around us
        //find out where the camera is pointing and identify if there is  
        //a position where we want to place a virtual obj

        // In oder to represent that position in space we use a Pose Obj
        //A Pose object is a data structure that describes the position 
        // and rotation of a 3D point 

        UpdatePlacementPose();
        UpdatePlacementIndicator();

        print("GameObject.Find(\"Player\"): " + GameObject.Find("Player"));

        if(placementPoseIsValid && 
           Input.touchCount > 0 && 
           Input.GetTouch(0).phase == TouchPhase.Began
         && GameObject.Find("chick Variant") == null)
        {
            PlaceObject();
        }

    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }

        else
            placementIndicator.SetActive(false);
    }


    private void UpdatePlacementPose()
    {
        //To derermine the screen center we use the below code
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        //After arOrigin.Raycast is called, we either end up with an empty hit list
        //meaning that there is no flat plane infront of the camera currently,
        //or we will have a list with one or more items representing the planes that 
        //are infront of the camera
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        //To keep track of the plane count
        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            //we set the placementPose, by looking up the first
            //hit result in our hits array
            placementPose = hits[0].pose;

            //Instead of using the rotation that comes with the pose
            //we want to calculate a new rotation based on the camera direction 

            //This will be a vector that acts sort of an arrow that describes the direction
            //that the camera is faces along the x,y,z axis 
            var cameraForward = Camera.current.transform.forward;

            //Since we do not care about how much the camera is pointing to the sky,
            //or towards the ground we only care about its bearing, we create the below variable cameraBearing
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;


            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}