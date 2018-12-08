using UnityEngine;

public class Level1Player : Obj
{
    public Level1 mgr;
    public float maxSpeed = 3f;
    private Vector3 v;
    int dragFingerIndex = -1;
    Vector2 tapPos = Vector2.zero;

    void OnDrag(DragGesture gesture)
    {
        if (mgr.hasFinded)
        {
            return;
        }
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

            var maxV = direction * maxSpeed;
            v = Vector3.Lerp(v, maxV, 0.01f);
            transform.position += v * Time.deltaTime;
        }

        if (gesture.Phase == ContinuousGesturePhase.Ended)
        {
            dragFingerIndex = -1;
            tapPos = Vector2.zero;
            v = Vector3.zero;
        }
    }

}
