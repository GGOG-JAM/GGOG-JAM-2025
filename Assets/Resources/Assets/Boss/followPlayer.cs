using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 0f, -5f);
    public bool Lerp;
    public float camLag = 5f;
    void Start()
    {
    }

    void Update()
    {
        if (Lerp)
        {
                Vector3 targetPosition = playerTransform.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, camLag * Time.deltaTime);
                //camLag * Time.deltaTime camera lagi istersek 1f'in yerine yaz
            }
        else
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z + offset.magnitude);
        }
}



}
