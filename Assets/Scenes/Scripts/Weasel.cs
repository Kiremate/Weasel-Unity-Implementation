using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weasel : MonoBehaviour
{
    private char[] letters = {' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

    // Phrase to create
    [SerializeField]
    private string goalPhrase;

    // Max quantity of characters
    private const int MAX_CHARACTERS = 28;
    // Max quantity of phrases for each generation
    private const int MAX_COPIES = 100;
    // Probability of mutating
    private const int MUTATION_PROC = 5;
    // Last generated phrase
    private string currentPhrase;
    // Max Score Phrase
    private string maxScorePhrase;
    // Number of the current generation
    public static int numGeneration = 0;

    private int lastScore = 0;
    private int maxScoreAchieved = 0;
    // Copies
    [SerializeField]
    private string[] randomStrings;

    private bool foundPhrase = false;
    private bool foundItEffect = false;

    [SerializeField]
    private Text printPhrases;
    [SerializeField]
    private Text printScoreBoard;
    [SerializeField]
    private Text printGen;
    
    [SerializeField]
    private GameObject FoundItGo;



    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("generatePhrases");
        StartCoroutine("printRandomCharacters");
        StartCoroutine("printScore");
        StartCoroutine("printGeneration");

    }

    // Update is called once per frame
    void Update()
    {



    }


    // Generaters first random String
    // and keeps creating once the first its done
    private string generateRandomString()
    {
        string result = "";
        // If there is no phrase, create one
        if (currentPhrase == null)
        {
            for (int i = 0; i < MAX_CHARACTERS; ++i)
            {
                result += letters[Random.Range(0, letters.Length)];
            }
            currentPhrase = result;

        }
        else
        {

            for (int i = 0; i < currentPhrase.Length; ++i)
            {
                // 5% prob to mutate
                if (Random.Range(0, MAX_COPIES + 1) < MUTATION_PROC)
                {
                    result += letters[Random.Range(0, letters.Length)];
                }
                else
                {
                    result += currentPhrase[i];
                }
            }

        }
        int score = generateScore(result);
        // Save max score phrase
        if (score >= maxScoreAchieved)
        {
            maxScorePhrase = result;
        }
        return result;
    }

    // Generate 100 phrases
    private IEnumerator generatePhrases()
    {
        while (!foundPhrase)
        {
            if (maxScorePhrase != null)
                currentPhrase = maxScorePhrase;

            randomStrings = new string[MAX_COPIES];
            for (int i = 0; i < randomStrings.Length; ++i)
            {
                randomStrings[i] = generateRandomString();
            }
            // New Phrase with more score than the previous one
            // Increment of the new generation 
            numGeneration++;
            // Clear Scoreboard
            printScoreBoard.text = "";
            yield return new WaitForSeconds(1);
        }
        Debug.LogError("FOUND IT MFKER!");
        yield return null;
    }

    private int generateScore(string phrase)
    {

        int result = 0;
        for (int i = 0; i < phrase.Length; ++i)
        {
            if (phrase[i].Equals(goalPhrase[i]))
                result++;


        }

        lastScore = result;

        if (result > maxScoreAchieved)
            maxScoreAchieved = result;

        if (result == 28 && !foundItEffect)
        {
            StartCoroutine("foundIt");
            foundPhrase = true;
        }

        return result;
    }

    private IEnumerator printRandomCharacters()
    {
        for (; ; )
        {
            if(maxScorePhrase != null)
                printPhrases.text += numGeneration.ToString() + ": " + maxScorePhrase + " score: " + generateScore(maxScorePhrase) + "\n";


            yield return new WaitForSeconds(1);
        }

    }


    private IEnumerator printGeneration()
    {
        for (; ; )
        {
            // Print the best of the gen
            printGen.text = "GENERATION: " + numGeneration.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator printScore()
    {
        for (; ; )
        {
            printScoreBoard.text = "SCORE: " + maxScoreAchieved.ToString();
            yield return new WaitForSeconds(1);
        }
    }


    private IEnumerator foundIt()
    {
        foundItEffect = true;
        
        for(; ; )
        {
            FoundItGo.SetActive(!FoundItGo.activeSelf);
            yield return new WaitForSeconds(1);
        }
    }


}
