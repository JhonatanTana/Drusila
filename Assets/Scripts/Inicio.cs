using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public void Iniciar() {
        SceneManager.LoadScene("Dificuldades");
    }

    public void Sair() {
        Application.Quit();
    }
}
