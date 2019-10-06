using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public Dialogue script;
    // Start is called before the first frame update

    abstract protected IEnumerator callDialogue();
}
