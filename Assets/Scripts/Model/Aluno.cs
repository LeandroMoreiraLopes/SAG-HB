using UnityEngine;
using System.Collections;

public class Aluno : Pessoa {

	int matricula;
	
	public int GetMatricula()
	{
		return matricula;
	}

	public void SetMatricula(int m)
	{
		matricula = m;
	}
}
