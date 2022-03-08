using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField]
    float m_followHeight = 8f;
    [SerializeField]
    float m_followDistance = 6f;

    Transform m_player;
    float m_targetHeight;
    float m_currentHeight;
    float m_currentRotation;

    // Start is called before the first frame update
    void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_targetHeight = m_player.position.y + m_followHeight;
        m_currentRotation = transform.eulerAngles.y;

        m_currentHeight = Mathf.Lerp(transform.position.y, m_targetHeight, 0.9f * Time.deltaTime);

        Quaternion euler = Quaternion.Euler(0f, m_currentRotation, 0f);

        Vector3 targetPosition = m_player.position - (euler * Vector3.forward) * m_followDistance; //target position for camera.

        targetPosition.y = m_currentHeight;

        transform.position = targetPosition;
        transform.LookAt(m_player);
    }
}
