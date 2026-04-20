using UnityEngine;

public class BulletFatExplosion : MonoBehaviour
{
    public float LifeTime; // 存活时间

    [HideInInspector]
    public float ActiveTime; // 激活时间
    public PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - ActiveTime >= LifeTime)
        {
            // GetComponent<ParticleSystem>().Stop();
            gameObject.SetActive(false);
            playerControl.SetBulletExplosionPrefabObject(gameObject);
        }
    }
}
