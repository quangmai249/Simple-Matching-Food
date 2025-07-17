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
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        private void Register()
        {
            GameObject[] arr = GameObject.FindGameObjectsWithTag(TagName.TAG_PANEL_ROOT);

            foreach (GameObject item in arr)
            {
                foreach (Transform child in item.transform)
                {
                    if (Enum.TryParse(child.name, out EnumPanelType enumPanelType))
                        panels[enumPanelType] = child.gameObject;
                }
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
        public GameObject GetPanel(EnumPanelType enumPanelType)
        {
            if (panels.TryGetValue(enumPanelType, out GameObject obj))
                return obj;

            return null;
        }
    }
}