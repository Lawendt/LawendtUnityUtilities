using UnityEngine;

namespace LUT.Snippets
{
    public class AutoRotate3DFixedUpdate : MonoBehaviour
    {
        public Vector3 torque;

        void FixedUpdate()
        {
            transform.Rotate(torque * Time.fixedDeltaTime);
        }
    }
}
