using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMover : MonoBehaviour
{
    [HideInInspector] public Collider2D discardPile;
    [HideInInspector] public Player player;

    private Collider2D activeCollision;

    enum State
    {
        None,
        Dragged
    }

    public bool CanMove
    {
        get { return canMove; }
        set
        {
            if (!value && state == State.Dragged)
            {
                transform.position = startPos;
            }
            canMove = value;
        }
    }

    private bool canMove = false;

    private State state = State.None;
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        if (!CanMove || state != State.None) return;

        startPos = transform.position;
        state = State.Dragged;
    }

    private void OnMouseDrag()
    {
        if(!CanMove || state != State.Dragged) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = startPos.z;
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        if (!CanMove || state != State.Dragged) return;

        if (activeCollision == discardPile && discardPile != null)
        {
            gameObject.transform.SetParent(activeCollision.transform, false);
            gameObject.transform.localPosition = Vector3.zero;
            state = State.None;
            player.chosenCard = GetComponent<Card>();
            GetComponent<Collider2D>().enabled = false;
            Destroy(this);
        }
        else
        {
            transform.position = startPos;
            state = State.None;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activeCollision = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeCollision = null;
    }
}
