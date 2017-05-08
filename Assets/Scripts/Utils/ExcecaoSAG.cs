using UnityEngine;
using System;

public class ExcecaoSAG : Exception {

	private int codigo;
	private String msg;

    PainelDeErro painelDeErro;

    public ExcecaoSAG(string msg){
        painelDeErro = GameObject.FindGameObjectWithTag("PainelDeErro").GetComponent<PainelDeErro>();
		this.setMsg(msg);
        painelDeErro.MensagemDeErro(msg);

	}

	public string getMsg() {
		return msg;
	}

	public void setMsg(string msg){
		this.msg=msg;
	}

    
}
