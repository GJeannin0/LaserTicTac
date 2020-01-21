using UnityEngine;



	public class AutoDisable : MonoBehaviour
	{
		float remaining = 0;

		[SerializeField]
		float time = 3;

		void OnEnable ()
		{
			remaining = time;
		}

		void FixedUpdate ()
		{
			if ((remaining -= Time.fixedDeltaTime) <= 0) {
				gameObject.SetActive (false);
			}
		}
	}

