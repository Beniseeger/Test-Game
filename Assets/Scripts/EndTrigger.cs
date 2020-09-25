using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter()
    {
        Invoke("triggerLevelEnding", 0.75f);
    }

    private void triggerLevelEnding()
    {
        gameController.completeLevel();
    }
}
