using UnityEngine;

public class Level1Player : MonoBehaviour {
    public Level1 mgr;
    public float maxSpeed = 3f;
    private Vector3 v;
    int dragFingerIndex = -1;
    Vector2 tapPos = Vector2.zero;

    void OnDrag(DragGesture gesture)
    {
        FingerGestures.Finger finger = gesture.Fingers[0];

        if (dragFingerIndex == -1 && gesture.Phase == ContinuousGesturePhase.Started)
        {
            dragFingerIndex = finger.Index;
        }
        if (dragFingerIndex != finger.Index)
        {
            return;
        }

        if (gesture.Phase == ContinuousGesturePhase.Started)
        {
            tapPos = gesture.Position;
        }

        if (gesture.Phase == ContinuousGesturePhase.Updated)
        {
            var direction = (gesture.Position - tapPos).normalized;
            MovePlayer(direction);
        }

        if (gesture.Phase == ContinuousGesturePhase.Ended)
        {
            dragFingerIndex = -1;
            tapPos = Vector2.zero;
        }
    }

    void MovePlayer(Vector3 direction)
    {
        var maxV = direction * maxSpeed;
        v = Vector3.Lerp(v, maxV, 0.01f);
        transform.position += v * Time.deltaTime;
    }
}
