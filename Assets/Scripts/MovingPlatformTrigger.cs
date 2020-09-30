using UnityEngine;
using UnityEngine.UI;

public class MovingPlatformTrigger : MonoBehaviour
{
    private CharacterController controller;
    private Score score;
    public GameObject Player;
    private bool triggeredAlready = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform.parent.transform;
        }

        if (triggeredAlready) return;

        if (other.gameObject.CompareTag("Player"))
        {
            
            score = GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<Score>();
            score.scoreNumber = score.scoreNumber + 1;
            triggeredAlready = true;
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
