using UnityEngine;
using System.Collections;

public class Aluno : Pessoa {

	long matricula;
	
	public long GetMatricula()
	{
		return matricula;
	}

	public void SetMatricula(long m)
	{
		matricula = m;
	}
}
