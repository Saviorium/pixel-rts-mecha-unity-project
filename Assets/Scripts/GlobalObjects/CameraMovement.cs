using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float zoomPerScroll = 2f; // more -> faster scroll, 1 -> no scroll, 1.1 -> slow scroll
    private new Camera camera;

    void Start() {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate ()
    {
        float horizontal_x = Input.GetAxis("Horizontal");
        float vertical_z = Input.GetAxis("Vertical");
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll != 0) {
            ZoomTo(getMousePos(), scroll);
        }
        gameObject.transform.position = new Vector3( gameObject.transform.position.x + horizontal_x, 
                                                     gameObject.transform.position.y - vertical_z ,
                                                     -10);
    }

    private void ZoomTo(Vector3 toPoint, float scroll) {
        // unity scrolls 0.1 by default
        Vector3 oldPos = gameObject.transform.position;
        float zoom = Mathf.Pow(zoomPerScroll, -scroll);
        Vector3 newPos = oldPos + ((toPoint - oldPos) * (1 - zoom));
        CenterScreen(newPos);
        camera.orthographicSize = Mathf.Max(camera.orthographicSize * zoom, 0.1f); // fixme: glitched UI if max zoomed at position far from center
    }

    private void CenterScreen(Vector3 pos) {
        gameObject.transform.position = pos;
    }

    private Vector3 getMousePos() { //TODO: utils class?
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.Scale(new Vector3(1f, 1f, 0f));
        return pos;
    }
}
