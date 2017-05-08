using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecaoDoAlunoParaAvaliacao : MonoBehaviour {

    public void Seleciona()
    {
        if (transform.parent.name == "Grid - Alunos")
            gameObject.GetComponentInParent<AtualizaGridDosAlunos>().AtualizaAlunoDaEsquerda(gameObject.name);

        else if (transform.parent.name == "Grid - AlunosSelecionados")
            gameObject.GetComponentInParent<AtualizaGridDosAlunos>().AtualizaAlunoDaDireita(gameObject.name);
    }
}
