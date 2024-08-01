using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float health;
    int silverCurrency = 50;
    int goldCurrency = 0;
    [SerializeField]
    GameObject SilverText;
    [SerializeField]
    GameObject GoldText;
    // Start is called before the first frame update
    void Start()
    {
        SilverText.GetComponent<TMP_Text>().SetText("Silver: " + silverCurrency.ToString());
        GoldText.GetComponent<TMP_Text>().SetText("Gold: " + goldCurrency.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(health  < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(GetSilver() >= 100)
        {
            SetGold(GetGold()+1);
            SetSilver(GetSilver()-100);
        }
        if(GetSilver() < 0)
        {
            SetGold(GetGold()-1);
            SetSilver(GetSilver()+100);
        }
    }
    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float playerHealth)
    {
        health = playerHealth;
    }
    public int GetSilver()
    {
        return silverCurrency;
    }
    public void SetSilver(int silver)
    {
        silverCurrency = silver;
        SilverText.GetComponent<TMP_Text>().SetText("Silver: " + silverCurrency.ToString());
    }
    public int GetGold()
    {
        return goldCurrency;
    }
    public void SetGold(int gold)
    {
        goldCurrency = gold;
        GoldText.GetComponent<TMP_Text>().SetText("Gold: " + goldCurrency.ToString());
    }
}
