using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour {

    GameObject cameraPrincipal;
    [SerializeField]
    Transform[] camDestino;
    int indice;
   
    enum GameState {intro, login, menuPrincipalFuncionario, menuManterAluno, menuDadosDoAluno, menuManterFuncionario, menuDadosDoFuncionario,
        menuManterMateria, menuDadosDaMateria, menuManterTema, menuDadosDoTema, menuManterPergunta, menuDadosDaPergunta, menuManterAvaliacao,
        menuDadosDaAvaliacao, menuRelatoriosDoFuncionario, menuPrincipalAluno, menuAvaliacaoAluno, menuSimuladoAluno, menuRelatoriosDoAluno};
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
            case GameState.menuManterTema:
                indice = 9;
                break;
            case GameState.menuDadosDoTema:
                indice = 10;
                break;
            case GameState.menuManterPergunta:
                indice = 11;
                break;
            case GameState.menuDadosDaPergunta:
                indice = 12;
                break;
            case GameState.menuManterAvaliacao:
                indice = 13;
                break;
            case GameState.menuDadosDaAvaliacao:
                indice = 14;
                break;
            case GameState.menuRelatoriosDoFuncionario:
                indice = 15;
                break;
            case GameState.menuPrincipalAluno:
                indice = 16;
                break;
            case GameState.menuAvaliacaoAluno:
                indice = 17;
                break;
            case GameState.menuSimuladoAluno:
                indice = 18;
                break;
            case GameState.menuRelatoriosDoAluno:
                indice = 19;
                break;
        }

        myGameState = gs;
    }
}
