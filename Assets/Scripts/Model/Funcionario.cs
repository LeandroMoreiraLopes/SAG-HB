using UnityEngine;
using System.Collections;

public class Funcionario : Pessoa {

	int matricula;

	// Use this for initialization
	void Start () {
		
	}
	
	public int GetMatricula()
	{
		return matricula;
	}

	public void SetMatricula(int m)
	{
		matricula = m;
	}
}
