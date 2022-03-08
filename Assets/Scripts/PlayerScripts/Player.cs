using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator m_animator;
    CharacterController m_characterController;
    CollisionFlags m_collisionFlags = CollisionFlags.None;

    float m_movementSpeed = 5f;
    bool m_canMove;
    bool m_finishedMovement = true;

    Vector3 m_targetPos = Vector3.zero;
    Vector3 m_movement = Vector3.zero;

    float m_gravity = 9.8f;
    float m_height;

    float m_player_ToPointDistance;

    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CalculateHeight();
        //CheckIfFinishedMovement();
        m_movement.y = m_height * Time.deltaTime;
        m_characterController.Move(m_movement);
    }
    
    bool IsGrounded()
    {
        return m_collisionFlags == CollisionFlags.CollidedBelow ? true : false; //this statement is the same as using an if/else stetement for returning true or false
    }

    void CalculateHeight()
    {
        if (IsGrounded())
        {
            m_height = 0f;
        }
        else
        {
            m_height -= m_gravity * Time.deltaTime;
        }
    }

    void CheckIfFinishedMovement()
    {
        if (!m_finishedMovement)
        {
            if (!m_animator.IsInTransition(0) && !m_animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                m_finishedMovement = true;
            }
            else
            {
                MovePlayer();
                m_movement.y = m_height * Time.deltaTime;
                m_collisionFlags = m_characterController.Move(m_movement);
            }
        }
    }
    void MovePlayer()
    {
        m_movement.y = m_height * Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) //Convert Screen coordinates to unity worldspace coordinates on mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Returns a ray going from camera to a screen pooint
            RaycastHit hit; //allows us to extra information from a raycast collision

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    m_player_ToPointDistance = Vector3.Distance(transform.position, hit.point); //store distance from player to where mouse was clicked

                    if (m_player_ToPointDistance >= 1.0f)
                    {
                        m_canMove = true;
                        m_targetPos = hit.point;
                    }
                }
            }
        }

        if (m_canMove)
        {
            m_animator.SetFloat("Walk", 1.0f);
            Vector3 target_Temp = new Vector3(m_targetPos.x, transform.position.y, m_targetPos.z);

            //rotate player to direction of the ray target (mouse click point) 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target_Temp - transform.position), 15.0f * Time.deltaTime);

            m_movement = transform.forward * m_movementSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, m_targetPos) <= 0.3f)
            {
                m_canMove = false;
            }
        }
        else
        {
            m_movement.Set(0f, 0f, 0f);
            m_animator.SetFloat("Walk", 0f);
        }

        if(m_animator.GetBool("Death"))
        {
            m_canMove = false;
        }
    }

    public bool FinishedMovement
    {
        get { return m_finishedMovement; }
        set { m_finishedMovement = value; }
    }

    public Vector3 TargetPosition
    {
        get { return m_targetPos; }
        set { m_targetPos = value; }
    }
}
