using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassandoResposta : MonoBehaviour {

    AllBattlesController controleDeBatalha;

	// Use this for initialization
	void Start () {
        controleDeBatalha = GameObject.FindGameObjectWithTag("Controlador").GetComponent<AllBattlesController>();
		
	}
	
    public void Seleciona()
    {
        controleDeBatalha.Respondendo(gameObject.name);
    }
}
