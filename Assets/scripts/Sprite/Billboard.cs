﻿using UnityEngine;


public class Billboard : MonoBehaviour
{
    void LateUpdate() 
    {
       //transform.LookAt(Camera.main.transform.position, Vector3.up);
	   transform.rotation =  Camera.main.transform.rotation;
    }
}