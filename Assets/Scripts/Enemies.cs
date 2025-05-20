using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour {

    [Header("Configurações de Movimento")]
    public float moveSpeed = 2f;
    public Transform leftLimit;
    public Transform rightLimit;
    private bool movingRight = true;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Config de Vida")]
    public int health = 1;
    public bool dying = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        Patrol();
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            dying = true;
            animator.SetBool("morte", true);
            Invoke("Die", 0.5f); // 1 segundo de delay para a animação rodar
        }
    }

    // Movimentação de patrulha
    private void Patrol() {
        if (movingRight) {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

            // Verifica se chegou ao limite direito
            if (transform.position.x >= rightLimit.position.x) {
                movingRight = false;
                Flip();
            }
        }
        else {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);

            // Verifica se chegou ao limite esquerdo
            if (transform.position.x <= leftLimit.position.x) {
                movingRight = true;
                Flip();
            }
        }
    }

    // Inverte o sprite quando muda de direção
    private void Flip() {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // Detecta colisão física
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            Controller.Instance.PerderVida();

            if (Controller.Instance.RecuperaVida() > 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    void Die() {
        // Aqui você pode colocar animação de morte antes de destruir
        Destroy(gameObject);
    }
}
