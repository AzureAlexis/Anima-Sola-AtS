using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance = null;
    private string followMode;
    private Transform followTarget;
    private Vector2 followPoint;

    void Start()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        if (followTarget != null)
            pos = followTarget.position;
        else if (followPoint != null)
            pos = followPoint;

        switch (followMode)
        {
            case "smooth":
                transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * 5);
                break;
            case "snap":
                transform.position = pos;
                break;
        }
    }

    public void SetFollow(Transform obj, string mode = "snap")
    {
        followMode = mode;
        followTarget = obj;
        followPoint = transform.position;
    }

    public void SetFollow(Vector2 point, string mode = "snap")
    {
        followMode = mode;
        followPoint = point;
        followTarget = null;
    }
    public void Move(Vector2 point)
    {
        transform.position += (Vector3)point;
    }
}
