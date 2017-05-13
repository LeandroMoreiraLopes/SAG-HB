using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainHB : MonoBehaviour {

    [SerializeField]
    CameraController controladorDaCamera;

    int id_aluno, id_avaliacao;

    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();
    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    CtrCadastroTema cadastroTema = new CtrCadastroTema();
    CtrCadastroPergunta cadastroPergunta = new CtrCadastroPergunta();

    Aluno aluno = new Aluno();
    Avaliacao avaliacao = new Avaliacao();
    List<Tema> temas = new List<Tema>();
    List<List<Pergunta>> perguntas = new List<List<Pergunta>>();

    [SerializeField]
    Text boasVindasTXT;

    [SerializeField]
    GameObject estatisticas;

    int hora, minuto, segundo;
    int dataRealizacao;

    TimeController timeController;
    AllBattlesController battleController;

    // Use this for initialization
    IEnumerator Start () {
        timeController = GetComponent<TimeController>();
        battleController = GetComponent<AllBattlesController>();

        id_aluno = PlayerPrefs.GetInt("IdUltimoAlunoLogado");
        id_avaliacao = PlayerPrefs.GetInt("IdAvaliacaoVigente");

        aluno.SetId(id_aluno);
        cadastroAluno.Carregar(aluno);

        avaliacao.SetId(id_avaliacao);
        cadastroAvaliacao.Carregar(avaliacao);

        temas = cadastroTema.ListarTodosPorAvaliacao(id_avaliacao);
        yield return temas;

        for (int i = 0; i < temas.Count; i++)
        {
            perguntas.Add(cadastroPergunta.ListarTodosPorTema(temas[i].GetId()));
        }

        battleController.SetTemas(temas);

        BoasVindas();


    }

    void BoasVindas()
    {
        string s = "Bem vindo {0},\n\nVocê irá fazer sua avaliação {1} da matéria {2}, com os temas:\n{3}\n Boa sorte e boa avaliação!";
        string temasTXT = "";
        for (int i = 0; i < temas.Count; i++)
        {
            if (i == temas.Count - 1)
            temasTXT += temas[i].GetNome() + ".\n";

            else temasTXT += temas[i].GetNome() + ",\n";
        }
        s = string.Format(s, aluno.GetNomeCompleto(), avaliacao.GetDescricao(), avaliacao.GetMateria().GetNome(), temasTXT);
        boasVindasTXT.text = s;
    }

    public void InicarJogo()
    {
        //definindo a data de realizacao da avaliacao
        DateTime hoje = DateTime.Now;
        string dataHoje = hoje.ToShortDateString();
        string[] dh = dataHoje.Split('/');

        if (dh[1].Length == 1) dh[1] = "0" + dh[1];
        if (dh[0].Length == 1) dh[0] = "0" + dh[0];
        
        dataHoje = dh[2] + dh[0] + dh[1];
        dataRealizacao = int.Parse(dataHoje);

        controladorDaCamera.SetPosicionamento(1);
        battleController.CriarBatalhas(temas, perguntas);
        timeController.IniciarContagem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            battleController.ChamandoPerguntaDasBatalhasAtivas();
        }
    }

    public void FinalizarJogo(int tema1certo, int tema2certo, int tema3certo, int tema4certo, int tema1total, int tema2total,
        int tema3total, int tema4total)
    {
        cadastroAvaliacao.GuardarResultadoAvaliacao(id_aluno, id_avaliacao, dataRealizacao, tema1certo, tema2certo, tema3certo, 
            tema4certo, tema1total, tema2total, tema3total, tema4total);

        estatisticas.SetActive(true);
        estatisticas.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Tema: " + temas[0].GetNome();
        estatisticas.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "Total de perguntas: " + tema1total +
            "\nTotal de acertos: " + tema1certo + "\nDesempenho: " + ((int)tema1certo*100/tema1total).ToString();
        if (temas.Count >= 2 && tema2total != 0)
        {
            estatisticas.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Tema: " + temas[1].GetNome();
            estatisticas.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "Total de perguntas: " + tema2total +
                "\nTotal de acertos: " + tema2certo + "\nDesempenho: " + ((int)tema2certo*100/ tema2total).ToString();
        }
        if (temas.Count >= 3 && tema3total != 0)
        {
            estatisticas.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Tema: " + temas[2].GetNome();
            estatisticas.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().text = "Total de perguntas: " + tema3total +
                "\nTotal de acertos: " + tema3certo + "\nDesempenho: " + ((int)tema3certo*100/tema3total).ToString();
        }
        if (temas.Count >= 4 && tema4total != 0)
        {
            estatisticas.transform.GetChild(7).gameObject.GetComponent<Text>().text = "Tema: " + temas[3].GetNome();
            estatisticas.transform.GetChild(6).GetChild(0).gameObject.GetComponent<Text>().text = "Total de perguntas: " + tema4total +
                "\nTotal de acertos: " + tema4certo + "\nDesempenho: " + ((int)tema4certo*100/tema4total).ToString();
        }


    }    
}
