﻿using Simulation;
using UnityEngine;

namespace Utils {
  public class CameraSwitchController : MonoBehaviour {
    private Camera mainCamera;
    private FollowCam followingCamera;

    private void Awake() {
      PickCameras();
    }

    private void PickCameras() {
      mainCamera = Camera.main;
      followingCamera = FindObjectOfType<FollowCam>();
    }

    private void Update() {
      if (mainCamera.enabled) {
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        RaycastHit raycastHit;
        if (!Physics.Raycast(mainCamera.transform.position, forward, out raycastHit)) return;
        var hitObject = raycastHit.transform.gameObject;
        var chargedObject = hitObject.GetComponent<ChargedObject>();
        if (chargedObject == null || !Input.GetKeyDown(KeyCode.Space)) return;
        FlipCameras();
        followingCamera.Target = chargedObject.transform;
      } else if (followingCamera.enabled) {
        if (Input.GetKeyDown(KeyCode.Space)) {
          FlipCameras();
        }
      }
    }

    private void FlipCameras() {
      mainCamera.enabled = !mainCamera.enabled;
      followingCamera.enabled = !followingCamera.enabled;
    }
  }
}
