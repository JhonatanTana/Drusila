using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip somClique;



    public void RestartGame() {

        audioSource.PlayOneShot(somClique);

        int nVida = Dificuldades.Instance.RecuperaVidaDefinida();
        Controller.Instance.DefineVida(nVida);

        StartCoroutine(CarregarCenaComDelay("SCENE 1", somClique.length));
    }

    public void QuitGame() {
        audioSource.PlayOneShot(somClique);

        StartCoroutine(CarregarCenaComDelay("Inicio", somClique.length));
    }

    private System.Collections.IEnumerator CarregarCenaComDelay(string nomeCena, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nomeCena);
    }
}
