using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Window _window;
    [SerializeField] private PopUpNoticeController _notification;

    void Start()
    {
        _button.onClick.AddListener(OpenWindow);
        _window.gameObject.SetActive(false);
    }

    private void OpenWindow()
    {
        _window.gameObject.SetActive(true);
        _window.Show();
    }
}