using TMPro;
using UnityEngine;

public class PlayerHpDisplay : MonoBehaviour
{
    [Header("玩家血量文本")]
    public TMP_Text hpText;

    private PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHp();
    }

    void DisplayHp()
    {
        int hp = player.hp;

        hpText.text = player.hp >= 40 ? $"Player HP: {hp}" : $"<color=red>Player HP: {hp}</color>";
    }
}
