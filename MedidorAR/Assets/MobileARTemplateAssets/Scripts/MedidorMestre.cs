using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MeasurementController : MonoBehaviour
{
    [Header("Prefabs e Configurações")]
    [SerializeField] private GameObject measurementPointPrefab;
    [SerializeField] private GameObject linePrefab; 
    [SerializeField] private float measurementFactor = 100f;
    [SerializeField] private Vector3 offsetMeasurement = new Vector3(0, 0.05f, 0);

    [Header("UI")]
    [SerializeField] private Button clearButton;

    private ARRaycastManager raycastManager;
    private GameObject currentStartPoint;
    private GameObject currentEndPoint;
    private LineRenderer currentLine;
    private TextMeshPro currentText;

    // Armazenamento para deletar depois
    private List<GameObject> allObjects = new List<GameObject>();
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isDragging = false;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        if (clearButton != null)
            clearButton.onClick.AddListener(ClearAll);
    }

    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        Vector2 touchPosition = pointer.position.ReadValue();

        //if (EventSystem.current.IsPointerOverGameObject(pointer.deviceId))
        //{
           // return;
       // }

        // 1. COMEÇO DO TOQUE: Cria a estrutura da medição
        if (pointer.press.wasPressedThisFrame)
        {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                StartLine(hitPose.position);
                isDragging = true;
            }
        }

        // 2. ARRASTANDO: Atualiza o ponto final e a linha
        if (pointer.press.isPressed && isDragging)
        {
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                UpdateLine(hits[0].pose.position);
            }
        }

        // 3. SOLTOU: Finaliza essa medição e permite a próxima
        if (pointer.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }
    }

    void StartLine(Vector3 position)
    {
        // Cria os pontos
        currentStartPoint = Instantiate(measurementPointPrefab, position, Quaternion.identity);
        currentEndPoint = Instantiate(measurementPointPrefab, position, Quaternion.identity);

        // Cria a linha (use um prefab que já tenha LineRenderer e TextMeshPro para facilitar)
        GameObject lineObj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLine = lineObj.GetComponent<LineRenderer>();
        currentText = lineObj.GetComponentInChildren<TextMeshPro>();

        // Salva na lista para o botão "Limpar"
        allObjects.Add(currentStartPoint);
        allObjects.Add(currentEndPoint);
        allObjects.Add(lineObj);
    }

    void UpdateLine(Vector3 position)
    {
        currentEndPoint.transform.position = position;

        currentLine.SetPosition(0, currentStartPoint.transform.position);
        currentLine.SetPosition(1, currentEndPoint.transform.position);

        float distance = Vector3.Distance(currentStartPoint.transform.position, currentEndPoint.transform.position);
        currentText.text = $"{(distance * measurementFactor).ToString("F2")} cm";

        // Centraliza o texto entre os dois pontos
        currentText.transform.position = (currentStartPoint.transform.position + currentEndPoint.transform.position) / 2 + offsetMeasurement;

        // Faz o texto olhar para a câmera (Billboard)
        currentText.transform.LookAt(Camera.main.transform);
        currentText.transform.Rotate(0, 180, 0);
    }


public void ClearAll()
    {
        Debug.Log("Botão Limpar clicado! Tentando apagar " + allObjects.Count + " objetos.");

        foreach (var obj in allObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        allObjects.Clear();
        Debug.Log("Lista limpa com sucesso.");
    }
}