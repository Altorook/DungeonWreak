using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    
   public int randomSpawn = 0;
    int randomSpawnMax = 5;
    int randomSpawnMin = 0;
    [SerializeField]
    GameObject chest;
    // Start is called before the first frame update
    void Start()
    {
        randomSpawn = Random.Range(randomSpawnMin, randomSpawnMax+1);
        if(randomSpawn > randomSpawnMax-3)
        {
            Instantiate(chest, this.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
           
      
    }
}
