using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class RLLaunch2 : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private float coolDown = 2f;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private GameObject StandardRocket, VenomRocket;

    private bool canShoot = true;
    private InputAction launch1, launch2;

    void OnEnable() => InputActions.FindActionMap("RocketLauncher").Enable();
    void OnDisable() => InputActions.FindActionMap("RocketLauncher").Disable();

    void Awake()
    {
        launch1 = InputSystem.actions.FindAction("Launch"); // Обычная ракета
        launch2 = InputSystem.actions.FindAction("Launch2"); // Ядовитая ракета
    }

    IEnumerator Launch(GameObject Rocket)
    {
        canShoot = false;
        Instantiate(Rocket, launchPoint.position, launchPoint.rotation);

        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }
    void Update()
    {
        if (!canShoot)
            return;

        if (launch1.IsPressed())
            StartCoroutine( Launch(StandardRocket) );
        else if (launch2.IsPressed())
            StartCoroutine( Launch(VenomRocket) );
    }
}
