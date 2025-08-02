using UnityEngine;

public class RuleBook : MonoBehaviour
{
    public float distanceFromCamera;
    public Vector3 originalPos;
    public Vector3 originalRotVector;
    private Quaternion originalRot;
    public float moveSpeed;
    public float rotateSpeed;
    public bool shouldMove;
    public Transform cam;

    void Start()
    {
        originalRot = Quaternion.Euler(originalRotVector);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            Vector3 targetPos = cam.position + cam.forward * distanceFromCamera;
            Quaternion targetRot = Quaternion.LookRotation(cam.position - gameObject.transform.position);

            transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
        }
        else
        {

            Vector3 targetPosR = originalPos;
            Quaternion targetRotR = originalRot;

            transform.position = Vector3.Lerp(gameObject.transform.position, targetPosR, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotR, Time.deltaTime * rotateSpeed);
        }
    }
    public void lookAt()
    {
        shouldMove = true;
    }
    public void PlaceDown()
    {
        shouldMove = false;
    }
}
