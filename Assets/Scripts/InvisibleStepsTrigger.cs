using UnityEngine;
using UnityEngine.UI;

public class InvisibleStepsTrigger : MonoBehaviour
{
    private Score score;

    private bool triggeredAlready = false;

    private void OnTriggerEnter(Collider other)
    {


        if (triggeredAlready) return;

        if (other.gameObject.CompareTag("Player"))
        {

            gameObject.GetComponentInParent<MeshRenderer>().enabled = true;
            score = GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<Score>();
            score.scoreNumber = score.scoreNumber + 1;
            triggeredAlready = true;
        }
        
    }
}
