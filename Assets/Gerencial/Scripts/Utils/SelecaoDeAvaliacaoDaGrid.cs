using UnityEngine;

public class SelecaoDeAvaliacaoDaGrid : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<AvaliacaoView>().AtualizaAvaliacaoSelecionada(gameObject.name);
    }
}
