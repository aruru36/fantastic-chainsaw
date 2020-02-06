using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggeringEndGame : MonoBehaviour
{
    public GameObject gameClear;
    public GameObject buttonClear;
    public GameObject end;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameClear.SetActive(true);
            gameClear.GetComponent<Text>().enabled = true;
            buttonClear.SetActive(true);
            buttonClear.GetComponent<Image>().enabled = true;
            end.SetActive(true);
            end.GetComponentInChildren<Text>().enabled = true;
            Destroy(player);
        }
    }
}
