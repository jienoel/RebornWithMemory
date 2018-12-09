using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        [ContextMenu( "Print offset" )]
        void PrintOffset()
        {
            Debug.Log( target.position - transform.position );
        }

        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private Vector3 m_offset;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = transform.position;
            m_offset = (target.position - transform.position);
            transform.parent = null;
        }


        public bool isMoving = false;
        // Update is called once per frame
        private void Update()
        {
            if( target.position.x - transform.position.x - m_offset.x < lookAheadFactor )
            {
                isMoving = false;
                return;
            }
            
            isMoving = true;
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition - m_offset).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position - m_offset + m_LookAheadPos + Vector3.forward*m_offset.z;
            aheadTargetPos.y = transform.position.y;
            aheadTargetPos.z = transform.position.z;
            aheadTargetPos.x = target.position.x - m_offset.x;
           // Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
            Vector3 newPos = Vector3.Lerp( transform.position, aheadTargetPos, damping * Time.deltaTime );

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
