using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject projetilPrefab;
    public Transform pontoDeDisparo;
    public float velocidadeDoProjetil = 5f;
    public float tempoEntreTiros = 2f;

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

    private Transform jogador;
    private float tempoProximoTiro = 0f;
    private bool parandoParaAtirar = false;

    public bool podeAtirar = false;  // Controle para só atirar se estiver na área

    void Start() {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!parandoParaAtirar) {
            Patrol();
        }
        else {
            rb.linearVelocity = Vector2.zero;
        }

        // Só atira se o jogador estiver na área e for a hora certa
        if (podeAtirar && jogador != null && Time.time >= tempoProximoTiro) {
            StartCoroutine(AtirarDepoisDeParar(0.5f));
            tempoProximoTiro = Time.time + tempoEntreTiros;
        }
    }

    private void Patrol() {
        if (movingRight) {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            if (transform.position.x >= rightLimit.position.x) {
                movingRight = false;
                Flip();
            }
        }
        else {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            if (transform.position.x <= leftLimit.position.x) {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Flip() {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    System.Collections.IEnumerator AtirarDepoisDeParar(float tempoParado) {
        parandoParaAtirar = true;
        yield return new WaitForSeconds(tempoParado);
        AtirarNoJogador();
        parandoParaAtirar = false;
    }

    void AtirarNoJogador() {
        if (projetilPrefab == null || pontoDeDisparo == null) return;

        GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, Quaternion.identity);

        Vector2 direcao = (jogador.position - pontoDeDisparo.position).normalized;
        direcao.y = 0;  

        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direcao * velocidadeDoProjetil;
        rb.gravityScale = 0;

        // Calcula o ângulo para a rotação
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;

        // Faz o projetil rotacionar apenas no eixo Z
        projetil.transform.rotation = Quaternion.Euler(0, 0, angulo);

        Destroy(projetil, 5f);
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            //dying = true;
            parandoParaAtirar = true;
            animator.SetTrigger("morte");
            Invoke("Die", 0.5f); // 1 segundo de delay para a animação rodar
        } else {
            animator.SetTrigger("dano");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            Controller.Instance.PerderVida();
        }
    }

    void Die() {
        // Aqui você pode colocar animação de morte antes de destruir
        Destroy(gameObject);
    }
}
