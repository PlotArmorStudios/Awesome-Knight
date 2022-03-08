using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPointer : MonoBehaviour
{
    Transform m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, m_player.position) <= 1f)
        {
            Destroy(gameObject);
        }
    }
}
