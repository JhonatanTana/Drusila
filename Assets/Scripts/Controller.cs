using UnityEngine;

public class Controller : MonoBehaviour
{

    public static Controller Instance;

    public int Vida;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void DefineVida(int vida) {
        Vida = vida;
    }

    public int RecuperaVida() {
        return Vida;
    }

    public void PerderVida() {
        Vida--;
    }

}
