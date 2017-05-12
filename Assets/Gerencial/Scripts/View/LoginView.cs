using System.Collections;
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
        umAluno.SetUsuario(usuarioIF.text.ToLower());
        umAluno.SetSenha(senhaIF.text.ToLower());
        
        int status = umCtrLogin.Logar(umAluno);
        yield return status;
        Invoke("Apagar", 1f);

        switch(status)
        {
            case 0:
                statusLogin.color = Color.red;
                statusLogin.text = "Usuário não encontrado";
                break;

            case 1:
                statusLogin.color = Color.green;
                statusLogin.text = "Login de Aluno com sucesso";
                PlayerPrefs.SetInt("IdUltimoAlunoLogado", umCtrLogin.GetAlunoId(umAluno));
                main.MudarGameState(19, 1);
                break;
            case 2:
                statusLogin.color = Color.blue;
                statusLogin.text = "Login de Funcionario com sucesso";
                Funcionario umFuncionario = new Funcionario();
                umFuncionario.SetUsuario(usuarioIF.text.ToLower());
                umFuncionario.SetSenha(senhaIF.text.ToLower());
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
