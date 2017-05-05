using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AvaliacaoView : MonoBehaviour {

    Main main;

    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    CtrCadastroTema cadastroTema = new CtrCadastroTema();
    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();
    CtrCadastroFuncionario cadastroFuncionario = new CtrCadastroFuncionario();
    CtrCadastroMateria cadastroMateria = new CtrCadastroMateria();

    List<Avaliacao> avaliacoes;

    [SerializeField]
    GameObject[] paineis;

    [SerializeField]
    AvaliacaoDinamicGrid aDG;

    [SerializeField]
    AtualizaGridDosTemas agT;

    [SerializeField]
    AtualizaGridDosAlunos agA;

    [SerializeField]
    InputField descricao, dataInicio, dataFim, autor, pesquisar;

    [SerializeField]
    Dropdown materiaDD;

    [SerializeField]
    Toggle simulado;

    [SerializeField]
    Button criar, atualiza, proximo, voltar, add, sub;

    [SerializeField]
    GameObject excluirPopUp;

    public int selecionado;


    void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
        StartCoroutine(AtualizaGridAvaliacao());
    }

    #region métodos para Consultar avaliacoes
    public void ConsultarAvaliacao()
    {
        StartCoroutine(CarregaConsultaAvaliacao());
    }

    IEnumerator CarregaConsultaAvaliacao()
    {
        //carrega a pergunta
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Carregar(umaAvaliacao);

        yield return umaAvaliacao;

        List<Tema> temasDaMateria = new List<Tema>();
        temasDaMateria = cadastroTema.ListarTodosPorMateria(umaAvaliacao.GetMateria().GetId());
        yield return temasDaMateria;

        List<Tema> temasDaAvaliacao = new List<Tema>();
        temasDaAvaliacao = cadastroTema.ListarTodosPorAvaliacao(umaAvaliacao.GetId());
        yield return temasDaAvaliacao;

        agT.AtualizaGrid(temasDaMateria, temasDaAvaliacao);

        List<Aluno> todosOsAlunos = new List<Aluno>();
        todosOsAlunos = cadastroAluno.ListarTodos();
        yield return todosOsAlunos;

        List<Aluno> alunos = new List<Aluno>();
        alunos = cadastroAluno.ListarTodosPorAvaliacao(umaAvaliacao.GetId());
        yield return alunos;

        agA.AtualizaGrid(todosOsAlunos, alunos);

        //Populando os campos
        descricao.text = umaAvaliacao.GetDescricao();
        dataInicio.text = FormatarData.FormatToString(umaAvaliacao.GetDataInicio());
        dataFim.text = FormatarData.FormatToString(umaAvaliacao.GetDataFim());
        autor.text = umaAvaliacao.GetFuncionarioAutor().GetNomeCompleto();
        simulado.isOn = umaAvaliacao.GetSimulado();

        StartCoroutine(AtualizaDropDown());
        materiaDD.value = EncontrarMateriaNaDropDownTrazendoValue(umaAvaliacao.GetMateria().GetNome());

        descricao.interactable = false;
        dataInicio.interactable = false;
        dataFim.interactable = false;
        simulado.interactable = false;
        materiaDD.interactable = false;
        add.interactable = false;
        sub.interactable = false;

        voltar.gameObject.SetActive(true);
        main.MudarGameState(14, 0);
    }
    #endregion
    /*
    #region métodos para Editar pergunta
    public void EditaPergunta()
    {
        StartCoroutine(PreencheCamposParaEditarPergunta());
    }

    IEnumerator PreencheCamposParaEditarPergunta()
    {
        //carrega a pergunta
        Pergunta umaPergunta = new Pergunta();
        umaPergunta.SetId(selecionado);
        cadastroPergunta.Carregar(umaPergunta);

        yield return umaPergunta;

        //carrega o tema relativa a pergunta
        Tema umTema = new Tema();
        umTema.SetId(umaPergunta.GetTemaId());
        cadastroTema.Carregar(umTema);

        yield return umTema;

        //carrega o funcionario autor da pergunta
        Funcionario umFuncionario = new Funcionario();
        umFuncionario.SetId(umaPergunta.GetFuncId());
        cadastroFuncionario.Carregar(umFuncionario);

        yield return umFuncionario;

        //Populando os campos
        descricao.text = umaPergunta.GetDescricao();
        correta.text = umaPergunta.GetCorreta();
        errada1.text = umaPergunta.GetErrada1();
        errada2.text = umaPergunta.GetErrada2();
        errada3.text = umaPergunta.GetErrada3();
        dificuldade.text = umaPergunta.GetDificuldade().ToString();
        simulado.isOn = umaPergunta.GetSimulado(); //ver como fazer uma marcação

        StartCoroutine(AtualizaDropDown());
        tema.value = EncontrarTemaNaDropDownTrazendoValue(umTema.GetNome());

        autor.text = umFuncionario.GetNomeCompleto();

        atualiza.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);

        main.MudarGameState(12, 0);
    }

    public void AtualizaPerguntaNoBanco()
    {
        Pergunta umaPergunta = new Pergunta();
        umaPergunta.SetId(selecionado);
        umaPergunta.SetDescricao(descricao.text);
        umaPergunta.SetCorreta(correta.text);
        umaPergunta.SetErrada1(errada1.text);
        umaPergunta.SetErrada2(errada2.text);
        umaPergunta.SetErrada3(errada3.text);
        umaPergunta.SetDificuldade(Int32.Parse(dificuldade.text));
        umaPergunta.SetSimulado(simulado.isOn);
        umaPergunta.SetTemaId(EncontrarTemaNaDropDownTrazendoId(tema.options[tema.value].text)); //banco com materia no id = 2

        umaPergunta.SetFuncId(PlayerPrefs.GetInt("IdUltimoFuncionarioLogado"));

        cadastroPergunta.Alterar(umaPergunta);

        StartCoroutine(AtualizaGrid());
        VoltaManterPergunta();
    }
    #endregion

    #region métodos para Incluir pergunta
    public void NovoPergunta()
    {
        ApagarTudo();
        criar.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(12, 0);
        StartCoroutine(AtualizaDropDown());
    }

    public void CriarPerguntaNoBanco()
    {
        Pergunta umaPergunta = new Pergunta();
        umaPergunta.SetDescricao(descricao.text);
        umaPergunta.SetCorreta(correta.text);
        umaPergunta.SetErrada1(errada1.text);
        umaPergunta.SetErrada2(errada2.text);
        umaPergunta.SetErrada3(errada3.text);
        umaPergunta.SetDificuldade(Int32.Parse(dificuldade.text));
        umaPergunta.SetSimulado(simulado.isOn);
        umaPergunta.SetTemaId(EncontrarTemaNaDropDownTrazendoId(tema.options[tema.value].text)); //banco com materia no id = 2

        umaPergunta.SetFuncId(PlayerPrefs.GetInt("IdUltimoFuncionarioLogado"));

        cadastroPergunta.Incluir(umaPergunta);

        StartCoroutine(AtualizaGrid());
        VoltaManterPergunta();
    }
    #endregion

    #region métodos para Excluir avaliacao
    public void TemCertezaPopUp()
    {
        if (selecionado != 0)
        {
            excluirPopUp.SetActive(true);
        }
    }

    public void ApagaAvaliacaoDoBanco()
    {
        Avaliacao umaAvaliacao = new Avaliacao();

        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Excluir(umaAvaliacao);
        excluirPopUp.SetActive(false);
        StartCoroutine(AtualizaGridAvaliacao());

    }

    public void NaoTemCertezaPopUp()
    {
        excluirPopUp.SetActive(false);
    }

    #endregion
    */
    void ApagarTudo()
    {
        criar.gameObject.SetActive(false);
        atualiza.gameObject.SetActive(false);
        voltar.gameObject.SetActive(false);
        descricao.text = "";
        dataInicio.text = "";
        dataFim.text = "";
        simulado.isOn = false;
        simulado.isOn = false;
        materiaDD.options.Clear();
        autor.text = "";
    }

    public void AtualizaAvaliacaoSelecionada(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGridAvaliacao()
    {
        //carrega lista de alunos
        avaliacoes = cadastroAvaliacao.ListarTodos();

        //aguarda downlaod
        yield return avaliacoes;

        //passa a lista para a grid e preenche a mesma
        aDG.SetListaDeAvaliacoes(avaliacoes);
        aDG.Resize();
    }

    IEnumerator AtualizaDropDown()
    {
        List<Materia> materias = new List<Materia>();
        materias = cadastroMateria.ListarTodos();

        yield return materias;

        materiaDD.options.Clear();

        for (int i = 0; i < materias.Count; i++)
        {
            if (materias[i] != null)
            {
                materiaDD.options.Add(new Dropdown.OptionData(materias[i].GetNome()));
            }
        }
        materiaDD.captionText = materiaDD.captionText;


    }

    public void VoltaManterAvaliacao()
    {
        descricao.interactable = true;
        dataInicio.interactable = true;
        dataFim.interactable = true;
        simulado.interactable = true;
        materiaDD.interactable = true;
        add.interactable = true;
        sub.interactable = true;

        ApagarTudo();
        main.MudarGameState(13, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }

    int EncontrarMateriaNaDropDownTrazendoId(string s)
    {
        int mat_id = 0;

        List<Materia> listaDeMaterias = new List<Materia>();
        listaDeMaterias = cadastroMateria.ListarTodos();

        for (int i = 0; i < listaDeMaterias.Count; i++)
        {
            if (listaDeMaterias[i].GetNome() == s)
                mat_id = listaDeMaterias[i].GetId();
        }
        return mat_id;
    }

    int EncontrarMateriaNaDropDownTrazendoValue(string s)
    {
        int mat_id = 0;

        List<Materia> listaDeMaterias = new List<Materia>();
        listaDeMaterias = cadastroMateria.ListarTodos();

        for (int i = 0; i < listaDeMaterias.Count; i++)
        {
            if (listaDeMaterias[i].GetNome() == s)
                mat_id = i;
        }
        return mat_id;
    }
}