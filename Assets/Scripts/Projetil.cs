using UnityEngine;

public class Projetil : MonoBehaviour
{
    public int dano = 1; // Dano que o projetil vai causar
    public float tempoDeVidaMinimo = 0.1f;
    private float tempoDeVida;

    void Start() {
        tempoDeVida = 0f;
    }

    void Update() {
        tempoDeVida += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (tempoDeVida < tempoDeVidaMinimo) return; // ainda no tempo de proteção

        if (other.CompareTag("Player")) {
            Controller.Instance.PerderVida();
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Boss")) {
            Destroy(gameObject);
        }
    }

    public void Destruir() {
        Destroy(gameObject);
    }
}
