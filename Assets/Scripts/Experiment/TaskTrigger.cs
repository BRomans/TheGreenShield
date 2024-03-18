using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField]
    public string TriggerName = "StartTask";

    private TaskController _taskController;

    // Start is called before the first frame update
    void Start()
    {
        _taskController = GetComponentInParent<TaskController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _taskController.TaskTriggerEntered(TriggerName);
        }
    }
}
