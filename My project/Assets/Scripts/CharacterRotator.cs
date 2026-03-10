using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    public float rotationSpeed = 120f;
    private bool isDragging = false;
    private float lastMouseX;
    void Update()
    {
        if (Input.mousePosition.x > Screen.width * 0.33f) return; // only rotate in left panel

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMouseX = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0))
            isDragging = false;

        if (isDragging && Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - lastMouseX;
            transform.Rotate(Vector3.up, -delta * rotationSpeed * Time.deltaTime);
            lastMouseX = Input.mousePosition.x;
        }
    }
}