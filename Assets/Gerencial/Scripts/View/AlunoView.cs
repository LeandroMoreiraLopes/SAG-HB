using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AlunoView : MonoBehaviour {

    Main main;

    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();

    List<Aluno> alunos;

    [SerializeField]
    AlunoDinamicGrid aDG;

    [SerializeField]
    InputField mat, nome, nascimento, cpf, telefone, celular, usuario, senha, email;

    [SerializeField]
    Button criar, atualiza, voltar;

    [SerializeField]
    GameObject excluirPopUp;

    public int selecionado;


    void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
        StartCoroutine(AtualizaGrid());
    }

    #region métodos para Consultar aluno
    public void ConsultarAluno()
    {
        StartCoroutine(CarregaConsultaAluno());
    }

    IEnumerator CarregaConsultaAluno()
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
        
        mat.interactable = false;
        nome.interactable = false;
        nascimento.interactable = false;
        cpf.interactable = false;
        telefone.interactable = false;
        celular.interactable = false;
        usuario.interactable = false;
        senha.interactable = false;
        email.interactable = false;

        voltar.gameObject.SetActive(true);
        main.MudarGameState(4, 0);
    }
    #endregion
      
    #region métodos para Editar aluno
    public void EditaAluno()
    {
        StartCoroutine(PreencheCamposParaEditarAluno());
    }

    IEnumerator PreencheCamposParaEditarAluno()
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
        atualiza.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(4, 0);
    }

    public void AtualizaAlunoNoBanco()
    {
        Aluno umAluno = new Aluno();
        umAluno.SetId(selecionado);
        umAluno.SetMatricula(long.Parse(mat.text));
        umAluno.SetNomeCompleto(nome.text);
        umAluno.SetNascimento(FormatarData.FormatToInt(nascimento.text));
        umAluno.SetCpf(cpf.text);
        umAluno.SetTelefone(long.Parse(telefone.text));
        umAluno.SetCelular(long.Parse(celular.text));
        umAluno.SetUsuario(usuario.text);
        umAluno.SetSenha(senha.text);
        umAluno.SetEmail(email.text);

        cadastroAluno.Alterar(umAluno);
        
        StartCoroutine(AtualizaGrid());
        VoltaManterAluno();
    }
    #endregion

    #region métodos para Incluir aluno
    public void NovoAluno()
    {
        ApagarTudo();
        criar.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(4, 0);
    }

    public void CriarAlunoNoBanco()
    {
        Aluno umAluno = new Aluno();
      
        umAluno.SetMatricula(long.Parse(mat.text));
        umAluno.SetNomeCompleto(nome.text);
        umAluno.SetNascimento(FormatarData.FormatToInt(nascimento.text));
        umAluno.SetCpf(cpf.text);
        umAluno.SetTelefone(long.Parse(telefone.text));
        umAluno.SetCelular(long.Parse(celular.text));
        umAluno.SetUsuario(usuario.text);
        umAluno.SetSenha(senha.text);
        umAluno.SetEmail(email.text);

        cadastroAluno.Incluir(umAluno);
        
        StartCoroutine(AtualizaGrid());
        VoltaManterAluno();
    }
    #endregion

    #region métodos para Excluir aluno
    public void TemCertezaPopUp()
    {
        if (selecionado != null && selecionado != 0)
        {
            excluirPopUp.SetActive(true);
        }
    }

    public void ApagaAlunoDoBanco()
    {
        Aluno umAluno = new Aluno();

        umAluno.SetId(selecionado);
        cadastroAluno.Excluir(umAluno);
        excluirPopUp.SetActive(false);
        StartCoroutine(AtualizaGrid());
        selecionado = 0;

    }
    
    public void NaoTemCertezaPopUp()
    {
        excluirPopUp.SetActive(false);
        selecionado = 0;
    }

    #endregion

    void ApagarTudo()
    {
        criar.gameObject.SetActive(false);
        atualiza.gameObject.SetActive(false);
        voltar.gameObject.SetActive(false);
        mat.text = "";
        nome.text = "";
        nascimento.text = "";
        cpf.text = "";
        telefone.text = "";
        celular.text = "";
        usuario.text = "";
        senha.text = "";
        email.text = "";
        selecionado = 0;
    }

    public void AtualizaAlunoSelecionado(string s)
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

    public void VoltaManterAluno()
    {
        mat.interactable = true;
        nome.interactable = true;
        nascimento.interactable = true;
        cpf.interactable = true;
        telefone.interactable = true;
        celular.interactable = true;
        usuario.interactable = true;
        senha.interactable = true;
        email.interactable = true;

        ApagarTudo();
        main.MudarGameState(3, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }
}
