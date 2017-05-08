using UnityEngine;

public class SelecaoDeSimuladoDisponivel : MonoBehaviour
{

    public void Seleciona()
    {
        gameObject.GetComponentInParent<SimuladoDisponivelView>().AtualizaSimuladoDisponivelSelecionada(gameObject.name);
    }
}
