using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // ตัวที่กล้องจะตาม (เช่น Player หรือ Box)
    public Vector3 offset = new Vector3(0, 5, -10); // ระยะห่างจาก target
    public float smoothSpeed = 5f;   // ความเร็วในการตาม (ยิ่งมากยิ่งเร็ว)

    void LateUpdate()
    {
        if (target == null) return;

        
        Vector3 desiredPosition = target.position + offset;

        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        
        transform.position = smoothedPosition;

        
        transform.LookAt(target);
    }
}
