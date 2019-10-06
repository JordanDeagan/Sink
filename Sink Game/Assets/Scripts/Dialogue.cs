using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    int chooseSelf, chooseBlue;
    public Text talk;
    public GameObject plant, tub, counter, sink, optionA, optionB;
    bool Teabag, Melatonin, duckCollected, wateredPlant, chooseA, chooseB;
    // Start is called before the first frame update
    void Start()
    {
        chooseBlue = 0;
        chooseSelf = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setText(string message)
    {
        talk.text = message;
    }

    void prepChoice(string question)
    {
        chooseA = false;
        chooseB = false;
        setText(question);
    }

    public IEnumerator WaitForButton()
    {
        while (Input.GetMouseButtonDown(0)) {
            yield return null;
        }
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null && hit.transform == optionA.transform)
                {
                    chooseA = true;
                    break;
                }
                if (hit.collider != null && hit.transform == optionB.transform)
                {
                    chooseB = true;
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator WaitForClick()
    {
        while (Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
    }
       
    IEnumerator startSink()
    {
        setText("It’s late.\nOr maybe very early—you aren’t sure.\nEither way, your eyelids have begun to weigh.");
        yield return StartCoroutine(WaitForClick());
        prepChoice("Will you have trouble falling asleep tonight?\nYes\nNo");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            yield return StartCoroutine(StaysUp());
        }
        yield return StartCoroutine(CleanUp());
    }

    IEnumerator StaysUp()
    {
        prepChoice("Why won’t you be able to sleep?\nBlue keeps you up at night\nYou’ve always had insomnia");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            yield return StartCoroutine(KeptUp());
        }
    }

    IEnumerator KeptUp()
    {
        setText("Blue is obsessed with tea. So much so\nthat they keep a cabinet of it in your shared\nbedroom, teabags spilling out over the drawer.");
        yield return StartCoroutine(WaitForClick());
        setText("They keep an electric kettle, too.\nThey love making tea in the middle of the night,\ncupping the warm mug between their palms.");
        yield return StartCoroutine(WaitForClick());
        setText("But the sound of the kettle wakes you up.\n\n");
        yield return StartCoroutine(WaitForClick());
        prepChoice("Will you confront them about this?\nYes\nNo");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            Teabag = true;
            Melatonin = false;
        }
        else if (chooseB)
        {
            Melatonin = false;
            Teabag = false;
        }
    }

    IEnumerator CleanUp()
    {
        setText("The liquid is drying on the sink.\nIf you wait until tomorrow to clean it, it will\ncongeal.");
        yield return StartCoroutine(WaitForClick());
        prepChoice("What do you clean first?\nThe sink\nYour hands");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            yield return StartCoroutine(cleanSink());
        }
        else if (chooseB)
        {
            yield return StartCoroutine(cleanSelf());
        }
        yield return StartCoroutine(endSink());
    }

    IEnumerator cleanSink()
    {
        chooseBlue++;
        setText("You mop up the liquid with a cleaning wipe.\nAnd then another.\nAnd another.");
        yield return StartCoroutine(WaitForClick());
        setText("Maybe it’s because it’s the first time you’ve\ndyed your hair, but somehow,\nyou’ve managed to get it everywhere.");
        yield return StartCoroutine(WaitForClick());
        setText("And it’s too late, anyway\nThe color has already sunk into the porcelain.\n");
        yield return StartCoroutine(WaitForClick());
        setText("You feel a twinge of guilt.\nBlue will wake up tomorrow morning\nand wonder at the stains.");
        yield return StartCoroutine(WaitForClick());
        setText("You picture their half-awake face in your\nmind’s eye: bleary, confused,\nmaybe even afraid of the change.");
        yield return StartCoroutine(WaitForClick());
        setText("You didn’t tell them,\nthat you were planning to do this.\nYou wanted something for yourself.");
        yield return StartCoroutine(WaitForClick());
    }

    IEnumerator cleanSelf()
    {
        chooseSelf++;
        setText("The water runs a saccharine pink as it passes\nthrough your fingers.\nYour hands are still stained from the dye.");
        yield return StartCoroutine(WaitForClick());
        setText("You know Blue is going to comment on it.\nMaybe it’s selfish, but you don’t want them to,\nno matter how casual they are.");
        yield return StartCoroutine(WaitForClick());
        setText("You want something for yourself.\n\n");
        yield return StartCoroutine(WaitForClick());
    }

    IEnumerator endSink()
    {
        tub.SetActive(true);
        yield return null;
    }

    public IEnumerator SinkDialogue()
    {
        sink.SetActive(false);
        yield return StartCoroutine(startSink());
    }

    public IEnumerator TubDialogue()
    {
        tub.SetActive(false);
        talk.text = "this is a Tub\n\n";
        plant.SetActive(true);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator PlantDialogue()
    {
        plant.SetActive(false);
        talk.text = "this is a Plant\n\n";
        yield return new WaitForFixedUpdate();
    }

    void endGame()
    {
    }
}
