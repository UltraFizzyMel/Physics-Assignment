using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    public GameObject ai;
    public GameObject playerMovement;

    private void Start()
    {
        StartCoroutine(CountDownToStart());
    }

    IEnumerator CountDownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        
        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
		
		playerMovement.GetComponent<PlayerMovement>().enabled = true;
		ai.GetComponent<AI>().enabled = true;
    }
}
