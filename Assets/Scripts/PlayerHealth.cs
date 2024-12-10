
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");

    [Header("玩家血量")]
    public int hp;
    [Header("背景图片")]
    public Image damageImg;
    [Header("死亡音效")]
    public AudioClip deathClip;
    [Header("受伤音效")]
    public AudioClip hurtClip;

    private Animator animator;
    private Color flashColor = new Color(1f, 0f, 0f, 0.3f); // 玩家受到伤害时背景图片会变红
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(30);
        }
    }

    public void Damage(int dropHp)
    {
        if (GameManager.Instance.GameOver)
            return;

        this.hp -= dropHp;
        audioSource.PlayOneShot(hurtClip);
        StartCoroutine(DamageEffect());

        if (this.hp <= 0)
        {
            this.hp = 0;
            // 播放死亡动画
            animator.SetTrigger(Death);
            audioSource.PlayOneShot(deathClip);

            GameManager.Instance.GameOver = true;
        }
    }

    IEnumerator DamageEffect()
    {
        damageImg.color = flashColor;
        while (true)
        {
            damageImg.color = Color.Lerp(damageImg.color, Color.clear, Time.deltaTime);
            if (damageImg.color.a <= 0.01f)
                break;

            yield return new WaitForEndOfFrame();
        }
    }

    void DeathComplete()
    {
        GetComponent<CapsuleCollider>().isTrigger = true;

        Destroy(this.gameObject, 2f);
    }
}
