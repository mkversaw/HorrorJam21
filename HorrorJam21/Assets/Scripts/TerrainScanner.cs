using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TerrainScanner : MonoBehaviour
{

    [SerializeField] GameObject TerrainScannerPrefab;
    [SerializeField] float duration = 10;
    [SerializeField] float size = 500;

    [SerializeField] float coolDown = 3f;

    private bool isOnCoolDown = false;


    IEnumerator CoolDown()
	{
        isOnCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isOnCoolDown = false;
	}

    public void StartTerrainScan()
	{
        if (!isOnCoolDown) // spam prevention
        {
            SpawnTerrainScanner();
            StartCoroutine(CoolDown()); 
        }
	}

    private void SpawnTerrainScanner()
	{
        Debug.Log("Scanning terrain");

        GameObject currScanner = Instantiate(TerrainScannerPrefab); 
        // set to same location as current camera POS in world space
        currScanner.transform.position = transform.position;

        ParticleSystem particleSys = currScanner.transform.GetChild(0).GetComponent<ParticleSystem>();


        if (particleSys != null)
		{
            ParticleSystem.MainModule main = particleSys.main;
            main.startLifetime = duration;
            main.startSize = size;
		} else
		{
            Debug.LogError("Child did not have a particle system. (Configured wrong?)");
		}

        // destroy instance after the duration has elapsed (+ 1 second)
        Destroy(currScanner, duration + 1);
	}
}
