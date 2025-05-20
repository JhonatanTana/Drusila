using UnityEngine;
using UnityEngine.SceneManagement;

public class Dificuldades : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip somClique;

    private int VidaDefinida;

    public static Dificuldades Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void Facil() {
        audioSource.PlayOneShot(somClique);

        StartCoroutine(CarregarCenaComDelay(somClique.length, 5));
    }

    public void Medio() {
        audioSource.PlayOneShot(somClique);

        StartCoroutine(CarregarCenaComDelay(somClique.length, 3));
    }

    public void Dificil() {
        audioSource.PlayOneShot(somClique);

        StartCoroutine(CarregarCenaComDelay(somClique.length, 1));
    }

    private System.Collections.IEnumerator CarregarCenaComDelay(float delay, int vida) {
        yield return new WaitForSeconds(delay);

        Controller.Instance.DefineVida(vida);
        Instance.DefineVidaRecuperada(vida);
        Iniciar();
    }

    private System.Collections.IEnumerator CarregarCenaComDelay(string nomeCena, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nomeCena);
    }

    public void Voltar() {
        audioSource.PlayOneShot(somClique);

        StartCoroutine(CarregarCenaComDelay("Inicio",somClique.length));
        //SceneManager.LoadScene("Inicio");
    }

    public void Iniciar() {
        SceneManager.LoadScene("SCENE 1");
    }

    private void DefineVidaRecuperada(int vida) {
        VidaDefinida = vida;
    }

    public int RecuperaVidaDefinida() {
        return VidaDefinida;
    }
}
