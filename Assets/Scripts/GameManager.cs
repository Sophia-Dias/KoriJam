using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
  Menu,
  Play,
  Stopped,
}

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  public GameObject creditCanvas;
  public GameState currentGameState { get; private set; }
  public GameObject clapCanvas;

  private void Awake()
  {
    if (Instance == null) Instance = this;
    if (Instance != this) Destroy(Instance);
  }

  private void Start()
  {
    creditCanvas.SetActive(false);
    clapCanvas.SetActive(true);
    currentGameState = GameState.Play;
  }

  public void ShowCredits()
  {
    creditCanvas.SetActive(true);
    clapCanvas.SetActive(false);
    currentGameState = GameState.Stopped;
  }

  public bool CanMove()
  {
    return currentGameState == GameState.Play && !DialogManager.Instance.isTalking;
  }
}
