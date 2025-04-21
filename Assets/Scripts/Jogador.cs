using UnityEngine;

public class Jogador : MonoBehaviour
{
    public float Speed;
    public float jump;
    public bool IsJumping;
    public bool DoubleJump;
    private Rigidbody2D rig;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        rig = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        Movimentar();
        Pular();
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
}
