using UnityEngine;

public class followPlayer : MonoBehaviour
{
    playerMain player;
    public Vector3 offset = new Vector3(0f, 0f, -5f);
    public bool Lerp;
    public float camLag = 5f;
    void Start()
    {
        player = FindAnyObjectByType<playerMain>();
    }

    void Update()
    {
        if (Lerp)
        {
                Vector3 targetPosition = player.gameObject.transform.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, camLag * Time.deltaTime);
                //camLag * Time.deltaTime camera lagi istersek 1f'in yerine yaz
            }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + offset.magnitude);
        }
}



}
