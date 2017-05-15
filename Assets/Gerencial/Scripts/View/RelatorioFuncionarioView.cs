using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RelatorioFuncionarioView : MonoBehaviour
{

    Main main;

    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();

    List<Avaliacao> avaliacoes;
    List<AvaliacaoAluno> avaliacoesAlunos;

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
        
    }

    public void IrParaRelatorios()
    {
        StartCoroutine(AtualizaGridAvaliacoesFinalizadas());
    }

    #region métodos para Consultar avaliacoes
    public void ConsultarRelatorioDeAvaliacao()
    {
        StartCoroutine(CarregaConsultaRelatorioDeAvaliacao());
    }

    IEnumerator CarregaConsultaRelatorioDeAvaliacao()
    {
        //carrega a pergunta
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Carregar(umaAvaliacao);

        yield return umaAvaliacao;

        avaliacoesAlunos = cadastroAvaliacao.ListarDadosDaAvaliacaoAlunoPoAvaliacao(selecionado);

        yield return avaliacoesAlunos;

        int numeroTotalDeAlunos = umaAvaliacao.GetAlunos().Count;
        int numeroDeAlunosQueRealizaram = 0;

        int totalDePerguntas = 0;
        int totalDeAcertos = 0;
        int tema1Perguntas = 0;
        int tema1Acertos = 0;
        int tema2Perguntas = 0;
        int tema2Acertos = 0;
        int tema3Perguntas = 0;
        int tema3Acertos = 0;
        int tema4Perguntas = 0;
        int tema4Acertos = 0;

        for (int i = 0; i < avaliacoesAlunos.Count; i++)
        {
            if (avaliacoesAlunos[i].GetDataRealizacao() != 0)
                numeroDeAlunosQueRealizaram++;

            totalDePerguntas += avaliacoesAlunos[i].GetTema1TotalDePerguntas() + avaliacoesAlunos[i].GetTema2TotalDePerguntas() +
                avaliacoesAlunos[i].GetTema3TotalDePerguntas() + avaliacoesAlunos[i].GetTema4TotalDePerguntas();

            totalDeAcertos += avaliacoesAlunos[i].GetTema1TotalDeAcertos() + avaliacoesAlunos[i].GetTema2TotalDeAcertos() +
                avaliacoesAlunos[i].GetTema3TotalDeAcertos() + avaliacoesAlunos[i].GetTema4TotalDeAcertos();

            tema1Perguntas += avaliacoesAlunos[i].GetTema1TotalDePerguntas();
            tema1Acertos += avaliacoesAlunos[i].GetTema1TotalDeAcertos();

            tema2Perguntas += avaliacoesAlunos[i].GetTema2TotalDePerguntas();
            tema2Acertos += avaliacoesAlunos[i].GetTema2TotalDeAcertos();

            tema3Perguntas += avaliacoesAlunos[i].GetTema3TotalDePerguntas();
            tema3Acertos += avaliacoesAlunos[i].GetTema3TotalDeAcertos();

            tema4Perguntas += avaliacoesAlunos[i].GetTema4TotalDePerguntas();
            tema4Acertos += avaliacoesAlunos[i].GetTema4TotalDeAcertos();
        }

        string rel = "";
        string relatorioCabecalho = "Descrição: {0}\n\nData de início: {1}       Data de término: {2}\n\nNúmero total de alunos para avaliação: " +
            "{3}\nAlunos que realizaram a avaliação: {4}\nAlunos que não realizaram a avaliação: {5}\n\n";
        string relatorioTema1 = "Tema 1: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioTema2 = "Tema 2: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioTema3 = "Tema 3: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioTema4 = "Tema 4: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioRodape = "Desempenho Geral: {0}";

        relatorioCabecalho = string.Format(relatorioCabecalho, umaAvaliacao.GetDescricao(), FormatarData.FormatToString(umaAvaliacao.GetDataInicio()),
            FormatarData.FormatToString(umaAvaliacao.GetDataFim()), numeroTotalDeAlunos, numeroDeAlunosQueRealizaram, (numeroTotalDeAlunos - numeroDeAlunosQueRealizaram));
        rel += relatorioCabecalho;

        if (umaAvaliacao.GetTemas().Count >= 1)
        {
            relatorioTema1 = string.Format(relatorioTema1, umaAvaliacao.GetTemas()[0].GetNome(), tema1Perguntas, tema1Acertos, (int)(tema1Acertos * 100 / tema1Perguntas));
            rel += relatorioTema1;
        }
        if (umaAvaliacao.GetTemas().Count >= 2)
        {
            relatorioTema2 = string.Format(relatorioTema2, umaAvaliacao.GetTemas()[1].GetNome(), tema2Perguntas, tema2Acertos, (int)(tema2Acertos * 100 / tema2Perguntas));
            rel += relatorioTema2;
        }
        if (umaAvaliacao.GetTemas().Count >= 3)
        {
            relatorioTema3 = string.Format(relatorioTema3, umaAvaliacao.GetTemas()[2].GetNome(), tema3Perguntas, tema3Acertos, (int)(tema3Acertos * 100 / tema3Perguntas));
            rel += relatorioTema3;
        }
        if (umaAvaliacao.GetTemas().Count >= 4)
        {
            relatorioTema4 = string.Format(relatorioTema4, umaAvaliacao.GetTemas()[3].GetNome(), tema4Perguntas, tema4Acertos, (int)(tema4Acertos * 100 / tema4Perguntas));
            rel += relatorioTema4;
        }

        relatorioRodape = string.Format(relatorioRodape, (int)(totalDeAcertos * 100 / totalDePerguntas));
        rel += relatorioRodape;

        relatorio.text = rel;

        StartCoroutine(AtualizaDropDown());

        main.MudarGameState(18, 0);
    }
    #endregion

    void ApagarTudo()
    {
        relatorio.text = "";
        alunosDD.options.Clear();
        selecionado = 0;
    }

    public void AtualizaAvaliacaoSelecionada(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGridAvaliacoesFinalizadas()
    {
        //carrega lista de avaliações
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

    int EncontrarAlunoNaDropDownTrazendoId(string s)
    {
        int aluno_id = 0;

        List<Aluno> listaDeAlunosDaAvaliacao = new List<Aluno>();
        listaDeAlunosDaAvaliacao = cadastroAluno.ListarTodos();

        for (int i = 0; i < listaDeAlunosDaAvaliacao.Count; i++)
        {
            if (listaDeAlunosDaAvaliacao[i].GetNomeCompleto() == s)
                aluno_id = listaDeAlunosDaAvaliacao[i].GetId();
        }
        return aluno_id;
    }

    #region métodos Para Atualizar o Relatório
    public void SelecionaAlunoNaDropDownEExibeRelatorio()
    {
        if (alunosDD.value == 0)
        {
            ConsultarRelatorioDeAvaliacao();
        }
        else
            StartCoroutine(TrazendoDadosDoAlunoSelecionado());

    }

    IEnumerator TrazendoDadosDoAlunoSelecionado()
    {
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Carregar(umaAvaliacao);
        yield return umaAvaliacao;

        Aluno umAluno = new Aluno();
        umAluno.SetId(EncontrarAlunoNaDropDownTrazendoId(alunosDD.options[alunosDD.value].text));
        cadastroAluno.Carregar(umAluno);
        yield return umAluno;

        AvaliacaoAluno umaAvaliacaoAluno = new AvaliacaoAluno();
        umaAvaliacaoAluno = cadastroAvaliacao.PegarDadosDaAvaliacaoAlunoDeUmAlunoEmUmaAvaliacao(umAluno.GetId(), selecionado);
        yield return umaAvaliacaoAluno;

        string rel = "";
        string relatorioCabecalho = "Descrição: {0}\n\nData de início: {1}       Data de término: {2}\n\n";
        string relatorioTema1 = "Tema 1: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioTema2 = "Tema 2: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioTema3 = "Tema 3: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioTema4 = "Tema 4: {0}\nTotal de perguntas: {1}\nTotal de acertos: {2}\nDesempenho: {3}\n\n";
        string relatorioRodape = "Desempenho Geral:\nTotal de perguntas: {0}\nTotal de acertos: {1}\nNota: {2}\n\nRanking {3}";

        relatorioCabecalho = string.Format(relatorioCabecalho, umaAvaliacao.GetDescricao(), FormatarData.FormatToString(umaAvaliacao.GetDataInicio()),
            FormatarData.FormatToString(umaAvaliacao.GetDataFim()));
        rel += relatorioCabecalho;

        if (umaAvaliacao.GetTemas().Count >= 1)
        {
            relatorioTema1 = string.Format(relatorioTema1, umaAvaliacao.GetTemas()[0].GetNome(), umaAvaliacaoAluno.GetTema1TotalDePerguntas(),
                umaAvaliacaoAluno.GetTema1TotalDeAcertos(), (int)(umaAvaliacaoAluno.GetTema1TotalDeAcertos() * 100 / umaAvaliacaoAluno.GetTema1TotalDePerguntas()));
            rel += relatorioTema1;
        }
        if (umaAvaliacao.GetTemas().Count >= 2)
        {
            relatorioTema2 = string.Format(relatorioTema2, umaAvaliacao.GetTemas()[1].GetNome(), umaAvaliacaoAluno.GetTema2TotalDePerguntas(),
                umaAvaliacaoAluno.GetTema2TotalDeAcertos(), (int)(umaAvaliacaoAluno.GetTema2TotalDeAcertos() * 100 / umaAvaliacaoAluno.GetTema2TotalDePerguntas()));
            rel += relatorioTema2;
        }
        if (umaAvaliacao.GetTemas().Count >= 3)
        {
            relatorioTema3 = string.Format(relatorioTema3, umaAvaliacao.GetTemas()[2].GetNome(), umaAvaliacaoAluno.GetTema3TotalDePerguntas(),
                umaAvaliacaoAluno.GetTema3TotalDeAcertos(), (int)(umaAvaliacaoAluno.GetTema3TotalDeAcertos() * 100 / umaAvaliacaoAluno.GetTema3TotalDePerguntas()));
            rel += relatorioTema3;
        }
        if (umaAvaliacao.GetTemas().Count >= 4)
        {
            relatorioTema4 = string.Format(relatorioTema4, umaAvaliacao.GetTemas()[3].GetNome(), umaAvaliacaoAluno.GetTema4TotalDePerguntas(),
                umaAvaliacaoAluno.GetTema4TotalDeAcertos(), (int)(umaAvaliacaoAluno.GetTema4TotalDeAcertos() * 100 / umaAvaliacaoAluno.GetTema4TotalDePerguntas()));
            rel += relatorioTema4;
        }

        int ace = umaAvaliacaoAluno.GetTema1TotalDeAcertos() + umaAvaliacaoAluno.GetTema2TotalDeAcertos() + umaAvaliacaoAluno.GetTema3TotalDeAcertos() + umaAvaliacaoAluno.GetTema4TotalDeAcertos();
        int per = umaAvaliacaoAluno.GetTema1TotalDePerguntas() + umaAvaliacaoAluno.GetTema2TotalDePerguntas() + umaAvaliacaoAluno.GetTema3TotalDePerguntas() + umaAvaliacaoAluno.GetTema4TotalDePerguntas();

        relatorioRodape = string.Format(relatorioRodape,ace, per, (int)(ace * 100 / per), "indisponível");
        rel += relatorioRodape;

        relatorio.text = rel;
    } 
    #endregion

    public void GerarPDF()
    {
        throw new ExcecaoSAG("Funcionalidade Indisponível");
    }
}