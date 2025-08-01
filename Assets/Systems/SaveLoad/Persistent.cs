using UnityEngine;

public class Persistent : MonoBehaviour
{
    public int coin;
    public int level;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
