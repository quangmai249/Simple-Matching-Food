using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

namespace Assets.Scrips.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        public static UIManager instance;

        private Transform _panelRoot;
        private Dictionary<EnumPanelType, GameObject> panels = new Dictionary<EnumPanelType, GameObject>();

        protected override void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            this.Register();
        }

        private void Start()
        {
        }

        private void Register()
        {
            _panelRoot = GameObject.FindGameObjectWithTag(TagName.TAG_PANEL_ROOT).transform;

            foreach (Transform child in _panelRoot)
            {
                if (Enum.TryParse(child.name, out EnumPanelType enumPanelType))
                    panels[enumPanelType] = child.gameObject;
            }
        }

        public void HiddenPanel(EnumPanelType enumPanelType)
        {
            if (panels.TryGetValue(enumPanelType, out GameObject obj))
                obj.SetActive(false);
        }

        public void ShowPanel(EnumPanelType enumPanelType)
        {
            foreach (var item in panels)
            {
                if (item.Value.activeSelf == true)
                    item.Value.SetActive(false);
            }

            if (panels.TryGetValue(enumPanelType, out GameObject obj))
                obj.SetActive(true);
            else
                Debug.Log($"do not show bc not found {enumPanelType}");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}