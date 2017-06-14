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
    CtrCadastroPergunta cadastroPergunta = new CtrCadastroPergunta();

    List<Avaliacao> avaliacoes;
    List<AvaliacaoAluno> avaliacoesAlunos;
    List<Tema> temasDaAvaliacao;
    List<List<PerguntaDaAvaliacaoDoAluno>> perguntasDaAvaliacao = new List<List<PerguntaDaAvaliacaoDoAluno>>();
    List<List<Pergunta>> perguntasDosTemasAvaliacao = new List<List<Pergunta>>();

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

        //carrega todas as avaliacoes do aluno
        avaliacoesAlunos = cadastroAvaliacao.ListarDadosDaAvaliacaoAlunoPoAvaliacao(selecionado);

        yield return avaliacoesAlunos;

        //carrega todos os temas de uma avaliacao
        temasDaAvaliacao = cadastroAvaliacao.ListarTemasDeUmaAvaliacao(selecionado);

        yield return temasDaAvaliacao;

        //carregar todos os resultados das perguntas de todas as avaliacoes do aluno
        foreach (AvaliacaoAluno aa in avaliacoesAlunos)
        perguntasDaAvaliacao.Add(cadastroAvaliacao.ListarPerguntasDeUmaAvaliacao(aa.getId()));

        //carregar todas as perguntas de todos os temas da avaliacao
        foreach (Tema tema in temasDaAvaliacao)
            perguntasDosTemasAvaliacao.Add(cadastroPergunta.ListarTodosPorTema(tema.GetId()));

        int numeroTotalDeAlunos = umaAvaliacao.GetAlunos().Count;
        int numeroDeAlunosQueRealizaram = 0;

        //contagem de alunos que realizaram a avaliacao
        for (int i = 0; i < avaliacoesAlunos.Count; i++)
        {
            if (avaliacoesAlunos[i].GetDataRealizacao() != 0)
                numeroDeAlunosQueRealizaram++;
        }

        //contagem de resultados
        List<List<int>> resultados = new List<List<int>>();
        for (int t = 0; t < temasDaAvaliacao.Count; t++)
        {
            List<Pergunta> perguntasPorTema = perguntasDosTemasAvaliacao[t];
            
            //varrendo cada aluno
            for (int a = 0; a < numeroTotalDeAlunos; a++)
            {
                //varrendo todas as perguntas de um aluno
                for (int i = 0; i < perguntasDaAvaliacao[a].Count; i++)
                {
                    //varrendo as perguntas de um tema
                    for (int j = 0; j < perguntasPorTema.Count; j++)
                    {
                        if (perguntasDaAvaliacao[a][i].getPerguntaId() == perguntasPorTema[j].GetId())
                        {
                            resultados[t][0]++;
                            if (perguntasDaAvaliacao[a][i].getCorreta())
                                resultados[t][1]++;
                        }
                    }
                }
            }
        }
        
        string rel = "";
        string relatorioCabecalho = "Descrição: {0}\n\nData de início: {1}       Data de término: {2}\n\nNúmero total de alunos para avaliação: " +
            "{3}\nAlunos que realizaram a avaliação: {4}\nAlunos que não realizaram a avaliação: {5}\n\n";
        relatorioCabecalho = string.Format(relatorioCabecalho, umaAvaliacao.GetDescricao(), FormatarData.FormatToString(umaAvaliacao.GetDataInicio()),
            FormatarData.FormatToString(umaAvaliacao.GetDataFim()), numeroTotalDeAlunos, numeroDeAlunosQueRealizaram, (numeroTotalDeAlunos - numeroDeAlunosQueRealizaram));

        string relatorioPorTema = "";
        for (int i = 0; i < temasDaAvaliacao.Count; i++)
        {
            string s = "Tema {0]: {1}\nTotal de perguntas: {2}\nTotal de acertos: {3}\nDesempenho: {4}%\n\n";
            float div = 100 * resultados[i][1] / resultados[i][0];
            string.Format(s, i + 1, temasDaAvaliacao[i].GetDescricao(), resultados[i][0], resultados[i][1], (int)div);
            relatorioPorTema += s;
        }
        
        string relatorioRodape = "Desempenho Geral: {0}%";

        int totalDeAcertos = 0, totalDePerguntas = 0;
        for (int i = 0; i < resultados.Count; i++)
        {
            totalDePerguntas += resultados[i][0];
            totalDeAcertos += resultados[i][1];
        }

        relatorioRodape = string.Format(relatorioRodape, (int)(totalDeAcertos * 100 / totalDePerguntas));

        rel += relatorioCabecalho;
        rel += relatorioPorTema;
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
        //carrega a pergunta
        Avaliacao umaAvaliacao = new Avaliacao();
        umaAvaliacao.SetId(selecionado);
        cadastroAvaliacao.Carregar(umaAvaliacao);

        yield return umaAvaliacao;

        //carrega o aluno
        Aluno umAluno = new Aluno();
        umAluno.SetId(EncontrarAlunoNaDropDownTrazendoId(alunosDD.options[alunosDD.value].text));
        cadastroAluno.Carregar(umAluno);
        yield return umAluno;

        //carrega a avaliacao do aluno
        AvaliacaoAluno umaAvaliacaoAluno = new AvaliacaoAluno();
        umaAvaliacaoAluno = cadastroAvaliacao.PegarDadosDaAvaliacaoAlunoDeUmAlunoEmUmaAvaliacao(umAluno.GetId(), selecionado);
        yield return umaAvaliacaoAluno;

        //carrega todos os temas de uma avaliacao
        temasDaAvaliacao = cadastroAvaliacao.ListarTemasDeUmaAvaliacao(selecionado);

        yield return temasDaAvaliacao;

        //carregar todos os resultados das perguntas de todas as avaliacoes do aluno
        List<PerguntaDaAvaliacaoDoAluno> perguntaDaAvaliacao = new List<PerguntaDaAvaliacaoDoAluno>();
        perguntaDaAvaliacao = cadastroAvaliacao.ListarPerguntasDeUmaAvaliacao(umaAvaliacaoAluno.getId());

        //carregar todas as perguntas de todos os temas da avaliacao
        foreach (Tema tema in temasDaAvaliacao)
            perguntasDosTemasAvaliacao.Add(cadastroPergunta.ListarTodosPorTema(tema.GetId()));

        //contagem de resultados
        List<List<int>> resultados = new List<List<int>>();
        for (int t = 0; t < temasDaAvaliacao.Count; t++)
        {
            List<Pergunta> perguntasPorTema = perguntasDosTemasAvaliacao[t];

            //varrendo todas as perguntas de um aluno
            for (int i = 0; i < perguntaDaAvaliacao.Count; i++)
            {
                //varrendo as perguntas de um tema
                for (int j = 0; j < perguntasPorTema.Count; j++)
                {
                    if (perguntaDaAvaliacao[i].getPerguntaId() == perguntasPorTema[j].GetId())
                    {
                        resultados[t][0]++;
                        if (perguntaDaAvaliacao[i].getCorreta())
                            resultados[t][1]++;
                    }
                }
            }
            
        }

        string rel = "";
        string relatorioCabecalho = "Descrição: {0}\n\nData de início: {1}       Data de término: {2}\n\nNome: " +
            "{3}\nMatríclua: {4}\n\n";
        relatorioCabecalho = string.Format(relatorioCabecalho, umaAvaliacao.GetDescricao(), FormatarData.FormatToString(umaAvaliacao.GetDataInicio()),
            FormatarData.FormatToString(umaAvaliacao.GetDataFim()), umAluno.GetNomeCompleto(), umAluno.GetMatricula());

        string relatorioPorTema = "";
        for (int i = 0; i < temasDaAvaliacao.Count; i++)
        {
            string s = "Tema {0]: {1}\nTotal de perguntas: {2}\nTotal de acertos: {3}\nDesempenho: {4}%\n\n";
            float div = 100 * resultados[i][1] / resultados[i][0];
            string.Format(s, i + 1, temasDaAvaliacao[i].GetDescricao(), resultados[i][0], resultados[i][1], (int)div);
            relatorioPorTema += s;
        }

        string relatorioRodape = "Desempenho Geral: {0}%\n\n";

        int totalDeAcertos = 0, totalDePerguntas = 0;
        for (int i = 0; i < resultados.Count; i++)
        {
            totalDePerguntas += resultados[i][0];
            totalDeAcertos += resultados[i][1];
        }

        relatorioRodape = string.Format(relatorioRodape, (int)(totalDeAcertos * 100 / totalDePerguntas));

        string relatorioComPerguntas = "";
        for (int i = 0; i < perguntaDaAvaliacao.Count; i++)
        {
            for (int j = 0; j < perguntasDosTemasAvaliacao.Count; j++)
            {
                for (int l = 0; l < perguntasDosTemasAvaliacao[j].Count; l++)
                {
                    if (perguntasDosTemasAvaliacao[j][l].GetId() == perguntaDaAvaliacao[i].getAvaliacaoId())
                    {
                        string s = "Pergunta: {0}\nResultado: {1}\n";
                        string resultado = (perguntaDaAvaliacao[i].getCorreta()) ? "correta" : "errada";
                        string.Format(s, perguntasDosTemasAvaliacao[j][l].GetDescricao(), resultado);

                        relatorioComPerguntas += s;
                    }
                }
            }
        }

        rel += relatorioCabecalho;
        rel += relatorioPorTema;
        rel += relatorioRodape;
        rel += relatorioComPerguntas;

        relatorio.text = rel;
    } 
    #endregion

    public void GerarPDF()
    {
        throw new ExcecaoSAG("Funcionalidade Indisponível");
    }
}