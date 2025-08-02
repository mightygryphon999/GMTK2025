using UnityEngine;

public class Persistent : MonoBehaviour
{
    public int coin;
    public int level;
    private static Persistent instance;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
