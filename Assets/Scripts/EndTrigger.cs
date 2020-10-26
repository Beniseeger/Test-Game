using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            Invoke("triggerLevelEnding", 0.35f);
        }
    }

    private void triggerLevelEnding()
    {
        gameController.completeLevel();
    }
}
