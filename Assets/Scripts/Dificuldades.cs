using UnityEngine;
using UnityEngine.SceneManagement;

public class Dificuldades : MonoBehaviour
{
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
        Controller.Instance.DefineVida(5);
        Instance.DefineVidaRecuperada(5);
        Iniciar();
    }

    public void Medio() {
        Controller.Instance.DefineVida(3);
        Instance.DefineVidaRecuperada(3);
        Iniciar();
    }

    public void Dificil() {
        Controller.Instance.DefineVida(1);
        Instance.DefineVidaRecuperada(1);
        Iniciar();
    }

    public void Voltar() {
        SceneManager.LoadScene("Inicio");
    }

    public void Iniciar() {
        SceneManager.LoadScene("Nivel1");
    }

    private void DefineVidaRecuperada(int vida) {
        VidaDefinida = vida;
    }

    public int RecuperaVidaDefinida() {
        return VidaDefinida;
    }
}
