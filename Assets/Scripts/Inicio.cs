using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip somClique;

    public void Iniciar() {
        // Toca o som
        audioSource.PlayOneShot(somClique);

        // Carrega a cena com um pequeno delay pra garantir que o som toque
        StartCoroutine(CarregarCenaComDelay("Dificuldades", somClique.length));
    }

    public void Sair() {
        audioSource.PlayOneShot(somClique);
        StartCoroutine(SairComDelay(somClique.length));
    }

    private System.Collections.IEnumerator CarregarCenaComDelay(string nomeCena, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nomeCena);
    }

    private System.Collections.IEnumerator SairComDelay(float delay) {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
