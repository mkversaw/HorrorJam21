using UnityEngine;

public class Fuse : MonoBehaviour
{
	[SerializeField] private FuseBox fuseBox;
	[SerializeField] private int whichFuse;
	[SerializeField] Material offMat;
	[SerializeField] Material onMat;
	[SerializeField] Renderer lightRenderer;
	bool isEnabled = false;

	public void SetOn()
	{
		if(!isEnabled)
		{
			return;
		}

		// play SFX
		// send signal to FuseBox to check progress
		fuseBox.NextFuse(whichFuse);
		transform.eulerAngles = new Vector3(180f, 0f, 0f);
		lightRenderer.material = onMat;
		isEnabled = false;
	}

	public void SetOff()
	{
		transform.eulerAngles = new Vector3(0f, 0f, 0f);
		lightRenderer.material = offMat;
		isEnabled = true;
	}

}
