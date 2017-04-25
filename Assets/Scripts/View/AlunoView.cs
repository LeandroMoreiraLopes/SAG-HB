using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.EventSystems;

public class AlunoView : MonoBehaviour {

    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();

    List<Aluno> alunos;

    [SerializeField] Text texto;

    [SerializeField]
    AlunoDinamicGrid aDG;

    [SerializeField]
    InputField mat, nome, nascimento, cpf, telefone, celular, usuario, senha, email;

    public int selecionado;

    Vector3 rotDestino;

    IEnumerator Start()
    {
        alunos = cadastroAluno.ListarTodos();
        
        yield return alunos;

        aDG.SetListaDeAlunos(alunos);
        aDG.Resize();

        
        Aluno a = new Aluno();
        a.SetMatricula(317);
        a.SetNomeCompleto("Doidão");
        a.SetNascimento(19810909);
        a.SetCpf("123");
        a.SetTelefone(321);
        a.SetCelular(9321);
        a.SetUsuario("doidin");
        a.SetSenha("111");
        a.SetEmail("doidin@uhu");

        cadastroAluno.Incluir(a);
       


    }

    public void Clique(string s)
    {
        selecionado = Int32.Parse(s);
    }

    public void EditAluno()
    {
       StartCoroutine(EditaAluno());
    }

    IEnumerator EditaAluno()
    {
        Aluno umAluno = new Aluno();
        umAluno.SetId(selecionado);
        cadastroAluno.Carregar(umAluno);

        yield return umAluno;

        mat.text = umAluno.GetMatricula().ToString();
        nome.text = umAluno.GetNomeCompleto();
        nascimento.text = FormatarData.Format(umAluno.GetNascimento());
        cpf.text = umAluno.GetCpf();
        telefone.text = umAluno.GetTelefone().ToString();
        celular.text = umAluno.GetCelular().ToString();
        usuario.text = umAluno.GetUsuario();
        senha.text = umAluno.GetSenha();
        email.text = umAluno.GetEmail();
        rotDestino = new Vector3(0, 90, 0);
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotDestino, 5 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rotDestino = Vector3.zero;
        }
    }


}
