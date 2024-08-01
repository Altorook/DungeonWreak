using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetEnemyChest : MonoBehaviour
{
    public List<GameObject> enemiesInGame = new List<GameObject>();
    public List<GameObject> chestsInGame = new List<GameObject>();
    [SerializeField]
    GameObject LevelLayout;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void DeleteEnemy()
    {
        foreach(GameObject gameObject in enemiesInGame)
        {
            Destroy(gameObject);
        }
        enemiesInGame.Clear();
    }
    void DeleteChest()
    {
        foreach (GameObject gameObject in chestsInGame)
        {
            Destroy(gameObject);
        }
        chestsInGame.Clear();
    }
    public void HandleReset()
    {
        DeleteEnemy();
        DeleteChest();
        LevelLayout.SetActive(false);
        LevelLayout.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        

        
    }
}
