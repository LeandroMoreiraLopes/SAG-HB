using UnityEngine;
using System.Collections;

public class Funcionario : Pessoa {

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
