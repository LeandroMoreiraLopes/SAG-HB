using UnityEngine;
using System.Collections;
using System;

public abstract class Pessoa {

	int id;
	string nomeCompleto;
	int nascimento;
	string cpf;
    long telefone, celular;
	string usuario, senha, email;


	public int GetId()
	{
		return id;
	}

	public void SetId(int i)
	{
		id = i;
	}

	public string GetNomeCompleto()
	{
		return nomeCompleto;
	}

	public void SetNomeCompleto(string n)
	{
		nomeCompleto = n;
	}

	public int GetNascimento()
	{
		return nascimento;
	}

	public void SetNascimento(int dt)
	{
		nascimento = dt;
	}

	public string GetCpf()
	{
		return cpf;
	}

	public void SetCpf(string c)
	{
		cpf = c;
	}

	public long GetTelefone()
	{
		return telefone;
	}

	public void SetTelefone(long t)
	{
		telefone = t;
	}

	public long GetCelular()
	{
		return celular;
	}

	public void SetCelular(long c)
	{
		celular = c;
	}

	public string GetUsuario()
	{
		return usuario;
	}

	public void SetUsuario(string u)
	{
		usuario = u;
	}

	public string GetSenha()
	{
		return senha;
	}

	public void SetSenha(string s)
	{
		senha = s;
	}
		
	public string GetEmail()
	{
		return email;
	}

	public void SetEmail(string e)
	{
		email = e;
	}
}
