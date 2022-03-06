using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private BoxCollider2D Box;

    void Start()
    {
        var aspect = (float)Screen.width / Screen.height;
        var orthoSize = Cam.orthographicSize;

        var width = 2.0f * orthoSize * aspect;
        var height = 2.0f * orthoSize;

        Box.size = new Vector2(width, height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("EXIT");

        //Debug.Log(collider.transform.position.y + " ? " + Box.bounds.max.y);

        if (collider.transform.position.x < Box.bounds.min.x)
        {
            collider.transform.position = new Vector3(Box.bounds.max.x, collider.transform.position.y, collider.transform.position.z);
        }
        else if (collider.transform.position.x > Box.bounds.max.x)
        {
            collider.transform.position = new Vector3(Box.bounds.min.x, collider.transform.position.y, collider.transform.position.z);
        }

        if (collider.transform.position.y < Box.bounds.min.y)
        {
            collider.transform.position = new Vector3(collider.transform.position.x, Box.bounds.max.y, collider.transform.position.z);
        }
        else if (collider.transform.position.y > Box.bounds.max.y)
        {
            collider.transform.position = new Vector3(collider.transform.position.x, Box.bounds.min.y, collider.transform.position.z);
        }
    }
}
