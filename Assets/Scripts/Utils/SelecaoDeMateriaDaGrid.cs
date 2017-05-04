using UnityEngine;

public class SelecaoDeMateriaDaGrid : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<MateriaView>().AtualizaMateriaSelecionado(gameObject.name);
    }
}
