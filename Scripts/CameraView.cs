using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour 
{

    [SerializeField]
    private float speed = 0;

    private float xMax;
    private float yMin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {

        GetInput();
		
	}

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax),0, -10);
    }

    public void SetLimits(Vector3 maxTile)
    {
        Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - p.x;
        yMin = maxTile.y - p.y;
    }
}
