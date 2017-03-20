using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour {
	
	public GameObject cubeBoi;
	public Transform track;
	public int rayDist = 5;
	public bool aware = false;
	//public CharacterController runner;


	// Use this for initialization

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() 
	{
		int rot = 0;
		if (aware) {
			rayDist = 20;
		}

		
		RaycastHit hit;
		Ray ray = new Ray(transform.position, transform.forward);
		//Vector3 fwd = transform.TransformDirection(Vector3.forward);
		//Vector3 bkwd = transform.TransformDirection (Vector3.back);

		cubeBoi.transform.Translate (Vector3.forward * Time.deltaTime);
		if (Physics.Raycast(ray, out hit, 20))
		{
			if (hit.collider.tag != "Player" && aware != true) {
				pat (ray, hit);
				print (aware);
			} 
			else if (hit.collider.tag == "Player") {
				print ("Aware");
				aware = true;
				print (aware);
				chase ();
			} 
			else if (aware && hit.collider.tag != "Player") {
				//cubeBoi.transform.position = Vector3.RotateTowards (transform.position, track.position, 1 * Time.deltaTime, 0.0f);
				//cubeBoi.transform.Rotate (rot += 1, 0, 0);
			}
			
		}




	}

	void pat(Ray r, RaycastHit h)
	{
		
		//Ray is being sent out and return true if ray hits a collider
		//The cube continuously moves until this is true, Then once true will rotate to move the other way
		if (Physics.Raycast(r, out h, 1)) {
			//cubeBoi.transform.Translate (Vector3.back * Time.deltaTime);
			cubeBoi.transform.Rotate(0,90,0);
			//print ("There is something in front of the object!");
		} 
	}

	void chase()
	{
		float speed = 1.0f * Time.deltaTime;
		cubeBoi.transform.position = Vector3.MoveTowards (transform.position, track.position, speed);
	}
}
