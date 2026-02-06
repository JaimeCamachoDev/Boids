using UnityEngine;
using System.Collections;

public class RandomPointJaime : MonoBehaviour
{
    public Vector3 areaSize = new Vector3(20f, 0f, 20f);
    public float minChangeInterval = 3f; // Intervalo de tiempo mínimo para cambiar la posición del punto
    public float maxChangeInterval = 8f; // Intervalo de tiempo máximo para cambiar la posición del punto
    public GameObject target;
    public bool final = false; // Booleana para activar el final del script
    public Vector3 finalPosition; // Posición prefijada final
    private float timer = 0f;
    private float changeInterval = 0f;

    Vector3 initialPoint = Vector3.zero;

    private void Start()
    {
        initialPoint = target.transform.position;
        // Inicializar el temporizador con un valor aleatorio entre minChangeInterval y maxChangeInterval
        changeInterval = Random.Range(minChangeInterval, maxChangeInterval);
        GetRandomPointInArea(); // Llama a este método en Start para mover el target inicialmente
    }

    private void Update()
    {
        if (final)
        {
            // Si la booleana final está activada, mover el target a la posición final y detener el script
            StopAllCoroutines();
            StartCoroutine(MoveToPosition(target.transform, finalPosition, 5f));
            return;
        }

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            GetRandomPointInArea(); // Llama a este método cuando el tiempo alcanza el intervalo para mover el target
            timer = 0f;
            // Actualizar el intervalo de cambio con un nuevo valor aleatorio entre minChangeInterval y maxChangeInterval
            changeInterval = Random.Range(minChangeInterval, maxChangeInterval);
        }
    }

    private void GetRandomPointInArea()
    {
        // Calcula el punto aleatorio dentro del área
        Vector3 randomPoint = new Vector3(Random.Range(-areaSize.x / 2f, areaSize.x / 2f), 0f, Random.Range(-areaSize.z / 2f, areaSize.z / 2f));
        randomPoint += initialPoint;
        // Inicia una rutina de movimiento suave hacia el nuevo punto
        StartCoroutine(MoveToPosition(target.transform, randomPoint, 5f));
    }

    private IEnumerator MoveToPosition(Transform targetTransform, Vector3 targetPosition, float timeToMove)
    {
        // Guarda la posición inicial del objeto
        Vector3 startPosition = targetTransform.position;

        // Lleva un registro del tiempo transcurrido
        float elapsedTime = 0f;

        // Mientras no haya pasado el tiempo total de movimiento
        while (elapsedTime < timeToMove)
        {
            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Calcula la interpolación lineal entre la posición inicial y la posición objetivo
            float t = Mathf.Clamp01(elapsedTime / timeToMove);
            targetTransform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Espera un frame antes de continuar
            yield return null;
        }

        // Asegúrate de que el objeto esté en la posición exacta al final del movimiento
        targetTransform.position = targetPosition;
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.blue;
            if (Application.isPlaying)
            {
                Gizmos.DrawWireCube(initialPoint, areaSize);
            }
            else
            {
                Gizmos.DrawWireCube(target.transform.position, areaSize);
            }
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.transform.position, 0.1f); // Dibuja una esfera en la posición actual del target
        }
    }
}
