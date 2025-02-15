using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace KeySystem
{
    public class XRKeyRaycast : MonoBehaviour
    {
        [SerializeField] private XRRayInteractor rayInteractor;
        [SerializeField] private int rayLength = 5;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string excludeLayerName = null;
        [SerializeField] private Image crosshair = null;

        [Header("Input Actions")]
        [SerializeField] private InputActionProperty interactAction;

        private KeyItemController raycastedObject;
        private bool isCrosshairActive;
        private bool doOnce;
        private const string interactableTag = "InteractiveObject";

        private void OnEnable()
        {
            interactAction.action.Enable();
            interactAction.action.performed += OnInteractPressed;
        }

        private void OnDisable()
        {
            interactAction.action.Disable();
            interactAction.action.performed -= OnInteractPressed;
        }

        private void Update()
        {
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
                if (hit.collider.CompareTag(interactableTag) && (mask & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    if (!doOnce)
                    {
                        raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                        CrosshairChange(true);
                    }
                    isCrosshairActive = true;
                    doOnce = true;
                }
            }
            else
            {
                if (isCrosshairActive)
                {
                    CrosshairChange(false);
                    doOnce = false;
                }
            }
        }

        private void OnInteractPressed(InputAction.CallbackContext context)
        {
            if (isCrosshairActive && raycastedObject != null)
            {
                raycastedObject.ObjectInteraction();
            }
        }

        private void CrosshairChange(bool on)
        {
            if (crosshair == null) return;

            if (on && !doOnce)
            {
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color = Color.white;
                isCrosshairActive = false;
            }
        }
    }
}