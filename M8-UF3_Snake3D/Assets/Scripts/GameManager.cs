using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public SnakeController snakeController;

    void Start()
    {
        Time.timeScale = 0; // Pausar el tiempo al iniciar el juego
        startCanvas.SetActive(true); // Mostrar el Canvas de inicio
    }

    public void StartGame()
    {
        Time.timeScale = 1; // Reanudar el tiempo al comenzar el juego
        startCanvas.SetActive(false); // Ocultar el Canvas de inicio
        snakeController.enabled = true; // Habilitar el control de la serpiente
    }
}
