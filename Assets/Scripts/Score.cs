using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int scoreNumber = 0;
    private int previousScore = 0;
    public Animator anim;
    public Animator imageAnim;
    public AudioSource scoreAudio;
    // Start is called before the first frame update
    void Start()
    {
        //score.GetComponentInChildren<Renderer>().enabled = false;
        previousScore = 0;
        scoreNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(previousScore < scoreNumber)
        {
            //score.GetComponentInChildren<Renderer>().enabled = true;
            anim.SetBool("scoreIncreased", true);
            previousScore = scoreNumber;
            imageAnim.SetBool("Increased", true);
            scoreAudio.Play();

        } else
        {
            anim.SetBool("scoreIncreased", false);
            imageAnim.SetBool("Increased", false);
            //score.GetComponentInChildren<Renderer>().enabled = false;
        }

        scoreText.text = scoreNumber.ToString();

    }
}
