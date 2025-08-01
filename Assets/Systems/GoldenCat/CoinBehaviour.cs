using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public Vector3 pushDirection;
    public float pushMagnitude;
    public GoldenCatController gcc;
    void Start()
    {
        gcc = FindAnyObjectByType<GoldenCatController>();
    }
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.B))
    //     {
    //         pushCoin();
    //     }
    // }
    public void pushCoin()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(pushDirection.normalized * pushMagnitude, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("coinDestroy"))
        {
            gcc.activeCoins.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
