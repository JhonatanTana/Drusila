using UnityEngine;

public class AreaDeTiro : MonoBehaviour
{
    public Boss bossScript;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            bossScript.podeAtirar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            bossScript.podeAtirar = false;
        }
    }
}
