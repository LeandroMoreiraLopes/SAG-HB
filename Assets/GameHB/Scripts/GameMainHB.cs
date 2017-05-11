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

    int hora, minuto, segundo;

    TimeController timeController;
    AllBattlesController battleController;

    [SerializeField]
    SingleBattleController singleBattle;

    Pergunta pergunta;

    // Use this for initialization
    void Start () {
        timeController = GetComponent<TimeController>();
        battleController = GetComponent<AllBattlesController>();

        id_aluno = PlayerPrefs.GetInt("IdUltimoAlunoLogado");
        id_avaliacao = PlayerPrefs.GetInt("IdAvaliacaoVigente");

        aluno.SetId(id_aluno);
        cadastroAluno.Carregar(aluno);

        avaliacao.SetId(id_avaliacao);
        cadastroAvaliacao.Carregar(avaliacao);

        temas = cadastroTema.ListarTodosPorAvaliacao(id_avaliacao);
        for (int i = 0; i < temas.Count; i++)
        {
            perguntas.Add(cadastroPergunta.ListarTodosPorTema(temas[i].GetId()));
        }

        BoasVindas();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pergunta = singleBattle.PegaPerguntaNaoFeita();

        }
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
        controladorDaCamera.SetPosicionamento(1);
        battleController.CriarBatalhas(temas, perguntas);
    }
}
