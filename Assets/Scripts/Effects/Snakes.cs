using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : MonoBehaviour
{
    public float XSpeed;
    public float YSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAfter5());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(XSpeed * Time.deltaTime, YSpeed * Time.deltaTime, 0);

    }

    IEnumerator KillAfter5()
    {
        yield return new WaitForSeconds(5f);
        Destroy(transform.gameObject);
    }
}
