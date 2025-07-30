using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CardObject : MonoBehaviour
{
    public float raiseAmount;
    public float raiseSpeed;
    public float floatAmount;
    public float transferSpeed;
    public GameObject cardSlot;
    public bool placed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void interact()
    {
        transform.DOMoveY(transform.position.y + raiseAmount, raiseSpeed).SetEase(Ease.OutBounce);
    }

    public void placeDown(GameObject target)
    {
        transform.DOMove(new Vector3(target.transform.position.x, target.transform.position.y + floatAmount, target.transform.position.z), transferSpeed).SetEase(Ease.OutSine);
        transform.DORotate(new Vector3(-90, target.transform.eulerAngles.y, 0), transferSpeed).SetEase(Ease.OutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
