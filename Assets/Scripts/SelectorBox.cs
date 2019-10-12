using System.Collections.Generic;
using UnityEngine;

public class SelectorBox : MonoBehaviour
{
    private Vector3 startPoint = Vector3.zero;
    private Vector3 endPoint = Vector3.zero;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            startPoint = getMousePos();
        }
        if(Input.GetMouseButton(0)) {
            endPoint = getMousePos();
            lineRenderer.enabled = true;
            updateBox();
        }
        if(Input.GetMouseButtonUp(0)) {
            lineRenderer.enabled = false;
        }
    }

    private void updateBox()
    {
        Vector3[] positions = new Vector3[5];
        positions[0] = startPoint;
        positions[1] = new Vector3(startPoint.x, endPoint.y, 0f);
        positions[2] = endPoint;
        positions[3] = new Vector3(endPoint.x, startPoint.y, 0f);
        positions[4] = startPoint;
        lineRenderer.SetPositions(positions);
    }

    private Vector3 getMousePos() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.Scale(new Vector3(1f, 1f, 0f));
        return pos;
    }
}
