using UnityEngine;

public class Jogador : MonoBehaviour
{
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
    public string enemyTag = "Enemy";         // Tag dos inimigos (adicione "Enemy" nos seus inimigos)

    void Awake() {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        rig = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        Movimentar();
        Pular();

        if (Input.GetMouseButtonDown(0)) { // 0 é o botão esquerdo do mouse
            Attack();
        }
    }

    void Movimentar() {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f) {

            //anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0f) {

            //anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);

        }
        else {

            //anim.SetBool("Walk", false);
        }
    }

    void Pular() {

        if (Input.GetButtonDown("Jump")) {

            if (!IsJumping) {

                rig.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                DoubleJump = true;
                //anim.SetBool("Jump", true);
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
            //anim.SetBool("Jump", false);
        }

    }

    void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.layer == 8) {

            IsJumping = true;
        }
    }

    void Attack() {
        // Toca a animação de ataque
        //anim.SetTrigger("ataque");

        // Detecta todos os objetos em um círculo no ponto de ataque
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Verifica se os objetos encontrados possuem a tag "Enemy"
        foreach (Collider2D obj in hitObjects) {
            if (obj.CompareTag(enemyTag)) {
                obj.GetComponent<Enemies>().TakeDamage(damage);
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
}
