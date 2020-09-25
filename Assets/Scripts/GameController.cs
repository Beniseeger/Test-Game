using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool gameActive = true;
    public GameObject completeLevelUI;

    public void EndGame()
    {
        if (gameActive)
        {
            gameActive = false;
            //Restart Game
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void completeLevel()
    {
        completeLevelUI.SetActive(true);
    }
}
