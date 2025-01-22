using UnityEngine;

public class LaneMovement : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform;
    [SerializeField] float offset = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offset;
        transform.position = cameraPos;
    }
}
