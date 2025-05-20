using System.Collections.Generic;
using UnityEngine;

public class ColetavelManager : MonoBehaviour {
    public static ColetavelManager Instance;
    private HashSet<string> coletados = new HashSet<string>();

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else {
            Destroy(gameObject); // Evita duplicatas
        }
    }

    public void RegistrarColetavel(string id) {
        if (!coletados.Contains(id))
            coletados.Add(id);
    }

    public bool FoiColetado(string id) {
        return coletados.Contains(id);
    }
}
