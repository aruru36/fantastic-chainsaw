using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image currentHealthbar;
    public Text ratioText;
    public GameObject player;
    public GameObject gameOver;
    public GameObject button;
    public GameObject retry;

    private float hitPoint = 200;
    private float nowPoint = 200;
    private float maxHitpoint = 200;
    public float InvisTime = 0;

    private void Start()
    {
        UpdateHealthBar();        
    }

    private void Update()
    {
        UpdateHealthBar();
        if (InvisTime > 0)
        {
            InvisTime -= Time.deltaTime;
        }
    }

    public void UpdateHealthBar()
    {
        nowPoint += (hitPoint - nowPoint) * 0.4f * Time.deltaTime;
        float ratio = hitPoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString();
        if (hitPoint < 0)
        {
            hitPoint = 0;
            Debug.Log("Dead!");
            gameOver.SetActive(true);
            gameOver.GetComponent<Text>().enabled = true;
            button.SetActive(true);
            button.GetComponent<Image>().enabled = true;
            retry.SetActive(true);
            retry.GetComponentInChildren<Text>().enabled = true;

            Destroy(player);
        }
    }

    public void TakeDamage(float damage)
    {
        if (InvisTime <= 0)
        {
            hitPoint -= damage;
        }
        UpdateHealthBar();

    }
    public void HealDamage(float heal)
    {
        hitPoint += heal*Time.deltaTime*2;
        if (hitPoint > maxHitpoint)
        {
            hitPoint = maxHitpoint;
        }
        UpdateHealthBar();
    }    
}
