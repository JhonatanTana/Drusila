using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start() {
        Controller.Instance.RecuperaVida();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) // Certifique-se que o jogador tem a tag "Player"
        {
            Controller.Instance.PerderVida();

            if (Controller.Instance.RecuperaVida() > 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else {
                SceneManager.LoadScene("GameOver"); // Carrega a cena de Game Over
            }
        }
    }
}
