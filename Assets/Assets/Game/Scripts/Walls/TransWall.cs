using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransWall : MonoBehaviour {

	private int layerMask = 1 << 2;
	private List<Transform> hidden;

	public GameObject player;
	
	void Start() {
		hidden = new List<Transform> ();
	}

	void Update () {
		Vector3 direction = player.transform.position - transform.position;
		RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity, layerMask);
		Debug.DrawRay (transform.position, direction, Color.red);
 
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			Transform currentHit = hit.transform;
 
			if (!hidden.Contains (currentHit)) {
				hidden.Add (currentHit);
				Renderer rend = hit.transform.GetComponent<Renderer>();
 
				if (rend) {
					rend.material.shader = Shader.Find("Transparent/Diffuse");
					Color tempColor = rend.material.color;
					tempColor.a = 0.1F;
					rend.material.color = tempColor;
				}
			}
		}
		
		for (int i = 0; i < hidden.Count; i++) {
			bool isHit = false; 
			for (int j = 0; j < hits.Length; j++) {
				if (hidden [i] == hits [j].transform) {
					isHit = true; 
					break;
				}
			}
 
			if (!isHit) {
				Renderer rend = hidden[i].transform.GetComponent<Renderer>();
				rend.material.shader = Shader.Find("Standard (Specular setup)");
				hidden.RemoveAt(i);
			}
		}
 
	}

}
