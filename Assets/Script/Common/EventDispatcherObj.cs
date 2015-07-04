using UnityEngine;
using System.Collections;

public class EventDispatcherObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		EventDispatcher.Instance().OnTick();
	}
}
