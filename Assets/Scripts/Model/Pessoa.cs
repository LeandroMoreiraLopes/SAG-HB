using UnityEngine;
using System.Collections;
using System;

public abstract class Pessoa {

	int id;
	string nomeCompleto;
	int nascimento;
	string cpf;
	int telefone, celular;
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

	public int GetTelefone()
	{
		return telefone;
	}

	public void SetTelefone(int t)
	{
		telefone = t;
	}

	public int GetCelular()
	{
		return celular;
	}

	public void SetCelular(int c)
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
