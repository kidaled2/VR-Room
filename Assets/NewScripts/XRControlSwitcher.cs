using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

// Alias tanýmýyla hangi XRController’ý kullandýðýmýzý netleþtiriyoruz:
using InputXRController = UnityEngine.InputSystem.XR.XRController;

public class XRControlSwitcher : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;     // XR Rig’in PlayerInput component’i
    [SerializeField] private XRDeviceSimulator deviceSimulator; // Sahnendeki Device Simulator

    private void Start()
    {
        // Baþlangýçta otomatik olarak VR kontrolcüye geç
        Invoke(nameof(SwitchToVR), 1f);
    }

    public void SwitchToVR()
    {
        // Simulator’ý kapat
        deviceSimulator.enabled = false;

        // Sadece InputSystem içindeki XRController tipindeki cihazlarý al
        var xrDevices = InputSystem.devices
            .Where(d => d is InputXRController)
            .ToArray();

        // “XR Controller” scheme’ini, bulunan XR controller cihazlarýyla aktif et
        playerInput.SwitchCurrentControlScheme("XR Controller", xrDevices);
        Debug.Log($"Switched to VR controllers: {string.Join(", ", xrDevices.Select(d => d.name))}");
    }

    public void SwitchToSimulator()
    {
        // Simulator’ý aç
        deviceSimulator.enabled = true;

        // Klavye ve fareyi alýp “Keyboard&Mouse” scheme’ine geri dön
        var kbMouse = new InputDevice[] { Keyboard.current, Mouse.current };
        playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", kbMouse);
        Debug.Log("Switched back to Simulator");
    }
}
