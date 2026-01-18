using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Templates.AR
{
    public class ARTemplateMenuManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] GameObject m_ModalMenu; // Menu de Opções
        [SerializeField] ARDebugMenu m_ARDebugMenu; // Menu de Debug

        [Header("Debug Controls")]
        [SerializeField] DebugSlider m_DebugPlaneSlider;
        [SerializeField] DebugSlider m_DebugMenuSlider;
        [SerializeField] ARPlaneManager m_PlaneManager;

        [Header("Settings")]
        [SerializeField] bool m_UseARPlaneFading = true;

        bool m_ShowOptionsModal;
        bool m_VisualizePlanes = true;
        bool m_ShowDebugMenu;
        bool m_InitializingDebugMenu;
        float m_DebugMenuPlanesButtonValue = 0f;

        readonly List<ARPlane> m_ARPlanes = new List<ARPlane>();
        readonly Dictionary<ARPlane, ARPlaneMeshVisualizer> m_ARPlaneMeshVisualizers = new Dictionary<ARPlane, ARPlaneMeshVisualizer>();
        readonly Dictionary<ARPlane, ARPlaneMeshVisualizerFader> m_ARPlaneMeshVisualizerFaders = new Dictionary<ARPlane, ARPlaneMeshVisualizerFader>();

        void OnEnable()
        {
            m_PlaneManager.trackablesChanged.AddListener(OnPlaneChanged);
        }

        void OnDisable()
        {
            m_PlaneManager.trackablesChanged.RemoveListener(OnPlaneChanged);
        }

        void Start()
        {
            if (m_ARDebugMenu != null)
            {
                m_ARDebugMenu.gameObject.SetActive(true);
                m_InitializingDebugMenu = true;
            }

            if (m_ModalMenu != null) m_ModalMenu.SetActive(false);

            m_DebugMenuSlider.value = m_ShowDebugMenu ? 1 : 0;
            m_DebugPlaneSlider.value = m_VisualizePlanes ? 1 : 0;
        }

        void Update()
        {
            if (m_InitializingDebugMenu)
            {
                m_ARDebugMenu.gameObject.SetActive(false);
                m_InitializingDebugMenu = false;
            }
        }

        // --- FUNÇÕES DE INTERFACE MANTIDAS ---

        public void ShowHideModal()
        {
            m_ShowOptionsModal = !m_ShowOptionsModal;
            if (m_ModalMenu != null) m_ModalMenu.SetActive(m_ShowOptionsModal);
        }

        public void ShowHideDebugPlane()
        {
            m_VisualizePlanes = !m_VisualizePlanes;
            m_DebugPlaneSlider.value = m_VisualizePlanes ? 1 : 0;
            ChangePlaneVisibility(m_VisualizePlanes);
        }

        public void ShowHideDebugMenu()
        {
            m_ShowDebugMenu = !m_ShowDebugMenu;
            m_DebugMenuSlider.value = m_ShowDebugMenu ? 1 : 0;

            if (m_ShowDebugMenu)
            {
                m_ARDebugMenu.gameObject.SetActive(true);
                if (m_ARDebugMenu.showPlanesButton.value != m_DebugMenuPlanesButtonValue)
                    m_ARDebugMenu.showPlanesButton.value = m_DebugMenuPlanesButtonValue;
            }
            else
            {
                m_DebugMenuPlanesButtonValue = m_ARDebugMenu.showPlanesButton.value;
                if (m_DebugMenuPlanesButtonValue == 1f)
                    m_ARDebugMenu.showPlanesButton.value = 0f;

                m_ARDebugMenu.gameObject.SetActive(false);
            }
        }

        void ChangePlaneVisibility(bool setVisible)
        {
            foreach (var plane in m_ARPlanes)
            {
                if (m_ARPlaneMeshVisualizers.TryGetValue(plane, out var visualizer))
                    visualizer.enabled = m_UseARPlaneFading ? true : setVisible;

                if (m_ARPlaneMeshVisualizerFaders.TryGetValue(plane, out var fader))
                {
                    if (m_UseARPlaneFading) fader.visualizeSurfaces = setVisible;
                    else fader.SetVisualsImmediate(1f);
                }
            }
        }

        // --- GERENCIAMENTO DE PLANOS (MANTIDO PARA O DEBUG FUNCIONAR) ---

        void OnPlaneChanged(ARTrackablesChangedEventArgs<ARPlane> eventArgs)
        {
            foreach (var plane in eventArgs.added)
            {
                m_ARPlanes.Add(plane);
                if (plane.TryGetComponent<ARPlaneMeshVisualizer>(out var vizualizer))
                    m_ARPlaneMeshVisualizers.Add(plane, vizualizer);

                if (!plane.TryGetComponent<ARPlaneMeshVisualizerFader>(out var fader))
                    fader = plane.gameObject.AddComponent<ARPlaneMeshVisualizerFader>();

                m_ARPlaneMeshVisualizerFaders.Add(plane, fader);
                fader.visualizeSurfaces = m_VisualizePlanes;
            }

            foreach (var plane in eventArgs.removed)
            {
                m_ARPlanes.Remove(plane.Value);
                m_ARPlaneMeshVisualizers.Remove(plane.Value);
                m_ARPlaneMeshVisualizerFaders.Remove(plane.Value);
            }
        }

        // Funções de Spawn e Delete foram removidas.
    }
}