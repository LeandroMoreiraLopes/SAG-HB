using System.Collections;
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
}
