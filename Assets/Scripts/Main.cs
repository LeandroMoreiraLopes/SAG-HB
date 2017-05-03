using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    GameObject cameraPrincipal;
    [SerializeField]
    Transform[] camDestino;
    int indice;
   
    enum GameState {intro, login, menuPrincipalFuncionario, menuManterAluno, menuDadosDoAluno, menuManterFuncionario, menuDadosDoFuncionario,
        menuManterMateria, menuDadosDaMateria, menuManterTema, menuDadosDoTema, menuManterAvaliacao, menuDadosDaAvaliacao, menuManterPerguntas,
        menuDadosDaPergunta, menuRelatoriosDoFuncionario, menuPrincipalAluno, menuAvaliacaoAluno, menuRelatoriosDoAluno};
    GameState myGameState = GameState.intro;

    [SerializeField]
    GameObject[] panels;

   	// Use this for initialization
	void Start () {
        cameraPrincipal = Camera.main.gameObject;
        MudarGameState(1, 1);
        
    }
	
	// Update is called once per frame
	void Update () {
        
        cameraPrincipal.transform.eulerAngles = Vector3.Lerp(cameraPrincipal.transform.eulerAngles, camDestino[indice].eulerAngles, 30*Time.deltaTime);
        cameraPrincipal.transform.position = Vector3.Lerp(cameraPrincipal.transform.position, camDestino[indice].position, 10 * Time.deltaTime);

    }

    public void FecharApplicacao()
    {
        Application.Quit();
    }

    public void Sair()
    {
        MudarGameState(1, 0);
    }

    //função para mudar de tela
    public void MudarGameState(int gsIndex, int time)
    {
        StartCoroutine(MudarGameState((GameState)gsIndex, time));
    }

    IEnumerator MudarGameState(GameState gs, int delay)
    {
        yield return new WaitForSeconds(delay);
        switch (gs)
        {
            case GameState.intro:
                indice = 0;
                panels[0].SetActive(true);
                break;
            case GameState.login:
                indice = 1;
                panels[0].SetActive(false);
                panels[1].SetActive(true);
                break;
            case GameState.menuPrincipalFuncionario:
                indice = 2;
                break;
            case GameState.menuManterAluno:
                indice = 3;
                break;
            case GameState.menuDadosDoAluno:
                indice = 4;
                break;
            case GameState.menuManterFuncionario:
                indice = 5;
                break;
            case GameState.menuDadosDoFuncionario:
                indice = 6;
                break;
            case GameState.menuManterMateria:
                indice = 7;
                break;
            case GameState.menuDadosDaMateria:
                indice = 8;
                break;
            case GameState.menuPrincipalAluno:
                indice = 16;
                break;
        }

        myGameState = gs;
    }
}
