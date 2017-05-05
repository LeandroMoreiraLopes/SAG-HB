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
        Invoke("Apagar", 1f);

        switch(status)
        {
            case 0:
                statusLogin.text = "Usuário não encontrado";
                break;

            case 1:
                statusLogin.text = "Login de Aluno com sucesso";
                PlayerPrefs.SetInt("IdUltimoAlunoLogado", umCtrLogin.GetAlunoId(umAluno));
                main.MudarGameState(16, 1);
                break;
            case 2:
                statusLogin.text = "Login de Funcionario com sucesso";
                Funcionario umFuncionario = new Funcionario();
                umFuncionario.SetUsuario(usuarioIF.text);
                umFuncionario.SetSenha(senhaIF.text);
                PlayerPrefs.SetInt("IdUltimoFuncionarioLogado", umCtrLogin.GetFuncionarioId(umFuncionario));
                main.MudarGameState(2, 1);
                break;
        }
    }

    public void LogarBTN()
    {
        StartCoroutine(Logar());
    }

    void Apagar()
    {
        usuarioIF.text = "";
        senhaIF.text = "";
        statusLogin.text = "";
    }
}
