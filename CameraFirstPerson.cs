using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFirstPerson : MonoBehaviour {


    public Camera MainCamera;
    public float horizontalspeed;
    public float verticalspeed;
    float h;
    float v;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        h = horizontalspeed * Input.GetAxis("Mouse X");
        v = verticalspeed * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);
        MainCamera.transform.Rotate(-v, 0, 0);


            if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 0.1f);
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -0.1f);
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(-0.1f, 0, 0);
                }
                else
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(0.1f, 0, 0);
                    }
                }
            }
        }
    


}
}
