using System;
using UnityEngine;

public class ExitTrigger : MonoBehaviour {

    public event Action OnPlayerEntered;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter");
        if (!other.CompareTag("Player")) {
            return;
        }
        OnPlayerEntered?.Invoke();
    }
}
