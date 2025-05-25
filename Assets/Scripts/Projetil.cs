using UnityEngine;

public class Projetil : MonoBehaviour
{
    public int dano = 1; // Dano que o projetil vai causar

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            Controller.Instance.PerderVida();

            // Destroi o projetil após causar dano
            Destroy(gameObject);
        }
        //else if (!other.CompareTag("Player") && !other.CompareTag("Boss")) // exemplo de destruição ao colidir com algo
        //{
        //    Debug.Log(other.tag.ToString());
        //    Destroy(gameObject);
        //}
    }
}
