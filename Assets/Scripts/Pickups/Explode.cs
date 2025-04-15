using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplodeBomb());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ExplodeBomb()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
