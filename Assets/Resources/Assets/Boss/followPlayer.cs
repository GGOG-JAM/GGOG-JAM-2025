using UnityEngine;

public class followPlayer : MonoBehaviour
{
    playerMain player;
    public float zOffSet;
    void Start()
    {
        player = FindAnyObjectByType<playerMain>();
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+zOffSet);
    }
}
