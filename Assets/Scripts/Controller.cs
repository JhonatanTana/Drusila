using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    public Animator anim;  // Referência pública
    public int Vida;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        // Se quiser usar GetComponentInChildren como fallback:
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }

    public void DefineVida(int vida) {
        Vida = vida;
    }

    public int RecuperaVida() {
        return Vida;
    }

    public void PerderVida() {
        Vida--;

        if (Vida > 0) {
            anim.SetTrigger("Dano");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else {
            anim.SetTrigger("Morte");
            //SceneManager.LoadScene("GameOver");
        }
    }

    public void GanharVida() {
        Vida++;
    }
}
