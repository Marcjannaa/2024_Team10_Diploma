using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombScript : MonoBehaviour
{
    [SerializeField] private GameObject Bomb;
    private GameObject placed;
    // Start is called before the first frame update
    private Vector3 position;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            position = transform.position;
            if (PickupScript.getStats().Bombs.Value > 0)
            {
                placed = Instantiate(Bomb, new Vector3(position.x, Bomb.transform.position.y, position.z), new Quaternion());
                PickupScript.getStats().Bombs.Modify(-1);
            }
        }
    }
    
    
}
