using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveForce = 5f;
    private GameObject player;
    private Rigidbody _rb;
    void Start()
    {
        
        this.player = GameObject.Find("Player").gameObject;
        this._rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        Vector3 direction = (player.transform.position - this.transform.position).normalized;
        this._rb.AddForce(moveForce * direction, ForceMode.Force);
        if(this.transform.position.y < -.5)
        {
            Destroy(this.gameObject);
        }
    }
}
