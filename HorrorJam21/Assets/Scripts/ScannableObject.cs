using UnityEngine;

public class ScannableObject : MonoBehaviour
{
	[SerializeField] private Material scannedMat;
	private Material defaultMat;

	private Renderer rend;

	private float pulseTimeRemaining = 0f;
	private bool pulseTimerRunning = false;

	private void Awake()
	{
		rend = GetComponent<Renderer>();
		defaultMat = rend.material;
	}

	private void Update()
	{
		if (pulseTimerRunning)
		{
			pulseTimeRemaining -= Time.deltaTime;

			if (pulseTimeRemaining <= 0f)
			{
				pulseTimeRemaining = 0f;
				pulseTimerRunning = false;
				SetDefaultMaterial();
			}
		}
	}

	private void SetScannedMaterial()
	{
		if (scannedMat != null)
		{
			rend.material = scannedMat;
		}
	}

	private void SetDefaultMaterial()
	{
		if (defaultMat != null)
		{
			rend.material = defaultMat;
			Debug.Log("Timer ended. Reverted to default material.");
		}
	}


	public void Pulse()
	{
		pulseTimeRemaining += 5f;

		if(!pulseTimerRunning) {
			pulseTimerRunning = true;
			SetScannedMaterial();
		}
	}
}
