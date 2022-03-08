using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    [SerializeField]
    Texture2D m_cursorTexture;
    [SerializeField] GameObject m_mousePoint;
    CursorMode m_mode = CursorMode.ForceSoftware;
    Vector2 m_hotSpot = Vector2.zero;

    GameObject m_instantiatedMouse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(m_cursorTexture, m_hotSpot, m_mode);
        
        //creates a circle where player clicked
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    temp.y = 4.18831f;
                    if (m_instantiatedMouse == null)
                    {
                        m_instantiatedMouse = Instantiate(m_mousePoint, temp, Quaternion.identity) as GameObject;
                        m_instantiatedMouse.transform.position = temp;
                    }
                    else
                    {
                        Destroy(m_instantiatedMouse);
                        m_instantiatedMouse = Instantiate(m_mousePoint, temp, Quaternion.identity) as GameObject;
                        m_instantiatedMouse.transform.position = temp;
                    }
                }
            }
        }
    }
}
