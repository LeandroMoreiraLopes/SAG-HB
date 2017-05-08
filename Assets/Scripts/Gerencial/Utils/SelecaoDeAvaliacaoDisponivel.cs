using UnityEngine;

public class SelecaoDeAvaliacaoDisponivel : MonoBehaviour
{

    public void Seleciona()
    {
        gameObject.GetComponentInParent<AvaliacaoDisponivelView>().AtualizaAvaliacaoDisponivelSelecionada(gameObject.name);
    }
}
