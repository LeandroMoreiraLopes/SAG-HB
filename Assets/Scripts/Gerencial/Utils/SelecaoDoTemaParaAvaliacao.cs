using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecaoDoTemaParaAvaliacao : MonoBehaviour {

    public void Seleciona()
    {
        if (transform.parent.name == "Grid - Temas") 
            gameObject.GetComponentInParent<AtualizaGridDosTemas>().AtualizaTemaDaEsquerda(gameObject.name);

        else if (transform.parent.name == "Grid - TemasSelecionados")
            gameObject.GetComponentInParent<AtualizaGridDosTemas>().AtualizaTemaDaDireita(gameObject.name);
    }
}
