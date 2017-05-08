using UnityEngine;

public class SelecaoDeFuncionarioDaGrid : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<FuncionarioView>().AtualizaFuncionarioSelecionado(gameObject.name);
    }
}
