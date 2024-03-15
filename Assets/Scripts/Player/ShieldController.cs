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

    public void SetShieldActive()
    {
        movingDirection = "up";
        movingEnabled = true;
        _shield.SetActive(true);
    }

    public void SetShieldInactive()
    {
        movingEnabled = true;
        movingDirection = "down";
    }

    public void MoveUp()
    {
        if (_shield.transform.position.y < targetHeight && movingEnabled)
        {
            Vector3 upDirection = moveSpeed * Time.deltaTime * Vector3.up.normalized;
            _shield.transform.Translate(upDirection, Space.World);
            Vector3 forwardDirection = moveSpeed * Time.deltaTime * Vector3.back.normalized;
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
            Vector3 downDirection = moveSpeed * Time.deltaTime * Vector3.down.normalized;
            _shield.transform.Translate(downDirection, Space.World);
            Vector3 backwardDirection = moveSpeed * Time.deltaTime * Vector3.forward.normalized;
            _shield.transform.Translate(backwardDirection, Space.World);
        }
        if (_shield.transform.position.y <= originalHeight)
        {
            movingEnabled = false;
            _shield.SetActive(false);
        }
    }
}
