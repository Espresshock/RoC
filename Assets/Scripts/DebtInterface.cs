using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebtInterface : MonoBehaviour
{

    public Text DebtCollector;
    public Text DebtCounter;

    private string[] DebtCollectorMessages = new string[]{"Still turning a profit I hope?", "How's the purse looking, buddy?", "Its that time again... Pay up!", "Show.. Me.. The.. Money!", "Same time tomorrow?"};
    
    private int previousmessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // void print debt collector message

    public void DebtCollectorMessage(){
       
        int message = Random.Range(0,5);
        while(message == previousmessage)
        {
            message = Random.Range(0,5);
        }
        DebtCollector.text = "The Debtor takes his cut:\n '" + DebtCollectorMessages[message] + "'";
        previousmessage = message;
    }

    // void update debt counter
    public void  UpdateDebtCounter(int CurrentDebt)
    {
        DebtCounter.text = "Remaining Debt: \n" + CurrentDebt.ToString();
    }



   
}
