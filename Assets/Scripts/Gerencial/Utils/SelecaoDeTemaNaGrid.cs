using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecaoDeTemaNaGrid : MonoBehaviour {

    public void Seleciona()
    {
        gameObject.GetComponentInParent<TemaView>().AtualizaTemaSelecionado(gameObject.name);
    }
}
