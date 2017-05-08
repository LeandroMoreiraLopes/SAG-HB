using UnityEngine;

public class SelecaoDeAlunoDaGrid : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<AlunoView>().AtualizaAlunoSelecionado(gameObject.name);
    }
}
