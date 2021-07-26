using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public List<KeyCode> SelectionInputOptions = new List<KeyCode>(); //Container for potential keys to be used to interact with systems
    public float InputCooldown; // Cooldown before individual input actions can take place.
    
    private bool SpaceIsPressed;

    public CoreGameManager GameManagerReference;
    private KeyCode TradeKey; //Holds the key to interact with the active trade. TradeKey should be generated using a function/
    // Start is called before the first frame update
    private float InputTimer; //Tracks time since last input.

    
    void Start()
    {
        InputTimer = InputCooldown;
    }

    public void CheckPlayerInput()
    {
        if (GameManagerReference.GameStateMachine.bPhaseInProgress)
        {
            InputTimer -= Time.deltaTime;
            switch (GameManagerReference.GameStateMachine.CurrentPhase)
            {
                case 0:
                    // Contract Phase

                    break;
                case 1:
                    // TradePhase
                    foreach(OfferScriptableObject Offer in GameManagerReference.GameStateMachine.OfferList)
                    {
                        if (Input.GetKeyDown(Offer.TradeKey) && InputTimer <= 0)
                        {
                            GameManagerReference.GameStateMachine.ExecuteTradeTransaction(Offer);
                        }
                    }
                    

                    break;
                case 2:
                    // Order Phase

                    break;
                case 3:
                    // End Phase

                    break;
            }

            if (Input.GetKeyDown(KeyCode.Space) && InputTimer <= 0 && GameManagerReference.GameStateMachine.bPhaseInProgress)
            {
                print(InputTimer);
                GameManagerReference.GameStateMachine.EndCurrentPhase();
                InputTimer = InputCooldown;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Space) && GameManagerReference.GameStateMachine.StartGamePhase < 6)
                {
                    GameManagerReference.GameStateMachine.StartGame();
                    InputTimer = InputCooldown;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
