using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_PlayerController : NetworkBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Transform gunTransform;
    [SerializeField] GameObject bullet;

    Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }


    void FixedUpdate()
    {
        RotateToMouse();
    }


    void OnAttack()
    {
        if (!IsOwner) return;

        SpawnBulletServerRpc();
    }


    [ServerRpc]
    private void SpawnBulletServerRpc()
    {
        GameObject tempBullet = Instantiate(bullet, gunTransform.position, transform.rotation);

        NetworkObject tempNetworkObj = tempBullet.GetComponent<NetworkObject>();
        tempNetworkObj.Spawn();
    }


    void OnMove(InputValue inputValue)
    {
        Vector2 movement = inputValue.Get<Vector2>();

        rb2d.linearVelocity = movement * movementSpeed;
    }


    void RotateToMouse()
    {
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lookPos -= transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
