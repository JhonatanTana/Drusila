using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour {
    public static Jogador Instance;
    public float Speed;
    public float jump;
    public bool IsJumping;
    public bool DoubleJump;
    private Rigidbody2D rig;
    private Animator anim;

    [Header("Configurações de Ataque")]
    public float attackRange = 0.5f; // Alcance do ataque
    public int damage = 1; // Dano causado
    public Transform attackPoint; // Ponto de ataque (você vai criar um vazio para isso)
    private string enemyTag = "Enemy";         // Tag dos inimigos (adicione "Enemy" nos seus inimigos)
    private string bossTag = "Boss";         // Tag dos inimigos (adicione "Enemy" nos seus inimigos)
    private int ataqueIndex = 0;
    private const int totalAtaques = 3;

    void Awake() {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        Movimentar();
        Pular();

        if (Input.GetAxis("Horizontal") > 0f) {
            anim.SetBool("Correndo", true);
        }
        else if (Input.GetAxis("Horizontal") < 0f) {
            anim.SetBool("Correndo", true);
        }
        else {
            anim.SetBool("Correndo", false);
        }

        if (Input.GetMouseButtonDown(0)) {

            anim.SetInteger("AtaqueIndex", ataqueIndex);
            anim.SetTrigger("Atacando");

            ataqueIndex = (ataqueIndex + 1) % totalAtaques;
            Attack();
        }

    }

    void Movimentar() {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f) {


            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0f) {

            transform.eulerAngles = new Vector3(0f, 180f, 0f);

        }
    }

    void Pular() {

        if (Input.GetButtonDown("Jump")) {

            if (!IsJumping) {

                rig.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                DoubleJump = true;
                anim.SetTrigger("Pulando");
                anim.SetBool("Pulo", true);
            }
            else {

                if (DoubleJump) {

                    rig.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    DoubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == 8) {

            IsJumping = false;
            anim.SetBool("Pulo", false);
        }

    }

    void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.layer == 8) {

            IsJumping = true;
        }
    }

    void Attack() {
        // Detecta todos os objetos no raio de ataque
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D obj in hitObjects) {
            if (obj.CompareTag(enemyTag)) {
                obj.GetComponent<Enemies>().TakeDamage(damage);
            }
            else if (obj.CompareTag(bossTag)) {
                obj.GetComponent<Boss>().TakeDamage(damage);
            }
        }
    }


    // Desenha o alcance do ataque no Editor para facilitar ajustes
    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void GanharDano() {
        damage++;
    }

    public void OnMorteAnimationComplete() {
        SceneManager.LoadScene("GameOver");
    }
}
