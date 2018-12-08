using UnityEngine;

public class Level5Player : Obj
{
    public Level5 mgr;
    public float maxSpeed = 3f;
    int dragFingerIndex = -1;
    Vector2 tapPos = Vector2.zero;
    
    private Rigidbody2D rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void OnDrag(DragGesture gesture)
    {
        if (mgr.hasFinded)
        {
            rb.velocity = Vector3.zero;
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
            rb.velocity = Vector3.Lerp(rb.velocity, maxV, 0.01f);
        }

        if (gesture.Phase == ContinuousGesturePhase.Ended)
        {
            dragFingerIndex = -1;
            tapPos = Vector2.zero;
            rb.velocity = Vector3.zero;
        }
    }
}
