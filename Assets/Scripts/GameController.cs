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
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayLosingMusic();

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
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayWinningMusic();
        completeLevelUI.SetActive(true);
    }
}
