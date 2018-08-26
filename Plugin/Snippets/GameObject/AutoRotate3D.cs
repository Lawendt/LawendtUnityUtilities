using UnityEngine;

namespace LUT.Snippets
{
    public class AutoRotate3D : MonoBehaviour
    {
        public Vector3 torque;

        void Update()
        {
            transform.Rotate(torque * Time.deltaTime);
        }
    }
}
