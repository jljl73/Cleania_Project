using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example class that shows how you can work with animator of chest via scripting.
/// This class is not required and exists only for demonstration purposes.
/// </summary>
[RequireComponent(typeof(Animator))]
public class ChestController : MonoBehaviour {
    private Animator _chestAnimator;
    private ChestLockController _chestLockController;

    /// <summary>
    /// Is chest currently opened or not
    /// </summary>
    public bool isOpen { get; private set; } = false;

    private void Awake() {
        _chestAnimator = GetComponent<Animator>();
        _chestLockController = GetComponentInChildren<ChestLockController>();
    }

    // Uncomment this if you want to open and close chest by mouse click.
    // Make sure that chest has a collider.
    private void OnMouseDown() {
        if (Input.GetKey(KeyCode.LeftControl)) {
            _chestLockController.SetLocked(!_chestLockController.isLocked);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) {
            StartCoroutine(UnlockAndOpen());
        }
        else {
            SetOpen(!isOpen);
        }
    }

    /// <summary>
    /// Open or close the chest via animator
    /// </summary>
    public void SetOpen(bool value) {
        if (value == isOpen) {
            return;
        }

        isOpen = value;
        _chestAnimator.SetBool("isOpen", value);
    }

    IEnumerator UnlockAndOpen() {
        _chestLockController.SetLocked(false);
        yield return new WaitForSeconds(0.6f);
        SetOpen(true);
    }
}