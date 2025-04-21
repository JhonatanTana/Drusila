using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame() {
        int nVida = Dificuldades.Instance.RecuperaVidaDefinida();
        Controller.Instance.DefineVida(nVida);
        SceneManager.LoadScene("Nivel1");
    }

    public void QuitGame() {
        SceneManager.LoadScene("Inicio");
    }
}
