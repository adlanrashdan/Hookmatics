using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Forge_HookablesManager : HookablesManager
{
    public Transform hookableSpawn;
    public Transform operatorHookableSpawn;
    
    private int randomIndex;
    private List<double> numbers;
    private List<char> operators;
    private int questionIndex;
    private int operatorIndex;
   
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else {
            print("There is already an instance of HookablesManager");
        }
        hookables = new List<GameObject>();
    }


    public override void SpawnHookables(){
        questionIndex = 0;
        operatorIndex = 0;
        randomIndex = UnityEngine.Random.Range(1, MathManager.instance.mathQuestions.Length-1);
        SeparateEquation(MathManager.instance.question);
        // print the numbers in the equation
       
        InvokeRepeating("spawnHookable", spawnTime, spawnTime/numbers.Count);
        
        Invoke("cancelNumberSpawning", spawnTime *2);
        
    }
    private void cancelNumberSpawning(){
        CancelInvoke("spawnHookable");
        Invoke("spawnOperatorHookables",0);
    }
    void spawnOperatorHookables(){
        InvokeRepeating("spawnOperatorHookable", spawnTime/2, (spawnTime/2)/operators.Count);
        Invoke("CancelInvoke", spawnTime);
    }
    protected override void spawnHookable(){
        Vector3 spawnPosition = new Vector3(hookableSpawn.position.x , hookableSpawn.position.y, hookableSpawn.position.z);
        spawnPosition += hookableSpawn.forward * hookables.Count * spawnDistance;
        hookables.Add(Instantiate(hookable,  spawnPosition , Quaternion.identity));
        AssignHookableValues();
    }
    void spawnOperatorHookable(){
        Vector3 spawnPosition = new Vector3(operatorHookableSpawn.position.x , operatorHookableSpawn.position.y, operatorHookableSpawn.position.z);
        spawnPosition += operatorHookableSpawn.forward * (hookables.Count-numbers.Count) * spawnDistance;
        hookables.Add(Instantiate(hookable, spawnPosition , Quaternion.identity));
        AssignOperatorHookableValues();
    }

    protected override void AssignHookableValues(){
        
        hookables[questionIndex].GetComponentInChildren<TMPro.TextMeshPro>().text = numbers[questionIndex].ToString();
        questionIndex++;
        print("random"+randomIndex);
        print(hookables[hookables.Count - 1]);
        
    }
    void AssignOperatorHookableValues(){
        
        hookables[questionIndex + operatorIndex].GetComponentInChildren<TMPro.TextMeshPro>().text = operators[operatorIndex].ToString();
        operatorIndex++;
        print("random"+randomIndex);
        print(hookables[hookables.Count - 1]);
        
    }
    public void SeparateEquation(string equation)
    {
        numbers = new List<double>();
        operators = new List<char>();
        //remove spaces
        equation = equation.Replace(" ", "");
        print("equation: "+equation);
        // Split the equation into tokens based on operators and numbers
        string[] tokens = equation.Split(new char[] { '+', '-', '*', '/', '(', ')', ' ' ,'^' }, StringSplitOptions.RemoveEmptyEntries);

        // Iterate over the tokens and separate numbers and operators
        foreach (string token in tokens)
        {
            
            if (double.TryParse(token, out double number))
            {
                numbers.Add(number);
            }
        }
        // Iterate over the tokens and separate numbers and operators
        foreach (char token in equation)
        {
            
            if (token == '+' || token == '-' || token == '*' || token == '/' || token == '^')
            {
                operators.Add(token);
            }
        }
    }



}
