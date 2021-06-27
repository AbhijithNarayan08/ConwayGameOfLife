using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellObject : MonoBehaviour
{
    public bool m_IsAlive = false;

    public int m_NumNeighbours = 0;

    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void OnMouseDown()
    {
        SetStatus(!m_IsAlive);
    }

    public void SetStatus(bool alive)
    {
        m_IsAlive = alive;
        
        if(spriteRenderer != null)
        {
            spriteRenderer.color = (alive == true) ? Color.black : Color.white;
        }
    }
}
