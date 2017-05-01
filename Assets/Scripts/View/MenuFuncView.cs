using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFuncView : MonoBehaviour {

    Main main;

    private void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
    }

    public void ManterAluno()
    {
        main.MudarGameState(4, 0);
    }
}
