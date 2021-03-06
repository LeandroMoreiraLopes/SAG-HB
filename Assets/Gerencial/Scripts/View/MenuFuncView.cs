﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFuncView : MonoBehaviour {

    Main main;

    private void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
    }

    public void IrParaManterAluno()
    {
        main.MudarGameState(3, 0);
    }

    public void IrParaManterFuncionario()
    {
        main.MudarGameState(5, 0);
    }

    public void IrParaManterMateria()
    {
        main.MudarGameState(7, 0);
    }

    public void IrParaManterTema()
    {
        main.MudarGameState(9, 0);
    }

    public void IrParaManterPergunta()
    {
        main.MudarGameState(11, 0);
    }

    public void IrParaManterAvaliacao()
    {
        main.MudarGameState(13, 0);
    }

    public void IrParaRelatoriosDoFuncionario()
    {
        main.MudarGameState(17, 0);
    }
}
