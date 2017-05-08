using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class PerguntaView : MonoBehaviour {

    Main main;

    CtrCadastroPergunta cadastroPergunta = new CtrCadastroPergunta();
    CtrCadastroTema cadastroTema = new CtrCadastroTema();
    CtrCadastroFuncionario cadastroFuncionario = new CtrCadastroFuncionario();

    List<Pergunta> perguntas;

    [SerializeField]
    PerguntaDinamicGrid pDG;

    [SerializeField]
    InputField descricao, correta, errada1, errada2, errada3, dificuldade, autor;

    [SerializeField]
    Dropdown tema;

    [SerializeField]
    Toggle simulado;

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

    #region métodos para Consultar perguntas
    public void ConsultarPergunta()
    {
        StartCoroutine(CarregaConsultaPergunta());
    }

    IEnumerator CarregaConsultaPergunta()
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

        descricao.interactable = false;
        correta.interactable = false;
        errada1.interactable = false;
        errada2.interactable = false;
        errada3.interactable = false;
        dificuldade.interactable = false;
        simulado.interactable = false;
        tema.interactable = false;
        autor.interactable = false;

        voltar.gameObject.SetActive(true);
        main.MudarGameState(12, 0);
    }
    #endregion

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

    #region métodos para Excluir pergunta
    public void TemCertezaPopUp()
    {
        if (selecionado != null && selecionado != 0)
        {
            excluirPopUp.SetActive(true);
        }
    }

    public void ApagaPerguntaDoBanco()
    {
        Pergunta umaPergunta = new Pergunta();

        umaPergunta.SetId(selecionado);
        cadastroPergunta.Excluir(umaPergunta);
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
        descricao.text = "";
        correta.text = "";
        errada1.text = "";
        errada2.text = "";
        errada3.text = "";
        dificuldade.text = "";
        simulado.isOn = false;
        tema.options = new List<Dropdown.OptionData>();
        autor.text = "";
    }

    public void AtualizaPerguntaSelecionada(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGrid()
    {
        //carrega lista de alunos
        perguntas = cadastroPergunta.ListarTodos();

        //aguarda downlaod
        yield return perguntas;

        //passa a lista para a grid e preenche a mesma
        pDG.SetListaDePerguntas(perguntas);
        pDG.Resize();
    }

    IEnumerator AtualizaDropDown()
    {
        List<Tema> temas = new List<Tema>();
        temas = cadastroTema.ListarTodos();

        yield return temas;

        tema.options.Clear();

        for (int i = 0; i < temas.Count; i++)
        {
            if (temas[i] != null)
            {
                tema.options.Add(new Dropdown.OptionData(temas[i].GetNome()));
            }
        }
        tema.captionText = tema.captionText;


    }

    public void VoltaManterPergunta()
    {
        descricao.interactable = true;
        correta.interactable = true;
        errada1.interactable = true;
        errada2.interactable = true;
        errada3.interactable = true;
        dificuldade.interactable = true;
        simulado.interactable = true;
        tema.interactable = true;

        ApagarTudo();
        main.MudarGameState(11, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }

    int EncontrarTemaNaDropDownTrazendoId(string s)
    {
        int mat_id = 0;

        List<Tema> listaDeTemas = new List<Tema>();
        listaDeTemas = cadastroTema.ListarTodos();

        for (int i = 0; i < listaDeTemas.Count; i++)
        {
            if (listaDeTemas[i].GetNome() == s)
                mat_id = listaDeTemas[i].GetId();
        }
        return mat_id;
    }

    int EncontrarTemaNaDropDownTrazendoValue(string s)
    {
        int mat_id = 0;

        List<Tema> listaDeTemas = new List<Tema>();
        listaDeTemas = cadastroTema.ListarTodos();

        for (int i = 0; i < listaDeTemas.Count; i++)
        {
            if (listaDeTemas[i].GetNome() == s)
                mat_id = i;
        }
        return mat_id;
    }
}