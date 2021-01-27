using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameObject.Find("Player").gameObject.GetComponent<Transform>().position;
    }
}
