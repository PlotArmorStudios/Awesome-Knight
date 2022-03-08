using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] float destroyTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);   
    }
}
