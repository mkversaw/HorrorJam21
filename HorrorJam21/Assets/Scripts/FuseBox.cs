using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
	[SerializeField] Fuse[] fuses;

	private int[] fuseOrder = new int[6];
	private int currentFuse = 0;

	bool isBroken = false;


	// 0-5 in random order with no duplicates
	private void GenerateRandomFuseOrder()
	{
		List<int> values = new List<int> { 0, 1, 2, 3, 4, 5 };
		for (int i = 0; i < fuseOrder.Length; i++)
		{
			int index = Random.Range(0, values.Count);
			fuseOrder[i] = values[index];
			values.RemoveAt(index);
		}
	}

	private void Awake()
	{
		SetFusesToGoodState();
	}
	void SetFusesToGoodState()
	{
		// disable all the switches to be not flippable
		// flip all switches to up state.
		// set all lights green
		// re-enable sonar
		// play fixed SFX

		currentFuse = 0;
		isBroken = false;
	}

	void SetFusesToBrokenState()
	{
		// enable all the switches to be flippable
		// flip all switches to down state.
		// set all lights red
		// disable sonar
		// play error SFX

		GenerateRandomFuseOrder();

		currentFuse = 0;
		isBroken = true;

		foreach (Fuse fuse in fuses)
		{
			fuse.SetOff();
		}
	}

	public bool NextFuse(int whichFuse)
	{
		if(!isBroken) { return false; }

		if(whichFuse == fuseOrder[currentFuse])
		{
			currentFuse++;
		} else
		{
			// Messed up, reset all the fuses
			SetFusesToBrokenState();
			return false;
		}

		// all the fuses have been set in the correct order
		if(currentFuse == 6)
		{
			SetFusesToGoodState();
		}

		return true;
	}

}
