using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //variables
    public Image currentHealthbar;
    public Vector3 StartScale;
    public float enemyHealth;
    public float enemyMaxHealth;
    //waktu dimana musuh jadi ga bisa diserang
    //infinite time
    public float InvTime = 0;
    public float enemyNowHealth = 10;


    public void Start()
    {
        StartScale = currentHealthbar.GetComponent<RectTransform>().localScale;
    }

    public void Update()
    {
        enemyNowHealth += (enemyHealth - enemyNowHealth) * 0.4f * Time.deltaTime;
        float ratio = enemyHealth / enemyMaxHealth;
        currentHealthbar.rectTransform.localScale = new Vector3(StartScale.x * ratio, StartScale.y, StartScale.z);        

        if (InvTime > 0)
        {
            InvTime -= Time.deltaTime;
        }
    }

}
