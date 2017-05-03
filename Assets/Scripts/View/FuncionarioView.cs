using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class FuncionarioView : MonoBehaviour
{

    Main main;

    CtrCadastroFuncionario cadastroFuncionario = new CtrCadastroFuncionario();

    List<Funcionario> funcionarios;

    [SerializeField]
    FuncionarioDinamicGrid fDG;

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

    #region métodos para Consultar funcionario
    public void ConsultarFuncionario()
    {
        StartCoroutine(CarregaConsultaFuncionario());
    }

    IEnumerator CarregaConsultaFuncionario()
    {
        Funcionario umFuncionario = new Funcionario();
        umFuncionario.SetId(selecionado);
        cadastroFuncionario.Carregar(umFuncionario);

        yield return umFuncionario;

        mat.text = umFuncionario.GetMatricula().ToString();
        nome.text = umFuncionario.GetNomeCompleto();
        nascimento.text = FormatarData.FormatToString(umFuncionario.GetNascimento());
        cpf.text = umFuncionario.GetCpf();
        telefone.text = umFuncionario.GetTelefone().ToString();
        celular.text = umFuncionario.GetCelular().ToString();
        usuario.text = umFuncionario.GetUsuario();
        senha.text = umFuncionario.GetSenha();
        email.text = umFuncionario.GetEmail();

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
        main.MudarGameState(6, 0);
    }
    #endregion

    #region métodos para Editar funcionario
    public void EditaFuncionario()
    {
        StartCoroutine(PreencheCamposParaEditarFuncionario());
    }

    IEnumerator PreencheCamposParaEditarFuncionario()
    {
        Funcionario umFuncionario = new Funcionario();
        umFuncionario.SetId(selecionado);
        cadastroFuncionario.Carregar(umFuncionario);

        yield return umFuncionario;

        mat.text = umFuncionario.GetMatricula().ToString();
        nome.text = umFuncionario.GetNomeCompleto();
        nascimento.text = FormatarData.FormatToString(umFuncionario.GetNascimento());
        cpf.text = umFuncionario.GetCpf();
        telefone.text = umFuncionario.GetTelefone().ToString();
        celular.text = umFuncionario.GetCelular().ToString();
        usuario.text = umFuncionario.GetUsuario();
        senha.text = umFuncionario.GetSenha();
        email.text = umFuncionario.GetEmail();
        atualiza.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(6, 0);
    }

    public void AtualizaFuncionarioNoBanco()
    {
        Funcionario umFuncionario = new Funcionario();
        umFuncionario.SetId(selecionado);
        umFuncionario.SetMatricula(int.Parse(mat.text));
        umFuncionario.SetNomeCompleto(nome.text);
        umFuncionario.SetNascimento(FormatarData.FormatToInt(nascimento.text));
        umFuncionario.SetCpf(cpf.text);
        umFuncionario.SetTelefone(int.Parse(telefone.text));
        umFuncionario.SetCelular(int.Parse(celular.text));
        umFuncionario.SetUsuario(usuario.text);
        umFuncionario.SetSenha(senha.text);
        umFuncionario.SetEmail(email.text);

        cadastroFuncionario.Alterar(umFuncionario);

        StartCoroutine(AtualizaGrid());
        VoltaManterFuncionario();
    }
    #endregion

    #region métodos para Incluir funcionario
    public void NovoFuncionario()
    {
        ApagarTudo();
        criar.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(6, 0);
    }

    public void CriarFuncionarioNoBanco()
    {
        Funcionario umFuncionario = new Funcionario();

        umFuncionario.SetMatricula(int.Parse(mat.text));
        umFuncionario.SetNomeCompleto(nome.text);
        umFuncionario.SetNascimento(FormatarData.FormatToInt(nascimento.text));
        umFuncionario.SetCpf(cpf.text);
        umFuncionario.SetTelefone(int.Parse(telefone.text));
        umFuncionario.SetCelular(int.Parse(celular.text));
        umFuncionario.SetUsuario(usuario.text);
        umFuncionario.SetSenha(senha.text);
        umFuncionario.SetEmail(email.text);

        cadastroFuncionario.Incluir(umFuncionario);

        StartCoroutine(AtualizaGrid());
        VoltaManterFuncionario();
    }
    #endregion

    #region métodos para Excluir funcionario
    public void TemCertezaPopUp()
    {
        if (selecionado != null && selecionado != 0)
        {
            excluirPopUp.SetActive(true);
        }
    }

    public void ApagaFuncionarioDoBanco()
    {
        Funcionario umFuncionario = new Funcionario();

        umFuncionario.SetId(selecionado);
        cadastroFuncionario.Excluir(umFuncionario);
        excluirPopUp.SetActive(false);
        StartCoroutine(AtualizaGrid());

    }

    public void NaoTemCertezaPopUp()
    {
        excluirPopUp.SetActive(false);
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
    }

    IEnumerator AtualizaGrid()
    {
        //carrega lista de alunos
        funcionarios = cadastroFuncionario.ListarTodos();

        //aguarda downlaod
        yield return funcionarios;

        //passa a lista para a grid e preenche a mesma
        fDG.SetListaDeFuncionarios(funcionarios);
        fDG.Resize();
    }

    public void AtualizaFuncionarioSelecionado(string s)
    {
        selecionado = Int32.Parse(s);
    }

    public void VoltaManterFuncionario()
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
        main.MudarGameState(5, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }
}
