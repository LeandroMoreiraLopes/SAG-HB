using UnityEngine;
using System.Collections;
using System;

public class ExcecaoSAG : Exception {

	private int codigo;
	private String msg;

	public ExcecaoSAG(string msg){
		this.setMsg(msg);

	}

	public string getMsg() {
		return msg;
	}

	public void setMsg(string msg){
		this.msg=msg;
	}
}
