using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HookablesManager : MonoBehaviour
{
    public static HookablesManager instance;
    public float spawnRate = 0.5f;
    public float spawnTime = 2.5f;
    public GameObject hookable;
    public float spawnDistance = 10f;
    //list of hookables
    public List<GameObject> hookables;
    private int randomIndex;
   
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

    void Start(){
        
        
    }

    public virtual void SpawnHookables(){
        randomIndex = Random.Range(1, 5);
        InvokeRepeating("spawnHookable", spawnTime, spawnRate);
        Invoke("CancelInvoke", 4f);
        Invoke("AssignHookableValues", 5f);
        
    }
    protected virtual void spawnHookable(){
        hookables.Add(Instantiate(hookable, new Vector3(transform.position.x + hookables.Count * spawnDistance, transform.position.y, transform.position.z), Quaternion.identity));
        AssignHookableValues();
    }
    public void DestroyAllHookables(){
        if(hookables.Count == 0){
            return;
        }
        foreach(GameObject hookable in hookables){
            Destroy(hookable);
        }
        hookables.Clear();
    }

    protected virtual void AssignHookableValues(){
        //get the Text mesh pro component and give it a random value
        // the first hookable is the correct answer
        print("random"+randomIndex);
        print(hookables[hookables.Count - 1]);
        
        if(hookables.Count == randomIndex){
            print("ANSWER" + MathManager.instance.answer.ToString());
            hookables[hookables.Count - 1].transform.GetChild(0).GetComponent<TextMeshPro>().text = MathManager.instance.answer.ToString();
            hookables[hookables.Count - 1].GetComponent<ScoreObject>().isCorrect = true; 
        } else {
        hookables[hookables.Count - 1].transform.GetChild(0).GetComponent<TextMeshPro>().text = GetRandomValues().ToString();}

    }
    private int GetRandomValues(){
        //generate values higher and lower than the answer
        int answer = int.Parse(MathManager.instance.answer);
        int randomOne = Random.Range(answer-answer/2,answer-1);
        int randomTwo = Random.Range(answer+1,answer+answer/2);
        //if the values are the same, generate a new one
        while(randomOne == randomTwo){
            randomTwo = Random.Range(answer+1,answer+answer/2);
        }
        //if the values are similar to the ones used in previous hookables, generate a new one
        foreach(GameObject hookable in hookables){
            if(hookable.transform.GetChild(0).GetComponent<TextMeshPro>().text == randomOne.ToString() || hookable.transform.GetChild(0).GetComponent<TextMeshPro>().text == randomTwo.ToString()){
                randomOne = randomOne - 1;
                randomTwo = randomTwo + 1;
            }
        }
        // return one of the values
        
        int randomIndex = Random.Range(0,1);
        if(randomIndex == 0){
            return randomOne;
        } else {
            return randomTwo;
        }

    }


    
}
