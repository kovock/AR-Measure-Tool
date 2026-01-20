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
    public enum MeasurementMode { Distancia, Area }

    [Header("Configuração de Modo")]
    [SerializeField] private MeasurementMode currentMode = MeasurementMode.Distancia;

    [Header("Prefabs e Configurações")]
    [SerializeField] private GameObject measurementPointPrefab;
    [SerializeField] private GameObject linePrefab; // Prefab com LineRenderer e TextMeshPro
    [SerializeField] private float measurementFactor = 100f;
    [SerializeField] private Vector3 offsetMeasurement = new Vector3(0, 0.08f, 0);

    [Header("Interface (UI)")]
    [SerializeField] private Button clearButton;
    [SerializeField] private Button distanceModeButton;
    [SerializeField] private Button areaModeButton;
    [SerializeField] private TextMeshProUGUI modeIndicatorText;

    private ARRaycastManager raycastManager;
    private Camera mainCamera;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private List<GameObject> allObjects = new List<GameObject>();

    // Variáveis do Modo Distância (Régua)
    private GameObject distStartPoint, distEndPoint;
    private LineRenderer distLine;
    private TextMeshPro distText;
    private bool isDragging = false;

    // Variáveis do Modo Área
    private List<GameObject> areaPoints = new List<GameObject>();
    private LineRenderer polygonLine;
    private TextMeshPro areaLabel;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        mainCamera = Camera.main;

        if (clearButton != null) clearButton.onClick.AddListener(ClearAll);
        if (distanceModeButton != null) distanceModeButton.onClick.AddListener(() => SetMode(MeasurementMode.Distancia));
        if (areaModeButton != null) areaModeButton.onClick.AddListener(() => SetMode(MeasurementMode.Area));

        UpdateUI();
    }

    public void SetMode(MeasurementMode mode)
    {
        currentMode = mode;
        ClearAll();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (modeIndicatorText != null)
            modeIndicatorText.text = "Modo: " + (currentMode == MeasurementMode.Distancia ? "Régua" : "Área");
    }

    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;
        if (EventSystem.current.IsPointerOverGameObject(pointer.deviceId)) return;

        Vector2 touchPos = pointer.position.ReadValue();

        if (currentMode == MeasurementMode.Distancia) HandleDistance(pointer, touchPos);
        else HandleArea(pointer, touchPos);
    }

    // --- MODO DISTÂNCIA: MANTÉM A MEDIDA NA LINHA ---
    void HandleDistance(Pointer pointer, Vector2 touchPos)
    {
        if (pointer.press.wasPressedThisFrame)
        {
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Vector3 pos = hits[0].pose.position;
                distStartPoint = Instantiate(measurementPointPrefab, pos, Quaternion.identity);
                distEndPoint = Instantiate(measurementPointPrefab, pos, Quaternion.identity);

                GameObject lObj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                distLine = lObj.GetComponent<LineRenderer>();
                distText = lObj.GetComponentInChildren<TextMeshPro>();

                allObjects.AddRange(new List<GameObject> { distStartPoint, distEndPoint, lObj });
                isDragging = true;
            }
        }

        if (pointer.press.isPressed && isDragging)
        {
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Vector3 currentPos = hits[0].pose.position;
                distEndPoint.transform.position = currentPos;

                // Atualiza a linha visual
                distLine.SetPosition(0, distStartPoint.transform.position);
                distLine.SetPosition(1, distEndPoint.transform.position);

                // ATUALIZA O TEXTO DE DISTÂNCIA (CM)
                float distance = Vector3.Distance(distStartPoint.transform.position, distEndPoint.transform.position);
                distText.text = $"{(distance * measurementFactor):F1} cm";

                // Posiciona o texto no meio da linha
                distText.transform.position = (distStartPoint.transform.position + distEndPoint.transform.position) / 2 + offsetMeasurement;
                RotateText(distText);
            }
        }

        if (pointer.press.wasReleasedThisFrame) isDragging = false;
    }

    // --- MODO ÁREA: APENAS ÁREA TOTAL NO CENTRO ---
    void HandleArea(Pointer pointer, Vector2 touchPos)
    {
        if (pointer.press.wasPressedThisFrame)
        {
            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Vector3 pos = hits[0].pose.position;
                GameObject pObj = Instantiate(measurementPointPrefab, pos, Quaternion.identity);
                areaPoints.Add(pObj);
                allObjects.Add(pObj);

                if (areaPoints.Count == 1)
                {
                    GameObject lObj = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                    polygonLine = lObj.GetComponent<LineRenderer>();
                    polygonLine.loop = true;
                    areaLabel = lObj.GetComponentInChildren<TextMeshPro>();
                    areaLabel.text = ""; // Fica vazio até ter 3 pontos
                    allObjects.Add(lObj);
                }
                UpdateAreaVisuals();
            }
        }
    }

    void UpdateAreaVisuals()
    {
        if (areaPoints.Count < 2) return;

        polygonLine.positionCount = areaPoints.Count;
        Vector3 center = Vector3.zero;

        for (int i = 0; i < areaPoints.Count; i++)
        {
            Vector3 pPos = areaPoints[i].transform.position;
            polygonLine.SetPosition(i, pPos);
            center += pPos;
        }

        if (areaPoints.Count >= 3)
        {
            float area = CalculateArea();
            areaLabel.text = string.Format("A: {0:F2} m2", area);

            // Texto no centro do polígono
            areaLabel.transform.position = (center / areaPoints.Count) + (offsetMeasurement * 1.5f);
            RotateText(areaLabel);
        }
    }

    float CalculateArea()
    {
        float area = 0;
        for (int i = 0; i < areaPoints.Count; i++)
        {
            Vector3 cur = areaPoints[i].transform.position;
            Vector3 next = areaPoints[(i + 1) % areaPoints.Count].transform.position;
            area += (cur.x * next.z) - (next.x * cur.z);
        }
        return Mathf.Abs(area) / 2.0f;
    }

    void RotateText(TextMeshPro text)
    {
        if (text == null) return;
        text.transform.rotation = Quaternion.LookRotation(text.transform.position - mainCamera.transform.position);
    }

    public void ClearAll()
    {
        foreach (var obj in allObjects) if (obj != null) Destroy(obj);
        allObjects.Clear();
        areaPoints.Clear();
        polygonLine = null;
        areaLabel = null;
        isDragging = false;
    }
}