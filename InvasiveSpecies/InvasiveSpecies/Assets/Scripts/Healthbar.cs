using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {

	Critter c;

	// Use this for initialization
	void Start () {
		c = transform.parent.GetComponent<Critter>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(1, Mathf.Clamp01(c.health/100f), 0);
	}
}
