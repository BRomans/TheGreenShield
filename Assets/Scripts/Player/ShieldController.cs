using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gtec.UnityInterface;

public class ShieldController : FlashObject3D
{
    [SerializeField]
    private float _shieldDuration = 2f;


    [SerializeField]
    private float targetHeight = 1.0f;

    [SerializeField]
    private float originalHeight = 0f;

    [SerializeField]
    public GameObject _shield;

    [SerializeField]
    private float moveSpeed = 3.0f;
    public bool activated { get; set; } = false;

    
    private bool movingEnabled = true;

    private float zMovement = 0.3f;

    public ShieldController(int classId) : base(classId)
    {
    }

    public string movingDirection { get; set; } = "down";

    private void Update()
    {
        
        StartMoving();
      
    }

    public void StartMoving()
    {
        if(movingDirection == "up")
        {
            MoveUp();
        }
        if(movingDirection == "down")
        {
            MoveDown();
        }
    }

    public void SwitchState(bool state)
    {
        activated = state;
        movingEnabled = true;
        //_selected.SetActive(activated);
        ChangeDirection();
    }

    public void ChangeDirection() 
    {
        if(movingDirection == "up")
        {
            
            movingDirection = "down";
            _shield.SetActive(activated);
        }
        else
        {
            movingDirection = "up";
            _shield.SetActive(activated);
        }
    }

    public void MoveUp()
    {
        
        if (_shield.transform.position.y < targetHeight && movingEnabled)
        {
            Vector3 upDirection = Vector3.up.normalized * moveSpeed * Time.deltaTime;
            //Debug.Log("Moving up" + upDirection);
            _shield.transform.Translate(upDirection, Space.World);
            Vector3 forwardDirection = Vector3.back.normalized * moveSpeed * Time.deltaTime;
            _shield.transform.Translate(forwardDirection, Space.World);
        } 
        if(_shield.transform.position.y >= targetHeight)
        {
            movingEnabled = false;
        }
    }

    public void MoveDown()
    {
        if (_shield.transform.position.y > originalHeight && movingEnabled)
        {
            Vector3 downDirection = Vector3.down.normalized * moveSpeed * Time.deltaTime;
            //Debug.Log("Moving down" + downDirection);
            _shield.transform.Translate(downDirection, Space.World);
            Vector3 backwardDirection = Vector3.forward.normalized * moveSpeed * Time.deltaTime;
            _shield.transform.Translate(backwardDirection, Space.World);
        }
        if (_shield.transform.position.y <= originalHeight)
        {
            movingEnabled = false;
        }
    }
}
