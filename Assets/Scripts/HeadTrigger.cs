using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    private void Start() {
        // Pega o Animator no objeto pai (Enemy)
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.tag);
        if (other.CompareTag("Player") && !isDead) {
            isDead = true;

            if (animator != null) {
                animator.SetBool("morte", true); // Troca para a animação de morte
            }

            // Desativa a colisão para evitar múltiplas chamadas
            Collider2D[] colliders = transform.parent.GetComponentsInChildren<Collider2D>();
            foreach (var collider in colliders) {
                collider.enabled = false;
            }

            // Espera o tempo da animação para destruir o inimigo
            float destroyDelay = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(transform.parent.gameObject, destroyDelay);
        }
    }
}
