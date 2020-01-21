using Bolt.AdvancedTutorial;
using UnityEngine;

public class LaserGun : Weapon
{
	public override void DisplayEffects(BoltEntity entity)
	{
		Vector3 pos;
		Quaternion rot;

		PlayerCamera.instance.CalculateCameraAimTransform(entity.GetState<ITicTacState>().Transform.Transform, entity.GetState<ITicTacState>().Pitch, out pos, out rot);

		Ray r = new Ray(pos, rot * Vector3.forward);
		RaycastHit rh;

		if (Physics.Raycast(r, out rh) && impactPrefab)
		{
			var en = rh.transform.GetComponent<BoltEntity>();
			var hit = GameObject.Instantiate(impactPrefab, rh.point, Quaternion.LookRotation(rh.normal)) as GameObject;

			if (en)
			{
				hit.GetComponent<RandomSound>().enabled = false;
			}

			if (trailPrefab)
			{
				var trailGo = GameObject.Instantiate(trailPrefab, muzzleFlash.position, Quaternion.identity) as GameObject;
				var trail = trailGo.GetComponent<LineRenderer>();

				trail.SetPosition(0, muzzleFlash.position);
				trail.SetPosition(1, rh.point);
			}
		}

		GameObject go = (GameObject)GameObject.Instantiate(shellPrefab, shellEjector.position, shellEjector.rotation);
		go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 2, ForceMode.VelocityChange);
		go.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-32f, +32f), Random.Range(-32f, +32f), Random.Range(-32f, +32f)), ForceMode.VelocityChange);

		// show flash
		muzzleFlash.gameObject.SetActive(true);
	}
}