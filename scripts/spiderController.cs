using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderController : MonoBehaviour
{
    GameObject selectedSpider;
    void Start()
    {
        DeactivateSpidersOutline();
        GetComponent<speechRecognition>().OnSpeechRecognized += ExecuteCommand;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("spiders"))
                {
                    DeactivateSpidersOutline();
                    selectedSpider = raycastHit.collider.gameObject;
                    selectedSpider.GetComponent<Outline>().enabled = true;
                    GetComponent<speechRecognition>().StartRecording();
                }
            }
        }
    }

    void DeactivateSpidersOutline()
    {
        GameObject[] spiders = GameObject.FindGameObjectsWithTag("spiders");
        foreach (GameObject spider in spiders)
        {
            spider.GetComponent<Outline>().enabled = false;
        }
    }
    
    void ExecuteCommand(string command)
    {
        command = command.ToLower();
        if (command.Contains("jump"))
        {
            selectedSpider.GetComponent<spiderMovement>().Jump();
        }
        else if (command.Contains("flip"))
        {
            selectedSpider.GetComponent<spiderMovement>().Flip();
        }
    }
}
