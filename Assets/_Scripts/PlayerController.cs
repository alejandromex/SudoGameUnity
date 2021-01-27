using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private GameObject visionPoint;
    private MeshRenderer _meshRenderer;
    private Vector3 OriginalScale;
    private Vector3 OriginalScaleIndicatorPowerUp;
    private float OriginalRigiBodyMass;
    public float moveForce = 5f;
    private float verticalInput;
    private bool powerUpActive = false;
    private string power = "";
    public float powerUpForce = 5f;
    public GameObject indicatorPowerUp;
    public GameObject[] indicatosObjectsPowersUps;
    private MeshFilter meshIndicatorPowerUp;
    void Start()
    {
        this._rb = GetComponent<Rigidbody>();
        this.visionPoint = GameObject.Find("VisionPoint").gameObject;
        this._meshRenderer = GetComponent<MeshRenderer>();
       meshIndicatorPowerUp = indicatorPowerUp.GetComponent<MeshFilter>();
        this.OriginalScale = this.transform.localScale;
        this.OriginalScaleIndicatorPowerUp = this.indicatorPowerUp.transform.localScale;
        this.OriginalRigiBodyMass = _rb.mass;
    }

    void Update()
    {
        this.verticalInput = Input.GetAxis("Vertical");
        this._rb.AddForce(this.visionPoint.transform.forward * moveForce * verticalInput, ForceMode.Force);
        if(this.transform.position.y < -3)
        {
            GlobalStats.spawnEnemiesLevel = 1;
            SceneManager.LoadScene(0);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        indicatorPowerUp.SetActive(true);

        if (other.CompareTag("PowerUp") && other.name.Contains("Speed"))
        {
            this._meshRenderer.material.color = Color.yellow;
            this.moveForce = 10;
            power = "speed";

        }
        if(other.CompareTag("PowerUp") && other.name.Contains("power"))
        {
            power = "power";
            this._meshRenderer.material.color = Color.red;
        }

        if (other.CompareTag("PowerUp") && other.name.Contains("DontFall"))
        {
            power = "dontfall";
            this._meshRenderer.material.color = Color.green;
            this.transform.localScale *= 3;
            this._rb.mass *= 10;
            indicatorPowerUp.transform.localScale *= 3;
        }


        powerUpActive = true;
        Destroy(other.gameObject);
        if (powerUpActive)
        {
            StartCoroutine(powerUpFase1());
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && power=="power")
        {
            Debug.Log("El jugador colicion con " + collision.gameObject.name);

            Rigidbody enemyRigibody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFormPlayer = collision.gameObject.transform.position - this.transform.position;
            enemyRigibody.AddForce(awayFormPlayer * powerUpForce, ForceMode.Impulse);
            
        }
    }

    IEnumerator powerUpFase1()
    {
        for(int i = 1; i< indicatosObjectsPowersUps.Length;i++)
        {
            yield return new WaitForSeconds(10/3);
            meshIndicatorPowerUp.sharedMesh = indicatosObjectsPowersUps[i].GetComponent<MeshFilter>().sharedMesh;
        }

        yield return new WaitForSeconds(10 / 3);

        resetPlayer();


    }

    private void resetPlayer()
    {
        powerUpActive = false;
        this._meshRenderer.material.color = Color.white;
        this.moveForce = 6;
        power = "";
        indicatorPowerUp.SetActive(false);
        meshIndicatorPowerUp.sharedMesh = indicatosObjectsPowersUps[0].GetComponent<MeshFilter>().sharedMesh;
        this.transform.localScale = OriginalScale;
        indicatorPowerUp.transform.localScale = OriginalScaleIndicatorPowerUp;
        this._rb.mass = OriginalRigiBodyMass;
    }

}
