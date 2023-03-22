using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Text operatorText;

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
        Debug.Log(bottomBarPar.childCount);
        if(bottomBarPar.childCount != 0)
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
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.SUBTRACTION:
                if (finalResult == result)
                {
                    if (value1 > value2)
                    {
                        if ((finalV1 == value1 && finalV2 == value2))
                        {
                            levelWinScreen.SetActive(true);
                        } 
                    }
                    else
                    {
                        if ((finalV1 == value2 && finalV2 == value1))
                        {
                            levelWinScreen.SetActive(true);
                        }
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.MULTIPLICATION:
                if (finalResult == result)
                {
                    if ((finalV1 == value1 && finalV2 == value2) || (finalV1 == value2 && finalV2 == value1))
                    {
                        levelWinScreen.SetActive(true);
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
            case GameplayState.DIVISION:
                if (finalResult == result)
                {
                    if (value1 > value2)
                    {
                        if ((finalV1 == value1 && finalV2 == value2))
                        {
                            levelWinScreen.SetActive(true);
                        } 
                    }
                    else
                    {
                        if ((finalV1 == value2 && finalV2 == value1))
                        {
                            levelWinScreen.SetActive(true);
                        }
                    }
                }
                else
                {
                    CheckForLevelFail();
                }
                break;
        }
    }

    public void GoToNextLevel()
    {
        PlayerPrefs.SetInt("CURR_LEVELS_PLAYED", GetCurrLevelsPlayed() + 1);
        levelFailScreen.SetActive(false);
        levelWinScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        mainMenu.SetActive(true);
        setPrevPosOfDraggables();
    }

    public void RestartLevel()
    {
        levelFailScreen.SetActive(false);
        levelWinScreen.SetActive(false);
        gamePlayScreen.SetActive(true);
        setPrevPosOfDraggables();
    }

    public void MainMenu()
    {
        levelFailScreen.SetActive(false);
        levelWinScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        mainMenu.SetActive(true);
        setPrevPosOfDraggables();
    }

    void setPrevPosOfDraggables()
    {
        for (int i = 0; i < bottomBarObjs.Length; i++)
        {
            bottomBarObjs[i].GetComponent<DragableItem>().ResetPositions();
        }
    }

    public void CheckForLevelFail()
    {
        if (bottomBarPar.childCount != 0)
        {
            return;
        }

        levelFailScreen.SetActive(true);
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

        CalculateResult();
    }

    void CalculateResult()
    {
        switch (currGameplayState)
        {
            case GameplayState.ADDITION:
                result = value1 + value2;
                break;
            case GameplayState.SUBTRACTION:
                if (value1 > value2)
                {
                    result = value1 - value2;
                }
                else
                {
                    result = value2 - value1;
                }
                break;
            case GameplayState.MULTIPLICATION:
                result = value1 * value2;
                break;
            case GameplayState.DIVISION:
                if (value2 != 0)
                {
                    result = value1 / value2;
                }
                else
                {
                    result = value2 / value1;
                }
                break;
        }
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
        operatorText.text = currOperator;
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