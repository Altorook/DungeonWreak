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
    GameObject playerCapsule;
    ResetEnemyChest REC;
    // Start is called before the first frame update
    void Start()
    {
/*        playerCapsule = GameObject.Find("PlayerCapsule");
        REC = playerCapsule.GetComponent<ResetEnemyChest>();
        randomSpawn = Random.Range(randomSpawnMin, randomSpawnMax+1);
        if(randomSpawn > randomSpawnMax-3)
        {
           REC.chestsInGame.Add(Instantiate(chest, this.gameObject.transform));
        }*/
    }
    private void OnEnable()
    {
        playerCapsule = GameObject.Find("PlayerCapsule");
        REC = playerCapsule.GetComponent<ResetEnemyChest>();
        randomSpawn = Random.Range(randomSpawnMin, randomSpawnMax + 1);
        if (randomSpawn > randomSpawnMax - 3)
        {
            REC.chestsInGame.Add(Instantiate(chest, this.gameObject.transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
           
      
    }
}
