using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameplayState currGameplayState = GameplayState.NONE;
    public static int rangeofFirstLevelUser = 9;
    public static int rangeofSecondLevelUser = 20;
    public static int rangeofThirdLevelUser = 40;
    public static int rangeofFourthLevelUser = 60;
    public static string currOperator = "";

    int value1;
    int value2;
    int result;

    public int finalV1, finalV2, finalResult;

    [SerializeField] GameObject[] bottomBarObjs = new GameObject[3];

    [SerializeField] Transform bottomBarPar;

    [SerializeField]
    GameObject mainMenu, gamePlayScreen, levelWinScreen, levelFailScreen;

    private void Start()
    {
        if(gamePlayScreen.activeInHierarchy)
        {
            gamePlayScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        GenerateRandomValues();
    }

    public void CheckForLevelComplete()
    {
        if(bottomBarPar.childCount >= 1)
        {
            return;
        }

        switch(currGameplayState)
        {
            case GameplayState.ADDITION:
                if (finalResult == result)
                {
                    if ((finalV1 == value1 && finalV2 == value2) || (finalV1 == value2 && finalV2 == value1))
                    {
                        levelWinScreen.SetActive(true);
                    } 
                }
                break;
            case GameplayState.SUBTRACTION:
                break;
            case GameplayState.MULTIPLICATION:
                break;
            case GameplayState.DIVISION:
                break;
        }
    }

    

    public void CheckForLevelFail()
    {

    }

    void GenerateRandomValues()
    {
        int playerLevel = GetCurrUserLevel();

        switch(playerLevel)
        {
            case 0:
                value1 = Random.Range(0, rangeofFirstLevelUser);
                value2 = Random.Range(0, rangeofFirstLevelUser);
                break;
            case 1:
                value1 = Random.Range(0, rangeofSecondLevelUser);
                value2 = Random.Range(0, rangeofSecondLevelUser);
                break;
            case 2:
                value1 = Random.Range(0, rangeofThirdLevelUser);
                value2 = Random.Range(0, rangeofThirdLevelUser);
                break;
            case 3:
                value1 = Random.Range(0, rangeofFourthLevelUser);
                value2 = Random.Range(0, rangeofFourthLevelUser);
                break;
        }

        result = value1 + value2;
        Debug.Log("Value 1: " + value1 + "\tValue 2 : " + value2 + "\tRes = " + result);
    }

    void SetValuesToUIElements()
    {
        if(Random.value >= 0.5f)
        {
            bottomBarObjs[0].GetComponent<DragableItem>().SetValueTextBox(value1);
            bottomBarObjs[1].GetComponent<DragableItem>().SetValueTextBox(value2);
            bottomBarObjs[2].GetComponent<DragableItem>().SetValueTextBox(result);
        }
        else
        {
            bottomBarObjs[2].GetComponent<DragableItem>().SetValueTextBox(value1);
            bottomBarObjs[0].GetComponent<DragableItem>().SetValueTextBox(value2);
            bottomBarObjs[1].GetComponent<DragableItem>().SetValueTextBox(result);
        }
    }

    void SetTheCurrentArithematicOperator()
    {
        switch (currGameplayState)
        {
            case GameplayState.ADDITION:
                currOperator = "+";
                break;
            case GameplayState.SUBTRACTION:
                currOperator = "-";
                break;
            case GameplayState.MULTIPLICATION:
                currOperator = "*";
                break;
            case GameplayState.DIVISION:
                currOperator = "/";
                break;
            case GameplayState.NONE:
                break;
        }
    }

    public void SetCurrGameState(int currGS)
    {
        currGameplayState = (GameplayState)currGS;
        mainMenu.SetActive(false);
        SetTheCurrentArithematicOperator();
        SetValuesToUIElements();
        gamePlayScreen.SetActive(true);
        Debug.Log("Curr Game State: " + currGameplayState);
    }

    int GetCurrUserLevel()
    {
        return PlayerPrefs.GetInt("CURR_USER_LEVEL", 0); //see PlayerPrefs class documentation
    }

    int GetCurrLevelsPlayed()
    {
        return PlayerPrefs.GetInt("CURR_LEVELS_PLAYED", 0);
    }
}

public enum GameplayState
{
    ADDITION = 0,
    SUBTRACTION = 1,
    MULTIPLICATION = 2,
    DIVISION = 3,
    NONE = 4
}