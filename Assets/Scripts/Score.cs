using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int scoreNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreNumber.ToString();
    }
}
