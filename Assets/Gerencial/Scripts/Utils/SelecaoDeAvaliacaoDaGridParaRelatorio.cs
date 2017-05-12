using UnityEngine;

public class SelecaoDeAvaliacaoDaGridParaRelatorio : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<RelatorioFuncionarioView>().AtualizaAvaliacaoSelecionada(gameObject.name);
    }
}
