using UnityEngine;
using UnityEngine.UI;

public class StepsTrigger : MonoBehaviour
{
    private Score score;
    public Text text;

    private bool triggeredAlready = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredAlready) return;

        if (other.gameObject.CompareTag("Player"))
        {
            score = text.GetComponent<Score>();
            score.scoreNumber = score.scoreNumber + 1;
            triggeredAlready = true;
        }
        
    }
}
