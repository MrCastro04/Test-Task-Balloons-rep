using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray);

        if (rayHit.collider == null) return;

        // Проверяем, есть ли на объекте компонент Balloon
        Balloon balloon = rayHit.collider.GetComponent<Balloon>();
        if (balloon != null)
        {
            balloon.Pop(); // Вызываем метод Pop
            Debug.Log($"Balloon popped: {rayHit.collider.gameObject.name}");
        }
        else
        {
            Debug.Log($"Clicked on: {rayHit.collider.gameObject.name}");
        }
    }
}