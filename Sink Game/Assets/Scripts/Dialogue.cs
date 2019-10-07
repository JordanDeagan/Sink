using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    int chooseSelf, chooseBlue;
    public Text talk, credits;
    string creditText;
    public GameObject plant, tub, sink, optionA, optionB, blackScreen, DisplayDuck,
        Teabags, Melatonin, soap, duckCollection, OneToothbrush, TwoToothbrush, 
        deadPlant, flowerPlant, washSink, washHands, singleDuck, title;
    GameObject hairDye;
    bool GetTeabag, GetMelatonin, duckCollected, wateredPlant, chooseA, chooseB, rejectDuck;
    // Start is called before the first frame update
    void Start()
    {
        chooseBlue = 0;
        chooseSelf = 0;
        creditText = "Writing by Daniel Dykiel\nArt by Freddie O’Brion\nConcept art by Kanti Gudur and Freddie O’Brion\nMusic by Jimi DePriest\nProgramming by Jordan Deagan\nStory concept by Daniel Dykiel, Freddie O’Brion, Kanti Gudur, Jimi DePriest, Hunter Mundy, and Jordan Deagan";
        StartCoroutine(Begining());
    }

    IEnumerator Begining()
    {
        setText("Click to start\n\n");
        yield return StartCoroutine(WaitForClick());
        title.SetActive(false);
        setText("You should probably clean the sink\n\n");
        sink.SetActive(true);
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
       



    public IEnumerator SinkDialogue()
    {
        sink.SetActive(false);
        washSink.SetActive(true);
        yield return StartCoroutine(startSink());
    }

    IEnumerator startSink()
    {
        setText("It’s late.\nOr maybe very early—you aren’t sure.\nEither way, your eyelids have begun to weigh.");
        yield return StartCoroutine(WaitForClick());
        prepChoice("Will you have trouble falling asleep tonight?\n>Yes\n>No");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            yield return StartCoroutine(StaysUp());
        }
        yield return StartCoroutine(CleanUp());
    }

    IEnumerator StaysUp()
    {
        prepChoice("Why won’t you be able to sleep?\n>Blue keeps you up at night\n>You’ve always had insomnia");
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
        prepChoice("Will you confront them about this?\n>Yes\n>No");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            GetTeabag = true;
            GetMelatonin = false;
        }
        else if (chooseB)
        {
            GetMelatonin = true;
            GetTeabag = false;
        }
    }

    IEnumerator CleanUp()
    {
        setText("The liquid is drying on the sink.\nIf you wait until tomorrow to clean it, it will\ncongeal.");
        yield return StartCoroutine(WaitForClick());
        prepChoice("What do you clean first?\n>The sink\n>Your hands");
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
        washSink.SetActive(false);
        washHands.SetActive(true);
        hairDye = washHands;
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
        hairDye = washSink;
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
        setText("You want to take a bath\n\n");
        yield return null;
    }

    



    public IEnumerator TubDialogue()
    {
        tub.SetActive(false);
        yield return StartCoroutine(StartTub());
    }

    public IEnumerator StartTub()
    {
        setText("The dye has sunk into your hands,\nand only your hands. But somehow\nyou feel it over the rest of your body.");
        yield return StartCoroutine(WaitForClick());
        setText("Hot and prickling, like a rash.\nYou run your fingers over your skin\nand expect to feel dry bumps.");
        yield return StartCoroutine(WaitForClick());
        setText("When you moved apartments,\nBlue gave you a rubber duck.\n");
        yield return StartCoroutine(WaitForClick());
        setText("They had been so pleased with the gift\nthat they couldn’t stop giggling to themselves.\n");
        yield return StartCoroutine(WaitForClick());
        DisplayDuck.SetActive(true);
        setText("It was vibrant, almost ugly.\nYou hated keeping it in the bathroom.\n");
        yield return StartCoroutine(WaitForClick());
        setText("Sometimes it was as if you could feel\nits eyes on you, even if they were just\nflecks of paint.");
        yield return StartCoroutine(WaitForClick());
        setText("But later, you realized why Blue\nhad chosen such an odd-seeming gift.\n");
        yield return StartCoroutine(WaitForClick());
        prepChoice("It was because:\n>They were playing a joke on you\n>You used to collect rubber ducks as a child");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            yield return StartCoroutine(PlayJoke());
        }
        else if (chooseB)
        {
            yield return StartCoroutine(CollectDucks());
        }
        yield return StartCoroutine(Appreciate());
    }

    IEnumerator PlayJoke()
    {
        setText("Sometimes it feels like Blue speaks a different language. Their voice rises and falls, their words blur together.");
        yield return StartCoroutine(WaitForClick());
        setText("And their jokes are long, convoluted. You didn’t know if they’re overly-pointed, or if you read too deep into them.");
        yield return StartCoroutine(WaitForClick());
        prepChoice("The joke had been about: \n>This being your first apartment\n>Your father being a programmer");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            yield return StartCoroutine(FirstApart());
        }
        else if (chooseB)
        {
            yield return StartCoroutine(Father());
        }
    }

    IEnumerator FirstApart()
    {
        setText("You agonized over color schemes, every object you bought. You wanted to build a space that was cozy and sophisticated.");
        yield return StartCoroutine(WaitForClick());
        setText("You didn’t realize how intense you were being until Blue pointed it out to you.\n");
        yield return StartCoroutine(WaitForClick());
        setText("And they had bought you a rubber duck—gaudy and cheap.\n");
        yield return StartCoroutine(WaitForClick());
    }

    IEnumerator Father()
    {
        setText("Your father had kept a rubber duck in his office bookcase.\n");
        yield return StartCoroutine(WaitForClick());
        setText("You didn’t remember exactly how that connected to programming, but somehow it did.");
        yield return StartCoroutine(WaitForClick());
        setText("You didn’t like to think about it too much: after all, your relationship with your father could be described as strained.");
        yield return StartCoroutine(WaitForClick());
        prepChoice("Were you ok with Blue joking about him?\n>Yes\n>No");
        yield return StartCoroutine(WaitForButton());
    }

    IEnumerator CollectDucks()
    {
        setText("You didn’t even remember talking about it. But you must have, because Blue remembered.\n");
        yield return StartCoroutine(WaitForClick());
        prepChoice("This is significant because:\n>You want to collect rubber-ducks again\n>Blue's thoughtfulness surprises you.");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            duckCollected = true;
        }
    }

    IEnumerator Appreciate()
    {
        prepChoice("Do you appreciate the gift?\n>Yes\n>No");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            chooseBlue++;
            setText("You adjust the rubber duck so that it centers more on the ledge. Right now, you can’t help but feel a certain special fondness for it.");
            yield return StartCoroutine(WaitForClick());
        }
        else if (chooseB)
        {
            chooseSelf++;
            rejectDuck = true;
            setText("It hadn’t felt right, to throw away a gift. But you want to.\n");
            yield return StartCoroutine(WaitForClick());
            setText("Your hand twitches. \n\n");
            yield return StartCoroutine(WaitForClick());
            setText("You reach out and for a moment, you think you’ll knock the duck into the tub.\n");
            yield return StartCoroutine(WaitForClick());
            setText("Instead, you turn it so it faces inward towards the wall.\n");
            yield return StartCoroutine(WaitForClick());
        }
        yield return StartCoroutine(EndTub());
    }

    public IEnumerator EndTub()
    {
        plant.SetActive(true);
        setText("You should probably check on the plant\n\n");
        yield return null;
    }



    public IEnumerator PlantDialogue()
    {
        plant.GetComponent<PlantObject>().enabled = false;
        plant.GetComponent<SpriteRenderer>().enabled = true;
        yield return StartCoroutine(StartPlant());
    }

    IEnumerator StartPlant()
    {
        setText("You step out of the tub. You walk to the plant in its corner.\n");
        yield return StartCoroutine(WaitForClick());
        setText("At first, the plant had brightened up the small room, added a breath of life.\n");
        yield return StartCoroutine(WaitForClick());
        setText("But over time it became smothering, almost selfish in the way it grew.\n");
        yield return StartCoroutine(WaitForClick());
        prepChoice("Did you remember to water the plant today?\n>Yes\n>No");
        yield return StartCoroutine(WaitForButton());
        if (chooseA)
        {
            wateredPlant = true;
            chooseBlue++;
        }
        else if (chooseB)
        {
            chooseSelf++;
        }
        yield return StartCoroutine(endGame());
    }





    IEnumerator endGame()
    {
        yield return StartCoroutine(FadeToBlack());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        hairDye.SetActive(false);
        DisplayDuck.SetActive(false);
        if (chooseSelf >= 2)
        {
            OneToothbrush.SetActive(true);
            if (GetMelatonin)
            {
                Melatonin.SetActive(true);
            }
            if (duckCollected)
            {
                duckCollection.SetActive(true);
            }
            else if (rejectDuck)
            {
                soap.SetActive(true);
            }
            else
            {
                singleDuck.SetActive(true);
            }
            if (wateredPlant)
            {
                flowerPlant.SetActive(true);
            }
            else
            {
                deadPlant.SetActive(true);
            }
        }
        else if (chooseBlue >= 2)
        {
            TwoToothbrush.SetActive(true);
            if (GetMelatonin)
            {
                Melatonin.SetActive(true);
            }
            else if (GetTeabag)
            {
                Teabags.SetActive(true);
            }
            if (duckCollected)
            {
                duckCollection.SetActive(true);
            }
            else if (rejectDuck)
            {
                soap.SetActive(true);
            }
            else
            {
                singleDuck.SetActive(true);
            }
            if (wateredPlant)
            {
                flowerPlant.SetActive(true);
            }
            else
            {
                deadPlant.SetActive(true);
            }
        }
        setText("");
        yield return StartCoroutine(FadeFromBlack());
        setText("Two Months Later\n\n(click to continue)");
        yield return StartCoroutine(WaitForClick());
        prepChoice("Exit Game\n>Yes\n>No");
        yield return StartCoroutine(WaitForButton());
        if (chooseB)
        {
            setText("Click to exit\n\n");
            yield return StartCoroutine(WaitForClick());
        }
        yield return StartCoroutine(runCredits());

    }

    IEnumerator FadeToBlack()
    {
        Color tmp = blackScreen.GetComponent<SpriteRenderer>().color;
        while (tmp.a < 1)
        {
            tmp.a += .005f;
            blackScreen.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeFromBlack()
    {
        Color tmp = blackScreen.GetComponent<SpriteRenderer>().color;
        while (tmp.a >0)
        {
            tmp.a -= .005f;
            blackScreen.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator runCredits()
    {
        yield return StartCoroutine(FadeToBlack());
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        credits.text = creditText;
        TwoToothbrush.SetActive(false);
        OneToothbrush.SetActive(false);
        Melatonin.SetActive(false);
        Teabags.SetActive(false);
        duckCollection.SetActive(false);
        soap.SetActive(false);
        singleDuck.SetActive(false);
        flowerPlant.SetActive(false);
        deadPlant.SetActive(false);
        blackScreen.SetActive(false);
        yield return new WaitForSecondsRealtime(15);
        Application.Quit();  
    }
}
