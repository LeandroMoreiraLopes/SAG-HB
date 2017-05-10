using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //variaveis de referencia para movimentacao/rotação da camera
	GameObject cam;
    Transform rotX;
    bool rotating = false;
	float mouseClickX, mouseClickY, mouseMoveX, mouseMoveY;

	enum Posicionamento{intro, livre, tema1, tema2, tema3, tema4};
    Posicionamento myPosition = Posicionamento.intro;

    [SerializeField] Transform[] temaTransforms;

	//variaveis para o camera shake
	Vector3 initialPosition;
	float amplitude = 0.5f;
	float duration = 0.5f;
    bool isShaking = false;

    AudioSource somAmbiente;

    // Use this for initialization
    void Start()
    {
        //pega o filho
        rotX = transform.GetChild(0);
        //pega o neto
        cam = transform.GetChild(0).GetChild(0).gameObject;

        somAmbiente = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		if (isShaking)
		{
			transform.position = initialPosition + Random.insideUnitSphere*amplitude;
		}
        
		mouseMoveX = Input.mousePosition.x;
		mouseMoveY = Input.mousePosition.y;

		cam.transform.LookAt(gameObject.transform);

        //se a posicao e livre, libera os movimentos
        if (myPosition == Posicionamento.livre)
        {
            //moving camera by mouse position
            if (Input.mousePosition.x > Screen.width - 50)
            {
                transform.Translate(2 * Time.deltaTime, 0, 0);
            }

            else if (Input.mousePosition.x < 50)
            {
                transform.Translate(-2 * Time.deltaTime, 0, 0);
            }

            if (Input.mousePosition.y > Screen.height - 50)
            {
                transform.Translate(0, 0, 2 * Time.deltaTime);
            }

            else if (Input.mousePosition.y < 50)
            {
                transform.Translate(0, 0, -2 * Time.deltaTime);
            }

            //limite de movimentacao
            if (transform.position.x < -10)
                transform.position = new Vector3(-10, transform.position.y, transform.position.z);

            else if (transform.position.x > 10)
                transform.position = new Vector3(10, transform.position.y, transform.position.z);

            if (transform.position.z < -10)
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            else if (transform.position.z > 5)
                transform.position = new Vector3(transform.position.x, transform.position.y, 5);

            if (Input.GetMouseButtonDown(1))
		    {
			    rotating = true;
			    mouseClickX = Input.mousePosition.x;
			    mouseClickY = Input.mousePosition.y;
		    }

		    else if (Input.GetMouseButtonUp(1))
		    {
			    rotating = false;
		    }

		    if (rotating)
		    {
			    if (mouseClickX - mouseMoveX < -50)
				    transform.Rotate(0,-30*Time.deltaTime, 0);

			    else if (mouseClickX - mouseMoveX > 50)
				    transform.Rotate(0,30*Time.deltaTime, 0);

			    if (mouseClickY - mouseMoveY < -20)
				    rotX.Rotate(10*Time.deltaTime, 0, 0);
			
			    else if (mouseClickY - mouseMoveY > 20)
				    rotX.Rotate(-10*Time.deltaTime, 0, 0);
		    }

		    if (Vector3.Distance(transform.position, cam.transform.position) > 2 
		        && Input.GetAxis("Mouse ScrollWheel") > 0)
		    {
			    cam.transform.Translate(0,0,15*Time.deltaTime);
		    }


		    else if (Vector3.Distance(transform.position, cam.transform.position) < 10
		        && Input.GetAxis("Mouse ScrollWheel") < 0)
		    {
			    cam.transform.Translate(0,0,-15*Time.deltaTime);
		    }

		    if (rotX.localEulerAngles.x >= 60)
		    {
			    rotX.localEulerAngles = new Vector3(60, rotX.localEulerAngles.y, rotX.localEulerAngles.z);
		    }

		    else if (rotX.localEulerAngles.x <= 5)
		    {
			    rotX.localEulerAngles = new Vector3(5, rotX.localEulerAngles.y, rotX.localEulerAngles.z);
		    }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }

	}

	public void Shake()
	{
		initialPosition = transform.position;
		isShaking = true;
		CancelInvoke();
		Invoke("StopShaking",duration);
	}

	void StopShaking()
	{
		isShaking = false;
		transform.position = initialPosition;
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
	}

    public void SetPosicionamento(int p)
    {
        somAmbiente.Play();

        if (p == 0)
            myPosition = Posicionamento.intro;
        else if (p == 1)
            myPosition = Posicionamento.livre;
        else if (p == 2)
            myPosition = Posicionamento.tema1;
        else if (p == 3)
            myPosition = Posicionamento.tema2;
        else if (p == 4)
            myPosition = Posicionamento.tema3;
        else if (p == 5)
            myPosition = Posicionamento.tema4;
    }


}
