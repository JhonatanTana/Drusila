using UnityEngine;

public class MusicaFundo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static MusicaFundo instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
