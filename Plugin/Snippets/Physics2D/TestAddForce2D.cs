#if UNITY_EDITOR
using UnityEngine;

namespace LUT.Snippets.Physics2D
{

	public class TestAddForce2D : MonoBehaviour
	{
		[System.Serializable]
		public enum SetupType
		{
			Vector,
			AngleAndMagnitude
		}

		public bool drawGizmos = true;

		[AutoFind(typeof(Rigidbody2D))]
		public new Rigidbody2D rigidbody2D;

		[Header("Setup")]
		public ForceMode2D forceMode = ForceMode2D.Force;
		public SetupType setupType;
		[ShowIf("setupType", SetupType.Vector)]
		public Vector3 force = Vector3.zero;

		[ShowIf("setupType", SetupType.AngleAndMagnitude)]
		public float angle;
		[ShowIf("setupType", SetupType.AngleAndMagnitude)]
		public float magnitude;

		[LUT.Button(LUT.ButtonMode.EnabledInPlayMode)]

		public void Execute()
		{
			if (setupType == SetupType.AngleAndMagnitude)
			{
				rigidbody2D.AddForce(new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude, Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude), forceMode);
			}
			else
			{
				rigidbody2D.AddForce(force, forceMode);
			}
		}

		public void OnDrawGizmos()
		{
			if (rigidbody2D == null || !drawGizmos)
			{
				return;
			}

			Gizmos.color = Color.white;
			if (setupType == SetupType.AngleAndMagnitude)
			{
				Gizmos.DrawLine(rigidbody2D.transform.position, rigidbody2D.transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude, Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude));
			}
			else
			{
				Gizmos.DrawLine(rigidbody2D.transform.position, rigidbody2D.transform.position + force * 0.2f);
			}
		}

		public bool CheckType(SetupType type)
		{
			return setupType == type;
		}
	}

}
#endif