using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject[] zomboesKind;
   // public int zombQuant;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
   // public static int currZombCount;
    private bool isSpawnActive;
    private bool spawned;


    public AudioClip[] zomboVoices;

    public AudioManager audioManager;

    void Start()
    {
       // currZombCount = 0;
        isSpawnActive = false;
        spawned = false;
    }

    void Update()
    {
        if (!LevelGenerator.paused && !isSpawnActive)
            StartSpawnZomboes();
        else if (LevelGenerator.paused && isSpawnActive)
        {
            StopSpawnZomboes();
            //if (spawned)
            //    destroyAllZomboes();
        }
    }

    public void StartSpawnZomboes()
    {
        InvokeRepeating("ZomboVoices", 3f, 1f);
        StartCoroutine(SpawnZomboes());
        StartCoroutine(SpawnZomboes());
        isSpawnActive = true;
    }

    public void ZomboVoices()
    {
        float roll = Random.Range(0f, 1f);
        if (roll > 0.4f)
            audioManager.PlayZombo();
    }

    public void StopSpawnZomboes()
    {
        StopAllCoroutines();
        isSpawnActive = false;
        CancelInvoke("ZomboVoices");
      
    }

    public void destroyAllZomboes()
    {
        GameObject[] zomboes = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject zombo in zomboes) {
            if (zombo.name != "zombie1" && zombo.name != "zombie2" && zombo.name != "zombie3") // save prefabs in the scene for instantiating
                 Destroy(zombo);
        }
        zomboes = null;
        spawned = false;
    }

    IEnumerator SpawnZomboes()
    {

        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                //if (currZombCount < zombQuant)
                //{
                      spawned = true;               
                    GameObject hazard = zomboesKind[Random.Range(0, zomboesKind.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    //GameObject zombo = new GameObject();
                    if (hazard != null)
                    StartCoroutine(EvasiveManeuverFun((GameObject)Instantiate(hazard, spawnPosition, spawnRotation)));
                    //currZombCount++;
               // }
                
                
                
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    IEnumerator EvasiveManeuverFun(GameObject go)
    {
        if (go != null)
        {
            go.GetComponent<Mover>().enabled = true;
            yield return new WaitForSeconds(8);
        }
        if (go != null)
        {
            go.GetComponent<Mover>().enabled = false;
            go.GetComponent<EvasiveManeuver>().enabled = true;
            yield return new WaitForSeconds(3);
        }
        if (go != null)
        {
            go.GetComponent<EvasiveManeuver>().enabled = false;
            go.GetComponent<EvasiveManeuverEnd>().enabled = true;
        }
    }




}