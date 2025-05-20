using UnityEngine;

public class Coletaveis : MonoBehaviour {
    
    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip somColeta;


    public bool VidaExtra;
    public bool DanoExtra;
    public string ColetavelID; // <- ID único preenchido no Inspector

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


        //// Verifica se esse coletável já foi coletado anteriormente
        //if (ColetavelManager.Instance != null && ColetavelManager.Instance.FoiColetado(ColetavelID)) {
        //    gameObject.SetActive(false); // Some se já foi pego
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player")) {


            if (somColeta != null && audioSource != null) {
                AudioSource.PlayClipAtPoint(somColeta, transform.position);
            }

            animator.SetBool("coleta", true);

            if (VidaExtra)
                Controller.Instance.GanharVida();
            else if (DanoExtra)
                Jogador.Instance.GanharDano();

            // Registra que foi coletado
            //ColetavelManager.Instance.RegistrarColetavel(ColetavelID);

            Destroy(gameObject); // Ou você pode usar: gameObject.SetActive(false);
        }
    }
}
