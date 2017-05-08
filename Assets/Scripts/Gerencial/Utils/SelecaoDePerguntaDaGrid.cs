using UnityEngine;

public class SelecaoDePerguntaDaGrid : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<PerguntaView>().AtualizaPerguntaSelecionada(gameObject.name);
    }
}
