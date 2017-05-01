using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    GameObject cameraPrincipal;
    Vector3 rotCamDestino;
    Vector3 posCamDestino;

    enum GameState {intro, login, menuPrincipalFuncionario, menuPrincipalAluno, menuFuncionario, menuAluno, menuMateria, menuTema, menuAvaliacao, menuPerguntas};
    GameState myGameState = GameState.intro;

    [SerializeField]
    GameObject[] canvas, panels;

   	// Use this for initialization
	void Start () {
        cameraPrincipal = Camera.main.gameObject;
        MudarGameState(1, 2);
        posCamDestino = Vector3.zero;

    }
	
	// Update is called once per frame
	void Update () {
        
        cameraPrincipal.transform.eulerAngles = Vector3.Lerp(cameraPrincipal.transform.eulerAngles, rotCamDestino, 30*Time.deltaTime);
        cameraPrincipal.transform.position = Vector3.Lerp(cameraPrincipal.transform.position, posCamDestino, 1 * Time.deltaTime);

    }

    public void Sair()
    {
        Application.Quit();
    }

    IEnumerator MudarGameState(GameState gs, int delay)
    {
        yield return new WaitForSeconds(delay);
        switch (gs)
        {
            case GameState.intro:
                rotCamDestino = Vector3.up * 0;
                panels[0].SetActive(true);
                break;
            case GameState.login:
                rotCamDestino = Vector3.up * 0;
                panels[0].SetActive(false);
                panels[1].SetActive(true);
                break;
        }

        myGameState = gs;
    }

    public void MudarGameState(int gsIndex, int time)
    {
        StartCoroutine(MudarGameState((GameState)gsIndex, time));
    }
}
