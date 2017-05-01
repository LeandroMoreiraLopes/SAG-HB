using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour {

    CtrLogin umCtrLogin = new CtrLogin();

    [SerializeField]
    Main main;

    [SerializeField]
    InputField usuarioIF, senhaIF;

    [SerializeField]
    Text statusLogin;

    IEnumerator Logar()
    {
        Aluno umAluno = new Aluno();
        umAluno.SetUsuario(usuarioIF.text);
        umAluno.SetSenha(senhaIF.text);

        int status = umCtrLogin.Logar(umAluno);
        yield return status;

        switch(status)
        {
            case 0:
                statusLogin.text = "Usuário não encontrado";
                break;

            case 1:
                statusLogin.text = "Login de Aluno com sucesso";
                main.MudarGameState(2, 2);
                break;
            case 2:
                statusLogin.text = "Login de Funcionario com sucesso";
                main.MudarGameState(2, 2);
                break;
        }
    }

    public void LogarBTN()
    {
        StartCoroutine(Logar());
    }
}
