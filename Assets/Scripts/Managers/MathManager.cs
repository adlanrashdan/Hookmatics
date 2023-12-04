using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using System;

public class MathManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MathManager instance;
    public GameObject MathDisplay;
    public TextMeshPro EquationDisplay;
    public string[] mathQuestions;
    public string[] mathAnswers;
    public string[] forgeQuestions;
    public string[] forgeAnswers;
    public string answer;
    public string question;
    public List<string> equation = new List<string>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("There is already an instance of MathManager");
        }
        mathQuestions = new string[]{ "4 + 7",
    "9 - 3",
    "20 / 4",
    "12 x 5",
    "√16",
    "2 ^ 3",
    "88 / 4",
    "23 * 17",
    "35 - 18",
    "144 / 12",
    "2 ^ 5",
    "12 + 3 * 7",
    "63 / 9",
    "15 * 6",
    "48 - 29",
    "10^3",
    "37 + 82",
    "7^2",
    "132 / 11",
    "56 - 38",
    "3 ^ 4",
    "91 + 15",
    "99 / 3",
    "29 * 7",
    "72 - 53",
    "5 + 9",
    "18 / 3",
    "8 x 6",
    "√25",
    "2 ^ 4",
    "125 / 5",
    "21 * 14",
    "40 - 15",
    "256 / 16",
    "2 ^ 6",
    "15 + 4 * 6",
    "72 / 8",
    "14 * 7",
    "57 - 28",
    "7^3",
    "22 + 63",
    "6^2",
    "165 / 11",
    "62 - 47",
    "4 ^ 3",
    "107 + 24",
    "80 / 2",
    "34 * 9",
    "98 - 79",
    "6 * 8",
    "144 / 9",
    "3 ^ 3",
    "16 - 9",
    "2 ^ 7",
    "105 / 5",
    "19 * 13",
    "28 - 14",
    "64 / 4",
    "2 ^ 8",
    "18 + 5 * 3",
    "81 / 9",
    "16 * 8",
    "46 - 29",
    "8^3",
    "31 + 49",
    "9^2",
    "176 / 8",
    "51 - 36",
    "5 ^ 4",
    "92 + 18",
    "108 / 3",
    "23 * 9",
    "68 - 57",
    "11^2",
    "10 - 5 * 3 - 2*8",
    "√121",
    "2 ^ 9",
    "115 / 5",
    "17 * 11",
};
        mathAnswers = new string[]{
    "11",
    "6",
    "5",
    "60",
    "4",
    "8",
    "22",
    "391",
    "17",
    "12",
    "32",
    "33",
    "7",
    "90",
    "19",
    "1000",
    "119",
    "49",
    "12",
    "18",
    "81",
    "106",
    "33",
    "203",
    "19",
    "14",
    "6",
    "48",
    "5",
    "16",
    "25",
    "294",
    "25",
    "16",
    "64",
    "39",
    "9",
    "98",
    "29",
    "343",
    "85",
    "36",
    "15",
    "15",
    "64",
    "131",
    "40",
    "306",
    "19",
    "48",
    "16",
    "27",
    "7",
    "128",
    "21",
    "247",
    "14",
    "16",
    "256",
    "33",
    "9",
    "128",
    "17",
    "512",
    "80",
    "81",
    "22",
    "15",
    "-21",
     "625",
     "110",
     "36",
     "207","11","121","-14","11","512","23","187"

     };
        forgeQuestions = new string[] { "9+3*2", "5*4-2", "8/2+6", "7-3*2", "10/2+3", "2+3*4", "6*2-4", "8/2+2", "9-3*2", "10/2+2", "2+3*3", "6*2-2", "8/2+1", "9-3*1", "10/2+1", "2+3*2", "6*2-1", "8/2+0", "9-3*0", "10/2+0", "7/7+6", "5*12-3" };
        forgeAnswers = new string[] { "15", "18", "10", "1", "8", "14", "8", "6", "3", "7", "11", "10", "5", "6", "6", "8", "11", "4", "9", "5", "7", "57" };


    }
    private void UpdateEquationDisplay()
    {
        string equationString = "";
        foreach (string c in equation)
        {
            equationString += c;
        }
        EquationDisplay.text = equationString;
    }
    public void AddToEquation(string c)
    {
        equation.Add(c);
        print("Added " + c + " to equation");
        if (EvaluateEquation())
        {
            print("Correct!");
            SoundEffectManager.instance.PlaySoundEffect("HappySuccess");
            ScoreManager.instance.IncrementScore();

        }
        else
        {
            print("Wrong!");
        }
        UpdateEquationDisplay();
    }
    public void RemoveFromEquation()
    {
        if (equation.Count > 0)
        {
            equation.RemoveAt(equation.Count - 1);
            print("Removed last element from equation");
        }
        UpdateEquationDisplay();
    }
    public void ClearEquation()
    {
        equation.Clear();
        print("Cleared equation");
        UpdateEquationDisplay();
    }

    public bool EvaluateEquation()
    {
        string equationString = "";
        foreach (string c in equation)
        {
            equationString += c;
        }
        DataTable table = new DataTable();

        // Use the Compute method to evaluate the expression
        try
        {
            var result = table.Compute(equationString, "");
            return Convert.ToDouble(result) == double.Parse(answer);
        }
        catch (Exception e)
        {
            print("Error in equation");
            return false;
        }

        //parse the equation into operators and numbers and calculate the result

    }
    public void GenerateMath()
    {
        int randomIndex = UnityEngine.Random.Range(20, mathQuestions.Length);
        // pick a random question
        if (LevelManager.instance.currentLevel < mathQuestions.Length - 21)
        {

            randomIndex = UnityEngine.Random.Range(LevelManager.instance.currentLevel, mathQuestions.Length - (mathQuestions.Length - (LevelManager.instance.currentLevel + 20)));
        }

        question = mathQuestions[randomIndex];
        answer = mathAnswers[randomIndex].ToString();

        print(mathAnswers);
        if (GameManager.instance.mode == GameManager.Mode.Forge)
        {
            randomIndex = UnityEngine.Random.Range(0, forgeQuestions.Length);
            question = forgeQuestions[randomIndex];
            answer = forgeAnswers[randomIndex];
            MathDisplay.GetComponent<TextMeshPro>().text = answer;
        }
        else
        {
            MathDisplay.GetComponent<TextMeshPro>().text = question;
        }
    }
}
