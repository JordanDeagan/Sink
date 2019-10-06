using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkObject : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        StartCoroutine(callDialogue());
    }

    override protected IEnumerator callDialogue()
    {
        yield return StartCoroutine(script.SinkDialogue());
    }
}