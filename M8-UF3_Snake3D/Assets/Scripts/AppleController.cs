using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    private SnakeController snakeController;

    void Start()
    {
        snakeController = FindObjectOfType<SnakeController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            snakeController.AppleEaten();
        }
    }
}
