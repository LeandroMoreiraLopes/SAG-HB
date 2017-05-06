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
    AvaliacaoDinamicGrid aDG;

    [SerializeField]
    AtualizaGridDosTemas agT;

    [SerializeField]
    AtualizaGridDosAlunos agA;

    [SerializeField]
    InputField descricao, dataInicio, dataFim, autor;//, pesquisar;

    [SerializeField]
    Dropdown materiaDD;

    [SerializeField]
    Toggle simulado;

    [SerializeField]
    Button criar, atualiza, menu, addTema, subTema, addAluno, subAluno;

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
        addTema.interactable = false;
        subTema.interactable = false;
        addAluno.interactable = false;
        subAluno.interactable = false;

        menu.gameObject.SetActive(true);

        main.MudarGameState(14, 0);
    }
    #endregion
    
    #region métodos para Editar pergunta
    public void EditaAvaliacao()
    {
        StartCoroutine(PreencheCamposParaEditarAvaliacao());
    }

    IEnumerator PreencheCamposParaEditarAvaliacao()
    {

        //carrega a pergunta
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Carregar(umaAvaliacao);

        yield return umaAvaliacao;

        materiaDD.value = EncontrarMateriaNaDropDownTrazendoValue(umaAvaliacao.GetMateria().GetNome());

        List<Tema> temasDaMateria = new List<Tema>();
        temasDaMateria = cadastroTema.ListarTodosPorMateria(umaAvaliacao.GetMateria().GetId());
        yield return temasDaMateria;

        List<Tema> temasDaAvaliacao = new List<Tema>();
        temasDaAvaliacao = cadastroTema.ListarTodosPorAvaliacao(umaAvaliacao.GetId());
        yield return temasDaAvaliacao;

        List<Aluno> todosOsAlunos = new List<Aluno>();
        todosOsAlunos = cadastroAluno.ListarTodos();
        yield return todosOsAlunos;

        List<Aluno> alunos = new List<Aluno>();
        alunos = cadastroAluno.ListarTodosPorAvaliacao(umaAvaliacao.GetId());
        yield return alunos;

        

        //Populando os campos
        descricao.text = umaAvaliacao.GetDescricao();
        dataInicio.text = FormatarData.FormatToString(umaAvaliacao.GetDataInicio());
        dataFim.text = FormatarData.FormatToString(umaAvaliacao.GetDataFim());
        autor.text = umaAvaliacao.GetFuncionarioAutor().GetNomeCompleto();
        simulado.isOn = umaAvaliacao.GetSimulado();

        StartCoroutine(AtualizaDropDown());
        
        agT.AtualizaGrid(temasDaMateria, temasDaAvaliacao);

        agA.AtualizaGrid(todosOsAlunos, alunos);

        atualiza.gameObject.SetActive(true);

        main.MudarGameState(14, 0);
    }

    public void AtualizaAvaliacaoNoBanco()
    {
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        umaAvaliacao.SetDescricao(descricao.text);
        umaAvaliacao.SetDataInicio(FormatarData.FormatToInt(dataInicio.text));
        umaAvaliacao.SetDataFim(FormatarData.FormatToInt(dataFim.text));
        umaAvaliacao.SetSimulado(simulado.isOn);
        
        Materia umaMateria = new Materia();
        umaMateria.SetId(EncontrarMateriaNaDropDownTrazendoId(materiaDD.options[materiaDD.value].text));
        cadastroMateria.Carregar(umaMateria);
        umaAvaliacao.SetMateria(umaMateria); //banco com materia no id = 2

        Funcionario umFuncionario = new Funcionario();
        umFuncionario.SetId(PlayerPrefs.GetInt("IdUltimoFuncionarioLogado"));
        cadastroFuncionario.Carregar(umFuncionario);
        umaAvaliacao.SetFuncionarioAutor(umFuncionario);

        List<int> idsDosTemas = new List<int>();
        idsDosTemas = agT.GetIDsDosTemasSelecionados();
        List<Tema> listaDeTemasSelecionados = new List<Tema>();
        for (int i = 0; i < idsDosTemas.Count; i++)
        {
            Tema umTema = new Tema();
            umTema.SetId(idsDosTemas[i]);
            cadastroTema.Carregar(umTema);
            listaDeTemasSelecionados.Add(umTema);
        }

        List<int> idsDosAlunos = new List<int>();
        idsDosTemas = agA.GetIDsDosAlunosSelecionados();
        List<Aluno> listaDeAlunosSelecionados = new List<Aluno>();
        for (int i = 0; i < idsDosAlunos.Count; i++)
        {
            Aluno umAluno = new Aluno();
            umAluno.SetId(idsDosAlunos[i]);
            cadastroAluno.Carregar(umAluno);
            listaDeAlunosSelecionados.Add(umAluno);
        }


        cadastroAvaliacao.Alterar(umaAvaliacao);

        StartCoroutine(AtualizaGridAvaliacao());
        VoltaManterAvaliacao();
    }
    #endregion

    #region métodos para Incluir avaliacao
    public void NovaAvaliacao()
    {
        ApagarTudo();
        criar.gameObject.SetActive(true);
        main.MudarGameState(14, 0);
        StartCoroutine(AtualizaDropDown());
    }

    public void CriarAvaliacaoNoBanco()
    {
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetDescricao(descricao.text);
        umaAvaliacao.SetDataInicio(FormatarData.FormatToInt(dataInicio.text));
        umaAvaliacao.SetDataFim(FormatarData.FormatToInt(dataFim.text));
        umaAvaliacao.SetSimulado(simulado.isOn);

        Materia umaMateria = new Materia();
        umaMateria.SetId(EncontrarMateriaNaDropDownTrazendoId(materiaDD.options[materiaDD.value].text));
        cadastroMateria.Carregar(umaMateria);
        umaAvaliacao.SetMateria(umaMateria); //banco com materia no id = 2

        Funcionario umFuncionario = new Funcionario();
        umFuncionario.SetId(PlayerPrefs.GetInt("IdUltimoFuncionarioLogado"));
        cadastroFuncionario.Carregar(umFuncionario);
        umaAvaliacao.SetFuncionarioAutor(umFuncionario);

        List<int> idsDosTemas = new List<int>();
        idsDosTemas = agT.GetIDsDosTemasSelecionados();
        List<Tema> listaDeTemasSelecionados = new List<Tema>();
        for (int i = 0; i < idsDosTemas.Count; i++)
        {
            Tema umTema = new Tema();
            umTema.SetId(idsDosTemas[i]);
            cadastroTema.Carregar(umTema);
            listaDeTemasSelecionados.Add(umTema);
        }

        List<int> idsDosAlunos = new List<int>();
        idsDosTemas = agA.GetIDsDosAlunosSelecionados();
        List<Aluno> listaDeAlunosSelecionados = new List<Aluno>();
        for (int i = 0; i < idsDosAlunos.Count; i++)
        {
            Aluno umAluno = new Aluno();
            umAluno.SetId(idsDosAlunos[i]);
            cadastroAluno.Carregar(umAluno);
            listaDeAlunosSelecionados.Add(umAluno);
        }


        cadastroAvaliacao.Incluir(umaAvaliacao);

        StartCoroutine(AtualizaGridAvaliacao());
        VoltaManterAvaliacao();
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
        cadastroAvaliacao.Carregar(umaAvaliacao);
        if (FormatarData.AntesDaDataInicial(umaAvaliacao.GetDataInicio()))
        {
            cadastroAvaliacao.Excluir(umaAvaliacao);
        }
        excluirPopUp.SetActive(false);
        StartCoroutine(AtualizaGridAvaliacao());
        selecionado = 0;
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
        menu.gameObject.SetActive(false);
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
        addTema.interactable = true;
        subTema.interactable = true;
        addAluno.interactable = true;
        subAluno.interactable = true;

        selecionado = 0;

        ApagarTudo();
        main.MudarGameState(13, 0);
    }

    public void IrParaTelaDeEdicaoDeAvaliacao(int i)
    {
        main.MudarGameState(i, 0);
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

    public void AtualizaGridDeTemasPorMateria()
    {
        StartCoroutine(GridDeTemasPorMateria());
    }

    IEnumerator GridDeTemasPorMateria()
    {
        List<Tema> temasDaMateria = new List<Tema>();
        temasDaMateria = cadastroTema.ListarTodosPorMateria(EncontrarMateriaNaDropDownTrazendoId(materiaDD.options[materiaDD.value].text));
        yield return temasDaMateria;

        agT.AtualizaGrid(temasDaMateria, new List<Tema>());
    }
}