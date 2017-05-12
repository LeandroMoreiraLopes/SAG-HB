using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RelatorioFuncionarioView : MonoBehaviour {

    Main main;

    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    
    List<Avaliacao> avaliacoes;

    [SerializeField]
    RelatorioAvaliacaoDinamicGrid rADG;

    [SerializeField]
    InputField relatorio;

    [SerializeField]
    Dropdown alunosDD;

    public int selecionado;


    void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
        StartCoroutine(AtualizaGridAvaliacao());
    }

    //refazer
    /*
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

        main.MudarGameState(18, 0);
    }
    #endregion
    */
       
    void ApagarTudo()
    {
        relatorio.text = "";
        alunosDD.options.Clear();
    }

    public void AtualizaAvaliacaoSelecionada(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGridAvaliacao()
    {
        //carrega lista de alunos
        avaliacoes = cadastroAvaliacao.ListarTodosCompleto();

        //aguarda downlaod
        yield return avaliacoes;

        //passa a lista para a grid e preenche a mesma
        rADG.SetListaDeAvaliacoes(avaliacoes);
        rADG.Resize();
    }

    IEnumerator AtualizaDropDown()
    {
        List<Aluno> alunos = new List<Aluno>();

        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Carregar(umaAvaliacao);
        for (int i = 0; i < umaAvaliacao.GetAlunos().Count; i++)
        {
            alunos.Add(umaAvaliacao.GetAlunos()[i]);
        }
        
        yield return alunos;

        alunosDD.options.Clear();
        alunosDD.options.Add(new Dropdown.OptionData("Todos os alunos"));

        for (int i = 0; i < alunos.Count; i++)
        {
            if (alunos[i] != null)
            {
                alunosDD.options.Add(new Dropdown.OptionData(alunos[i].GetNomeCompleto()));
            }
        }
        alunosDD.captionText = alunosDD.captionText;


    }

    public void VoltaManterAvaliacao()
    {
        selecionado = 0;

        ApagarTudo();
        main.MudarGameState(17, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }

    /*int EncontrarMateriaNaDropDownTrazendoId(string s)
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
    }*/
}