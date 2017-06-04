using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilacaoDosBarcos : MonoBehaviour {

    int angle;
    [SerializeField]
    float amplitude;

	// Use this for initialization
	void Start () {
        StartCoroutine(Movement());
	}

    IEnumerator Movement()
    {
        while (true)
        {
            angle++;
            transform.position += Vector3.up * Mathf.Sin(angle/2) * amplitude;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
