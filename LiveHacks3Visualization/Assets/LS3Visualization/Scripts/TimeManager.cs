using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	public UserCollection userCollection;
	public float timeToKill = 0.5f;

	private float lastCheckTime;

	// Use this for initialization
	void Start () {
		lastCheckTime = timeToKill;
	}
	
	// Update is called once per frame
	void Update () {
		Dictionary<string, User> allUsers = userCollection.allUsers;

		if (Time.time - lastCheckTime >= timeToKill) {
			lastCheckTime = Time.time;
			//Debug.Log ("Time to check active users!");
			foreach(string key in allUsers.Keys) {
				User user = allUsers[key];
				if (Time.time - user.lastTickTime >= timeToKill) {
					user.userVisObj.SetActive (false);
				} else {
					user.userVisObj.SetActive (true);
				}
			}
		}
			
		foreach(string key in allUsers.Keys) {
			User user = allUsers[key];
			Vector3 viewPos = Camera.main.WorldToViewportPoint (user.userVisObj.transform.position);
			viewPos.x = Mathf.Clamp01 (viewPos.x);
			viewPos.y = Mathf.Clamp01 (viewPos.y);
			user.userVisObj.transform.position = Camera.main.ViewportToWorldPoint (viewPos);
		}
	}
}
