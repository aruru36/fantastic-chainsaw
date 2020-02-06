using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [Multiline]
    public string[] talkingText;

    public Text dialogue;

    public void Start()
    {        
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            dialogue.gameObject.SetActive(true);
            dialogue.GetComponent<Text>().enabled = true;
            //munculin text 1 per satu 
            if (dialogue.text.Length < talkingText[0].Length)
            {
                dialogue.text += talkingText[0][dialogue.text.Length];
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //kalau tidak pakai "" textnya bakal keluar bwaaan 
            dialogue.text = "";
            dialogue.gameObject.SetActive(false);
            dialogue.GetComponent<Text>().enabled = false;
        }
    }
}
