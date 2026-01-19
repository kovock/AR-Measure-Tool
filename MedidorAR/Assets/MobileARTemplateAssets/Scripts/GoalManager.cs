using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Templates.AR
{
    public class GoalManager : MonoBehaviour
    {
        
        public enum OnboardingGoals { Empty, FindSurfaces, TapSurface, Hints, Scale }

        [Serializable]
        public class Step
        {
            public GameObject stepObject;
            public string buttonText;
            public bool includeSkipButton;
        }

        [SerializeField]
        List<Step> m_StepList = new List<Step>();

        [SerializeField]
        ObjectSpawner m_ObjectSpawner;

        [SerializeField]
        GameObject m_GreetingPrompt;

        [SerializeField]
        GameObject m_OptionsButton;

        [SerializeField]
        GameObject m_CreateButton;

        [SerializeField]
        ARTemplateMenuManager m_MenuManager;

       
        public ObjectSpawner objectSpawner { get => m_ObjectSpawner; set => m_ObjectSpawner = value; }
        public ARTemplateMenuManager menuManager { get => m_MenuManager; set => m_MenuManager = value; }

        void Start()
        {
            
            if (m_GreetingPrompt != null)
                m_GreetingPrompt.SetActive(true);

            if (m_OptionsButton != null)
                m_OptionsButton.SetActive(false);

            if (m_CreateButton != null)
                m_CreateButton.SetActive(false);
        }

        /// <summary>
        /// ESTE É O START: Chamado pelo botão do Greeting Prompt para liberar o app.
        /// </summary>
        public void StartCoaching()
        {
            // Desliga o prompt inicial
            if (m_GreetingPrompt != null)
                m_GreetingPrompt.SetActive(false);

            // Liga a UI principal
            if (m_OptionsButton != null)
                m_OptionsButton.SetActive(true);

            if (m_CreateButton != null)
                m_CreateButton.SetActive(true);

            // Ativa o MenuManager
            if (m_MenuManager != null)
                m_MenuManager.enabled = true;

           
            foreach (var step in m_StepList)
            {
                if (step.stepObject != null)
                    step.stepObject.SetActive(false);
            }
        }

        void Update() { }
        void CompleteGoal() { }
        public void ForceCompleteGoal() { }
        void OnObjectSpawned(GameObject spawnedObject) { }
    }
}