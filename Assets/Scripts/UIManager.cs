using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject mainMenuPanel;
    public GameObject waitingPanel;
    public GameObject onGameUIPanel;
    public GameObject _Camera;
    public Button createRoomButton;
    public Button joinRoomButton;
    public TMP_InputField roomNameInputField;

    private NetworkManager networkManager;
    private void Awake()
    {
        // Implementing the singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();

        createRoomButton.onClick.AddListener(OnCreateRoomClicked);
        joinRoomButton.onClick.AddListener(OnJoinRoomClicked);

        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        waitingPanel.SetActive(false);
        onGameUIPanel.SetActive(false);
    }

    public void ShowWaitingPanel()
    {
        mainMenuPanel.SetActive(false);
        waitingPanel.SetActive(true);
        onGameUIPanel.SetActive(false);
    }

    public void ShowOnGameUI()
    {
        _Camera.SetActive(false);
        mainMenuPanel.SetActive(false);
        waitingPanel.SetActive(false);
        onGameUIPanel.SetActive(true);
    }

    private void OnCreateRoomClicked()
    {
        string roomName = roomNameInputField.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            ShowWaitingPanel();
            networkManager.CreateRoom(roomName);
        }
    }

    private void OnJoinRoomClicked()
    {
        string roomName = roomNameInputField.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            ShowWaitingPanel();
            networkManager.JoinRoom(roomName);
        }
    }
}
