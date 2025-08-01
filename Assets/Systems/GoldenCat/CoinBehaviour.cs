using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public Vector3 pushDirection;
    public float pushMagnitude;
    public void pushCoin()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(pushDirection.normalized * pushMagnitude, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("coinDestroy"))
        {
            Destroy(gameObject);
        }
    }
}
