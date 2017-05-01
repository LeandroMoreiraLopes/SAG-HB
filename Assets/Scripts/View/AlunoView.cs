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

    [SerializeField]
    Button ok, atualiza;

    public int selecionado;

    Vector3 rotDestino;


    void Start()
    {
        StartCoroutine(AtualizaGrid());
    }

    #region métodos para Editar aluno
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
        nascimento.text = FormatarData.FormatToString(umAluno.GetNascimento());
        cpf.text = umAluno.GetCpf();
        telefone.text = umAluno.GetTelefone().ToString();
        celular.text = umAluno.GetCelular().ToString();
        usuario.text = umAluno.GetUsuario();
        senha.text = umAluno.GetSenha();
        email.text = umAluno.GetEmail();
        rotDestino = new Vector3(0, 90, 0);
        atualiza.gameObject.SetActive(true);
    }

    public void AtualizaAluno()
    {
        Aluno umAluno = new Aluno();
        umAluno.SetId(selecionado);
        umAluno.SetMatricula(int.Parse(mat.text));
        umAluno.SetNomeCompleto(nome.text);
        umAluno.SetNascimento(FormatarData.FormatToInt(nascimento.text));
        umAluno.SetCpf(cpf.text);
        umAluno.SetTelefone(int.Parse(telefone.text));
        umAluno.SetCelular(int.Parse(celular.text));
        umAluno.SetUsuario(usuario.text);
        umAluno.SetSenha(senha.text);
        umAluno.SetEmail(email.text);

        cadastroAluno.Alterar(umAluno);
        
        StartCoroutine(AtualizaGrid());
        ApagarTudo();
        rotDestino = new Vector3(0, 0, 0);
    }
    #endregion

    #region métodos para Incluir aluno
    public void CriaAluno()
    {
        ApagarTudo();
        ok.gameObject.SetActive(true);
        rotDestino = new Vector3(0, 90, 0);
    }

    public void NovoAluno()
    {
        Aluno umAluno = new Aluno();
      
        umAluno.SetMatricula(int.Parse(mat.text));
        umAluno.SetNomeCompleto(nome.text);
        umAluno.SetNascimento(FormatarData.FormatToInt(nascimento.text));
        umAluno.SetCpf(cpf.text);
        umAluno.SetTelefone(int.Parse(telefone.text));
        umAluno.SetCelular(int.Parse(celular.text));
        umAluno.SetUsuario(usuario.text);
        umAluno.SetSenha(senha.text);
        umAluno.SetEmail(email.text);

        cadastroAluno.Incluir(umAluno);
        
        StartCoroutine(AtualizaGrid());
        ApagarTudo();
        rotDestino = new Vector3(0, 0, 0);
    }
    #endregion

    #region métodos para Excluir aluno
    public void ApagaAluno()
    {
        Aluno umAluno = new Aluno();

        umAluno.SetId(selecionado);

        cadastroAluno.Excluir(umAluno);

        StartCoroutine(AtualizaGrid());
    } 
    #endregion

    void Update()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotDestino, 5 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ApagarTudo();
            StartCoroutine(AtualizaGrid());
            rotDestino = Vector3.zero;
        }
    }

    void ApagarTudo()
    {
        ok.gameObject.SetActive(false);
        atualiza.gameObject.SetActive(false);
        mat.text = "";
        nome.text = "";
        nascimento.text = "";
        cpf.text = "";
        telefone.text = "";
        celular.text = "";
        usuario.text = "";
        senha.text = "";
        email.text = "";
    }

    public void Clique(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGrid()
    {
        //carrega lista de alunos
        alunos = cadastroAluno.ListarTodos();

        //aguarda downlaod
        yield return alunos;

        //passa a lista para a grid e preenche a mesma
        aDG.SetListaDeAlunos(alunos);
        aDG.Resize();
    }
}
