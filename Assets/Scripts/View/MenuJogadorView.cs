using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuJogadorView : MonoBehaviour {

    Main main;

    private void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
    }

    public void IrParaAvaliacoesDisponiveis()
    {
        main.MudarGameState(19, 0);
    }

    public void IrParaSimuladosDisponiveis()
    {
        main.MudarGameState(20, 0);
    }

    public void IrParaRelatoriosDoAluno()
    {
        main.MudarGameState(21, 0);
    }
}
