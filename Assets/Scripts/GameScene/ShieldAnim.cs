using UnityEngine;
using System.Collections;

public class ShieldAnim : MonoBehaviour {

	
	void Update () {
		transform.Rotate (Vector3.right * Time.deltaTime * 150);
	}
}
