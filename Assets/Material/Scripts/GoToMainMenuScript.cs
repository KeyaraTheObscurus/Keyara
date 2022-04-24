using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenuScript : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}