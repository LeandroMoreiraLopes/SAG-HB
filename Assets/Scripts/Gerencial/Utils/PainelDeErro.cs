using UnityEngine;
using UnityEngine.UI;

public class PainelDeErro : MonoBehaviour {
    [SerializeField]
    Text textoDeErro;

    [SerializeField]
    GameObject painel;

    public void MensagemDeErro(string error)
    {
        painel.SetActive(true);
        textoDeErro.text = error;
    }
}
