using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyV1Prefab;
    List<int> enemyAmount = new List<int>();
    int enemiesSpawned;
    List<GameObject> enemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        enemyAmount.Add(2);
        enemyAmount.Add(3);
        enemyAmount.Add(3);
        enemyAmount.Add(4);
        enemyAmount.Add(4);
        enemyAmount.Add(5);
        enemyAmount.Add(6);
       enemiesSpawned = enemyAmount.ElementAt(Random.Range(0,enemyAmount.Count()));
        for(int i = 0; i < enemiesSpawned; i++)
        {
          enemyList.Add(Instantiate(enemyV1Prefab,this.transform.position,Quaternion.identity));
            enemyList.ElementAt(i).transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
