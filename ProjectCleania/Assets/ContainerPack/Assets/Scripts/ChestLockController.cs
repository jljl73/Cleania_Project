using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example class that shows how you can work with animator of chest via scripting.
/// This class is not required and exists only for demonstration purposes.
/// </summary>
[RequireComponent(typeof(Animator))]
public class ChestLockController : MonoBehaviour {
    private Animator _animator;

    /// <summary>
    /// Locked chest state means that you can not open the chest via appropriate method
    /// </summary>
    public bool isLocked { get; private set; } = true;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Unlock or lock the chest via animator
    /// </summary>
    public void SetLocked(bool value) {
        if (value == isLocked) {
            return;
        }

        isLocked = value;

        var rb = GetComponent<Rigidbody>();
        if (rb != null) {
            Destroy(rb);
        }

        _animator.enabled = true;
        _animator.SetBool("isLocked", value);
    }

    public void OnAnimationEventUnlock() {
        if (!isLocked) {
            _animator.enabled = false;
            gameObject.AddComponent<Rigidbody>();
        }
    }
}