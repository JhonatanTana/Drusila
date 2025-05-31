using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool jogoPausado = false;

    public GameObject menuPauseUI; // arraste aqui o painel de pause do Canvas
    public AudioSource audioSource;
    public AudioClip somClique;

    void Update() {
        // Checa se o jogador apertou ESC
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (jogoPausado) {
                Retomar();
            }
            else {
                Pausar();
            }
        }
    }

    public void Retomar() {
        audioSource.PlayOneShot(somClique);
        menuPauseUI.SetActive(false);
        Time.timeScale = 1f; // Volta ao normal
        jogoPausado = false;
    }

    void Pausar() {
        menuPauseUI.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
        jogoPausado = true;
    }

    // Botão de sair para o menu (opcional)
    public void CarregarMenu() {
        audioSource.PlayOneShot(somClique);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");
    }

    // Botão de sair do jogo (opcional)
    public void SairDoJogo() {
        audioSource.PlayOneShot(somClique);
        Application.Quit();
    }

    private System.Collections.IEnumerator CarregarCenaComDelay(string nomeCena, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nomeCena);
    }
}
