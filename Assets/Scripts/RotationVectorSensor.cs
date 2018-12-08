using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class RotationVectorSensor : MonoBehaviour {
	public Vector3 bounds = new Vector3(10.0f, 10.0f, 0.0f);
	public SpriteRenderer tex;
	private bool _enable_gyro = false;
	
	void Reset()
	{
		tex = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		_enable_gyro = EnableGyro();
	}

	private bool EnableGyro()
	{
		if (SystemInfo.supportsGyroscope)
		{
			_enable_gyro = Input.gyro.enabled;
			if (!_enable_gyro)
			{
				Input.gyro.enabled = true;
			}
			return true;
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_enable_gyro)
			projection();
	}

	private void projection()
	{
		Vector3 normalize_angle = new Vector3(-Mathf.Sin(Input.gyro.attitude.eulerAngles.x), -Mathf.Sin(Input.gyro.attitude.eulerAngles.y), 0.0f);
		transform.localPosition = Vector3.Scale(normalize_angle, bounds);
	}
}