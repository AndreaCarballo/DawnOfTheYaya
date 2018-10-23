using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba11 : MonoBehaviour {
    public GameObject player;
    public GameObject PosicionCamara;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        player.transform.position = Vector3.Lerp(player.transform.position, PosicionCamara.transform.position, 0.1f);
    }
}
