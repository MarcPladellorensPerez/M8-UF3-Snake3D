using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bodySpeed;
    [SerializeField] private float steerSpeed;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private Transform bodyPartsParent; // Referencia al objeto vacío "BodyParts"

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    private int applesEaten = 0;

    void Start()
    {
        bodyParts.Add(gameObject);
        InvokeRepeating("UpdatePositionHistory", 0f, 0.01f);
        SpawnApple();
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        MoveBody();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GrowSnake();
        }
    }

    void MoveBody()
    {
        for (int i = 1; i < bodyParts.Count; i++)
        {
            Vector3 targetPosition = bodyParts[i - 1].transform.position;
            Vector3 moveDirection = targetPosition - bodyParts[i].transform.position;
            bodyParts[i].transform.position += moveDirection.normalized * bodySpeed * Time.deltaTime;
            bodyParts[i].transform.LookAt(targetPosition);
        }
    }

    void UpdatePositionHistory()
    {
        positionHistory.Insert(0, transform.position);

        if (positionHistory.Count > 500)
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
        }
    }

    void GrowSnake()
    {
        Vector3 newPosition = bodyParts[bodyParts.Count - 1].transform.position - transform.forward * 1.0f;
        GameObject newBodyPart = Instantiate(bodyPrefab, newPosition, Quaternion.identity);
        newBodyPart.transform.parent = bodyPartsParent; // Establecer el padre como el objeto vacío "BodyParts"
        bodyParts.Add(newBodyPart);
    }

    void SpawnApple()
    {
        if (GameObject.FindWithTag("Apple") == null) // Verificar si ya hay una manzana en el juego
        {
            Vector3 randomPos = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
            GameObject apple = Instantiate(applePrefab, randomPos, Quaternion.Euler(-90, 0, 0));
            apple.transform.position = new Vector3(apple.transform.position.x, 1f, apple.transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            GrowSnake();
            SpawnApple();
        }
    }

    public void AppleEaten()
    {
        applesEaten++;
        Destroy(GameObject.FindGameObjectWithTag("Apple"));
        GrowSnake();
        SpawnApple();
    }
}
