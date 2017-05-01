using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    GameObject cameraPrincipal;
    [SerializeField]
    Transform[] camDestino;
    int indice;
   
    enum GameState {intro, login, menuPrincipalFuncionario, menuPrincipalAluno, menuAluno, menuPainelDoAluno, menuFuncionario, menuMateria, menuTema, menuAvaliacao, menuPerguntas};
    GameState myGameState = GameState.intro;

    [SerializeField]
    GameObject[] canvas, panels;

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

    public void Sair()
    {
        Application.Quit();
    }

    public void Logout()
    {
        MudarGameState(1, 0);
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
                indice = 0;
                panels[0].SetActive(false);
                panels[1].SetActive(true);
                break;
            case GameState.menuPrincipalFuncionario:
                indice = 1;
                break;
            case GameState.menuPrincipalAluno:
                indice = 4;
                break;
            case GameState.menuAluno:
                indice = 2;
                break;
            case GameState.menuPainelDoAluno:
                indice = 3;
                break;
        }

        myGameState = gs;
    }

    public void MudarGameState(int gsIndex, int time)
    {
        StartCoroutine(MudarGameState((GameState)gsIndex, time));
    }
}
