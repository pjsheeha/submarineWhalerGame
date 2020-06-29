using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class newspaperManager : MonoBehaviour
{
    public EcosystemManagement eco;
    public PlayerScript pla;
    string dayNumber;
    public TextMeshProUGUI[] titlesGUI;
    List<string> possibleTitles = new List<string>();
    List<int> possibleTitleScores = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        updateTitles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateTitlesFirstDay()
    {
        //

    }

    void appendPosibleTitle(string str, int score)
    {

        possibleTitles.Add(str);
        possibleTitleScores.Add(0);
    }

    int updateTitles()
    {
        /// generate based on facts

        // day number
        dayNumber = eco.day.ToString();
        // player actions
        string[] str;
        switch(pla.getPlayerActions())
        {
            case 0:
                str = new string[] { "The Player Did Literally Nothing",
                                            "Local Person Takes Boring Pictures"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
        }
        // population increase/decrease
        switch (eco.getPopulationChange())
        {
            case 0:
                str = new string[] { "Reports Say Environment Didn't Suffer Changes Since Yesterday"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
        }
        // gas price/inflation
        switch (eco.getPriceChecks())
        {
            case 0:
                str = new string[] { "Prices Are Back To Normal", "Gasoline Price Is Stable Again"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
            case 1:
                str = new string[] { "Gasoline Prices Are Up As Tourism Plunges",
                                    "Bad Tourism Fucked Gas Prices Again"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 1);
                break;

        }
        // global warmth
        switch (eco.getGlobalWarmth())
        {
            case 0:
                str = new string[] { "Plankton Keeps Temperature Stable",
                                        "'Local Temperatures Are Alright' Local Man Says"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
        }
        // specific animal increase/decrease
        /*too tired for this my boy*/
        // corals
        switch (eco.getCoralsState())
        {
            case 0:
                str = new string[] { "Corals Are The Same As Ever",
                                    "Corals Are Nice idk"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
            case -1:
                str = new string[] { "Make Corals White Again?",
                                    "Corals get Michael Jackson'd As Warmth Rises",
                                    "Local Woman Gets Excited At Coral's 'New White' Look, Turns Out They're Just Dying"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 1);
                break;
        }
        // tourism state
        switch (eco.getTourismState())
        {
            case 0:
                str = new string[] { "Local Man Sells The Same Amount Of Necklaces He Did Yesterday",
                                    "Tourism Stores Are Neither Closing Or Opening"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
        }
        // whaler ships politics/opinion piece
        switch (eco.getPublicOpinion())
        {
            case 0:
                str = new string[] { "No One Cares About Anything"};
                appendPosibleTitle(str[Random.Range(0, str.Length)], 0);
                break;
        }
        // filler (anything if we need to fill space)
        int interestingStories = calculateInterestingStories();
        while(interestingStories < titlesGUI.Length)
        {
            str = new string[] {"Another Anti-LGBT Rights Republican Gets Outed As Closeted Homosexual",
                                "Florida Company Names Only Black Employee “Chief Diversity Officer” Without Asking Him",
                                "Game Developer Realizes They Don't Even Enjoy Playing Games Anymore",
                                "WHOLESOME: 6-year Old Kid Sells Kidney To Buy 3 More Months Of Mother's Insulin",
                                "Liberal Politician Proves He's Not Racist By Singing 'Shakira - This Time For Africa' In Blackface",
                                "Meet The Only White Demographic That Uses Spice: The White Cop",
                                "New Study Finds 99% Of Cubans That Criticize Cuba Where Born And Raised Outside Cuba",
                                "Seattle Registers First Klansman Murder By A Cop As He Shoots Himself"};
            appendPosibleTitle(str[Random.Range(0, str.Length)], 1);
            interestingStories = calculateInterestingStories();
        }


        ///assign the strings to the actual gui
        for(int i = 0; i < titlesGUI.Length; i++)
        {
            titlesGUI[i].SetText(possibleTitles[i]);
        }

        return 1;
    }

    int calculateInterestingStories()
    {
        //if (titlesGUI.Length > possibleTitles.Count) return -1;
        
        int n = 0;
        for(int i = 0; i < possibleTitleScores.Count; i++)
        {
            n += possibleTitleScores[i];
        }
        return n;
    }
}
